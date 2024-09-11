using Reloaded.Memory.SigScan.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using System.Diagnostics;
using System.Text;
using p3rpc.socialStatTracker.Configuration;
using System.Drawing;

namespace p3rpc.socialStatTracker;
internal class Utils
{
    private static ILogger _logger;
    private static Config _config;
    private static IStartupScanner _startupScanner;
    internal static nint BaseAddress { get; private set; }

    internal static bool Initialise(ILogger logger, Config config, IModLoader modLoader)
    {
        _logger = logger;
        _config = config;
        using var thisProcess = Process.GetCurrentProcess();
        BaseAddress = thisProcess.MainModule!.BaseAddress;

        var startupScannerController = modLoader.GetController<IStartupScanner>();
        if (startupScannerController == null || !startupScannerController.TryGetTarget(out _startupScanner))
        {
            LogError($"Unable to get controller for Reloaded SigScan Library, stuff won't work :(");
            return false;
        }

        return true;

    }

    internal static void LogDebug(string message)
    {
        if (_config.DebugEnabled)
            _logger.WriteLine($"[Social Stat Tracker] {message}");
    }

    internal static void Log(string message)
    {
        _logger.WriteLine($"[Social Stat Tracker] {message}");
    }

    internal static void LogError(string message, Exception e)
    {
        _logger.WriteLine($"[Social Stat Tracker] {message}: {e.Message}", System.Drawing.Color.Red);
    }

    internal static void LogError(string message)
    {
        _logger.WriteLine($"[Social Stat Tracker] {message}", System.Drawing.Color.Red);
    }

    internal static void SigScan(string pattern, string name, Action<nint> action)
    {
        _startupScanner.AddMainModuleScan(pattern, result =>
        {
            if (!result.Found)
            {
                LogError($"Unable to find {name}, stuff won't work :(");
                return;
            }
            LogDebug($"Found {name} at 0x{result.Offset + BaseAddress:X}");

            action(result.Offset + BaseAddress);
        });
    }

    // Pushes the value of an xmm register to the stack, saving it so it can be restored with PopXmm
    public static string PushXmm(int xmmNum)
    {
        return // Save an xmm register 
            $"sub rsp, 16\n" + // allocate space on stack
            $"movdqu dqword [rsp], xmm{xmmNum}\n";
    }

    // Pushes all xmm registers (0-15) to the stack, saving them to be restored with PopXmm
    public static string PushXmm()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < 16; i++)
        {
            sb.Append(PushXmm(i));
        }
        return sb.ToString();
    }

    // Pops the value of an xmm register to the stack, restoring it after being saved with PushXmm
    public static string PopXmm(int xmmNum)
    {
        return                 //Pop back the value from stack to xmm
            $"movdqu xmm{xmmNum}, dqword [rsp]\n" +
            $"add rsp, 16\n"; // re-align the stack
    }

    // Pops all xmm registers (0-7) from the stack, restoring them after being saved with PushXmm
    public static string PopXmm()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 7; i >= 0; i--)
        {
            sb.Append(PopXmm(i));
        }
        return sb.ToString();
    }

    /// <summary>
    /// Gets the address of a global from something that references it
    /// </summary>
    /// <param name="ptrAddress">The address to the pointer to the global (like in a mov instruction or something)</param>
    /// <returns>The address of the global</returns>
    internal static unsafe nuint GetGlobalAddress(nint ptrAddress)
    {
        return (nuint)((*(int*)ptrAddress) + ptrAddress + 4);
    }

    public class MultiSignature
    {
        public readonly object __sigLock;
        public int registeredSignatures { get; set; }
        public nuint? returnedAddress { get; set; }
        public MultiSignature(string[] patterns, string name, Func<int, nuint> transformCb, Action<long> hookerCb)
        {
            __sigLock = new object();
            returnedAddress = null;

            registeredSignatures = patterns.Length;
            foreach (var pattern in patterns)
            {
                _startupScanner.AddMainModuleScan(pattern, result =>
                {
                    lock (__sigLock)
                    {
                        registeredSignatures--;
                    }
                    if (!result.Found)
                    {
                        if (returnedAddress != null)
                        {
                            LogDebug($"Location {name} was already found in a candidate pattern");
                        }
                        else if (registeredSignatures == 0)
                        {
                            LogError($"Couldn't find location for {name}, stuff will break :(");
                        }
                        else
                        {
                            LogDebug($"Couldn't find location for {name} using pattern {pattern}, trying with another pattern...");
                        }
                        return;
                    }
                    var callHookCb = false;
                    lock (__sigLock)
                    {
                        if (returnedAddress == null)
                        {
                            returnedAddress = transformCb(result.Offset);
                            callHookCb = true;
                        }
                    }
                    if (callHookCb)
                    {
                        Log($"Found {name} at 0x{returnedAddress:X}");
                        hookerCb((long)returnedAddress);
                    }
                    else
                    {
                        LogDebug($"Location {name} was already found in a candidate pattern");
                        return;
                    }
                });
            }
        }

    }    
}
