using BepInEx;

namespace Boom;

[BepInPlugin("org.dual.boom", nameof(Boom), "0.1.0")]
public sealed class Plugin : BaseUnityPlugin
{
    // Entry point for the plugin.
    public void OnEnable()
    {
        _ = new Hooks();
    }
}
