using p3rpc.socialStatTracker.Configuration;
using p3rpc.socialStatTracker.Template;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.Enums;
using Reloaded.Hooks.Definitions.X64;
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
public unsafe class Mod : ModBase // <= Do not Remove.
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
    private float* _circleSizes;
    private IAsmHook _circleSizeHook;
    private IReverseWrapper<GetCircleSizeDelegate> _getCircleSizeReverseWrapper;

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

        SigScan("48 8D 05 ?? ?? ?? ?? F3 44 0F 10 44 ?? ??", "CircleSize", address =>
        {
            _circleSizes = (float*)GetGlobalAddress(address + 3);

            string[] function =
            {
                "use64",
                "push rcx\npush rdx\npush r8\npush r10\npush r11",
                "sub rsp, 40",
                $"{_hooks.Utilities.GetAbsoluteCallMnemonics(GetCircleSize, out _getCircleSizeReverseWrapper)}",
                "add rsp, 40",
                "movss xmm8, xmm0",
                "pop r11\npop r10\npop r8\npop rdx\npop rcx",
            };
            _circleSizeHook = _hooks.CreateAsmHook(function, address + 7, AsmHookBehaviour.ExecuteAfter).Activate();
        });
    }

    private float GetCircleSize(int stat, int level)
    {
        if(level == 6) return _circleSizes[5];

        var points = _lastPoints[stat];
        if (points == -1) return _circleSizes[level - 1]; // In case last points isn't set up yet for some reason

        var required = _requiredPoints![stat][level];
        var lastRequired = _requiredPoints[stat][level - 1];
        var nextSize = _circleSizes[level] - 5;
        var currentSize = _circleSizes[level - 1];

        var percent = ((float)points - lastRequired) / ((float)required - lastRequired);
        return currentSize + percent * (nextSize - currentSize);
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
            if (!TryGetPointsStr(i, heroParam.Instance.points[i].points, out var pointsStr))
                continue;

            drawBase.BPUICommand_FontDraw(_positions[i].X, _positions[i].Y, 100, pointsStr, 255, 255, 255, 255, 1, 1, EUI_DRAW_POINT.UI_DRAW_CENTER_CENTER, EUIFontStyle.EUI_Defult_Value);
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

    private FString[] _pointStrs = new FString[3];
    private FString _blankStr = new FString("");
    private int[] _lastPoints = { -1, -1, -1 };
    private bool[] _resetStr = { false, false, false };

    private bool TryGetPointsStr(int stat, int points, out FString pointsFStr)
    {
        if (!_resetStr[stat] && _lastPoints[stat] == points)
        {
            pointsFStr = _pointStrs[stat];
            return _configuration.DisplayType != Config.PointDisplayType.None;
        }

        _resetStr[stat] = false;
        var level = GetLevel(stat, points);
        pointsFStr = _blankStr;
        string pointsStr;
        if (level == 6)
        {
            if (points == _requiredPoints![stat][5] || _configuration.DisplayType == Config.PointDisplayType.None)
                return false;
            pointsStr = $"+{points - _requiredPoints![stat][5]}";
        }
        else
        {
            var required = _requiredPoints![stat][level];
            var lastRequired = _requiredPoints[stat][level - 1];

            switch(_configuration.DisplayType)
            {
                case Config.PointDisplayType.Exact:
                    pointsStr = $"{points - lastRequired}/{required - lastRequired}";
                    break;
                case Config.PointDisplayType.Percent:
                    pointsStr = $"{((float)points - lastRequired) / ((float)required - lastRequired) * 100:0}%";
                    break;
                default:
                    _lastPoints[stat] = points;
                    return false;
            }                
        }

        pointsFStr = new FString(pointsStr);
        _lastPoints[stat] = points;
        _pointStrs[stat].Dispose();
        _pointStrs[stat] = pointsFStr;

        return true;
    }

    private delegate void ParameterStatusDrawDelegate(nuint info, uint param_2);

    [Function([FunctionAttribute.Register.r8, FunctionAttribute.Register.r15], FunctionAttribute.Register.rax, true)]
    private delegate float GetCircleSizeDelegate(int stat, int level);

    #region Standard Overrides
    public override void ConfigurationUpdated(Config configuration)
    {
        // Clear last points so strings will be recreated
        if(_configuration.DisplayType != configuration.DisplayType)
        {
            for(int i = 0; i < _resetStr.Length; i++)
                _resetStr[i] = true;
        }

        // Apply settings from configuration.
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