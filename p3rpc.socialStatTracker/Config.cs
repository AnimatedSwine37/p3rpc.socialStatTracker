using p3rpc.socialStatTracker.Template.Configuration;
using System.ComponentModel;

namespace p3rpc.socialStatTracker.Configuration;
public class Config : Configurable<Config>
{
    [DisplayName("Point Display Type")]
    [Description("How the number of points you need will be displayed.")]
    [DefaultValue(PointDisplayType.Exact)]
    public PointDisplayType DisplayType { get; set; } = PointDisplayType.Exact;

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