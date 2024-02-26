using p3rpc.socialStatTracker.Configuration;
using p3rpc.socialStatTracker.Template;
using Reloaded.Hooks.Definitions;
using Reloaded.Mod.Interfaces;
using System.Diagnostics;
using UE4SSDotNetFramework.Framework;
using static p3rpc.socialStatTracker.Native.UnrealString;
using static p3rpc.socialStatTracker.Native.xrd777;
using static p3rpc.socialStatTracker.Utils;
using IReloadedHooks = Reloaded.Hooks.ReloadedII.Interfaces.IReloadedHooks;

namespace p3rpc.socialStatTracker;
/// <summary>
/// Your mod logic goes here.
/// </summary>
public class Mod : ModBase // <= Do not Remove.
{
    /// <summary>
    /// Provides access to the mod loader API.
    /// </summary>
    private readonly IModLoader _modLoader;

    /// <summary>
    /// Provides access to the Reloaded.Hooks API.
    /// </summary>
    /// <remarks>This is null if you remove dependency on Reloaded.SharedLib.Hooks in your mod.</remarks>
    private readonly IReloadedHooks? _hooks;

    /// <summary>
    /// Provides access to the Reloaded logger.
    /// </summary>
    private readonly ILogger _logger;

    /// <summary>
    /// Entry point into the mod, instance that created this class.
    /// </summary>
    private readonly IMod _owner;

    /// <summary>
    /// Provides access to this mod's configuration.
    /// </summary>
    private Config _configuration;

    /// <summary>
    /// The configuration of the currently executing mod.
    /// </summary>
    private readonly IModConfig _modConfig;

    private IHook<ParameterStatusDrawDelegate> _paramDrawHook;
    private UIDrawBaseActor? _drawBase = null;

    public Mod(ModContext context)
    {
        //Debugger.Launch();
        _modLoader = context.ModLoader;
        _hooks = context.Hooks;
        _logger = context.Logger;
        _owner = context.Owner;
        _configuration = context.Configuration;
        _modConfig = context.ModConfig;

        Initialise(_logger, _configuration, _modLoader);

        SigScan("40 55 57 48 81 EC D8 00 00 00 48 8B 05 ?? ?? ?? ??", "HeroParamStatusDraw", address =>
        {
            _paramDrawHook = _hooks.CreateHook<ParameterStatusDrawDelegate>(ParamStatusDraw, address).Activate();
        });
    }

    private unsafe void ParamStatusDraw(nuint info, uint param_2)
    {
        _paramDrawHook.OriginalFunction(info, param_2);

        var drawBaseRef = ObjectReference.FindFirstOf("UIDrawBaseActor");
        if (drawBaseRef is null)
            return;
        var drawBase = new UIDrawBaseActor(drawBaseRef.Pointer);

        var heroParamRef = ObjectReference.FindFirstOf("HeroParameterHandle");
        if (heroParamRef is null)
            return;

        var heroParam = new HeroParameterHandle(heroParamRef.Pointer);

        if (_requiredPoints == null)
            SetupRequiredPoints(heroParam);

        for(int i = 0; i < 3; i++)
        {
            var points = heroParam.Instance.points[i].points;
            var level = GetLevel(i, points);
            string pointsStr;
            if (level == 6)
            {
                if (points == _requiredPoints![i][5])
                    continue;
                pointsStr = $"+{points - _requiredPoints![i][5]}";
            }
            else
            {
                var required = _requiredPoints![i][level];
                var lastRequired = _requiredPoints[i][level - 1];
                pointsStr = $"{points - lastRequired}/{required - lastRequired}";
            }

            drawBase.BPUICommand_FontDraw(_positions[i].X, _positions[i].Y, 100, new FString(pointsStr), 255, 255, 255, 255, 1, 1, EUI_DRAW_POINT.UI_DRAW_CENTER_CENTER, EUIFontStyle.EUI_Defult_Value);
        }
    }

    private (int X, int Y)[] _positions = 
    {
        (1500, 860),
        (1400, 465),
        (945, 860)
    };

    private int[][]? _requiredPoints;

    private unsafe void SetupRequiredPoints(HeroParameterHandle heroParam)
    {
        _requiredPoints = new int[3][];
        for (int i = 0; i < 3; i++)
        {
            _requiredPoints[i] = new int[6];
            var requiredArr = heroParam.Instance.pDataAsset->Tables.Values[i].Points;
            int last = 0;
            for (int level = 0; level < 6; level++)
            {
                var next = last + requiredArr.Values[level];
                _requiredPoints[i][level] = next;
                last = next;
            }
        }
    }

    private int GetLevel(int stat, int points)
    {
        for(int i = 0; i < 6; i++)
        {
            if (points < _requiredPoints![stat][i])
                return i;
        }
        return 6;
    }

    private delegate void ParameterStatusDrawDelegate(nuint info, uint param_2);

    #region Standard Overrides
    public override void ConfigurationUpdated(Config configuration)
    {
        // Apply settings from configuration.
        // ... your code here.
        _configuration = configuration;
        _logger.WriteLine($"[{_modConfig.ModId}] Config Updated: Applying");
    }
    #endregion

    #region For Exports, Serialization etc.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Mod() { }
#pragma warning restore CS8618
    #endregion
}