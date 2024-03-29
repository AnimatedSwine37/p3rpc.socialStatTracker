﻿using p3rpc.socialStatTracker.Template.Configuration;
using System.ComponentModel;

namespace p3rpc.socialStatTracker.Configuration;
public class Config : Configurable<Config>
{
    [DisplayName("Point Display Type")]
    [Description("How the number of points you need will be displayed.")]
    [DefaultValue(PointDisplayType.Exact)]
    public PointDisplayType DisplayType { get; set; } = PointDisplayType.Exact;

    [DisplayName("Dynamic Circle Size")]
    [Description("If enabled, the size of the social stat circles will increase as you gain points.")]
    [DefaultValue(true)]
    public bool ExpandCircles { get; set; } = true;

    [DisplayName("Display Extra Points")]
    [Description("If enabled, when you are at max level any extra points you have will be displayed.\nThis is not effected by Point Display Type")]
    [DefaultValue(true)]
    public bool DisplayExtra { get; set; } = true;

    [DisplayName("Debug Mode")]
    [Description("Logs additional information to the console that is useful for debugging.")]
    [DefaultValue(false)]
    public bool DebugEnabled { get; set; } = false;

    public enum PointDisplayType
    {
        None,
        Exact,
        Percent
    }

}

/// <summary>
/// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
/// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
/// </summary>
public class ConfiguratorMixin : ConfiguratorMixinBase
{
    // 
}