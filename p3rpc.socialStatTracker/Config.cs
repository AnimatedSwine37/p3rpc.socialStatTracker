using p3rpc.socialStatTracker.Template.Configuration;
using System.ComponentModel;

namespace p3rpc.socialStatTracker.Configuration;
public class Config : Configurable<Config>
{
    [DisplayName("Use Percentage")]
    [Description("If enabled you'll see the percentage of the way you are to the next level instead of the number of points.")]
    [DefaultValue(false)]
    public bool DisplayPercentage { get; set; } = false;

    [DisplayName("Debug Mode")]
    [Description("Logs additional information to the console that is useful for debugging.")]
    [DefaultValue(false)]
    public bool DebugEnabled { get; set; } = false;

    [DisplayName("X")]
    [DefaultValue(100)]
    public float X { get; set; } = 100;

    [DisplayName("Y")]
    [DefaultValue(100)]
    public float Y { get; set; } = 100;

}

/// <summary>
/// Allows you to override certain aspects of the configuration creation process (e.g. create multiple configurations).
/// Override elements in <see cref="ConfiguratorMixinBase"/> for finer control.
/// </summary>
public class ConfiguratorMixin : ConfiguratorMixinBase
{
    // 
}