using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using REPOMod.Utils;
using REPOMod.Config;

namespace REPOMod
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInProcess("REPO.exe")]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;
        private readonly Harmony harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);

        private void Awake()
        {
            Log = Logger;
            Log.LogInfo($"[Map Expand] Plugin {MyPluginInfo.PLUGIN_NAME} v{MyPluginInfo.PLUGIN_VERSION} loaded!");
            
            try
            {
                ModConfig.Initialize(Config);
                
                PluginPersistence.SetupPersistenceMonitor();
                
                Log.LogInfo("[Map Expand] Map expansion mod initialized!");
            }
            catch (System.Exception ex)
            {
                Log.LogError($"[Map Expand] ERROR during initialization: {ex.Message}");
                Log.LogError($"[Map Expand] Stack trace: {ex.StackTrace}");
            }
        }
        
        private void OnDestroy()
        {
            Log.LogInfo($"[Map Expand] Plugin {MyPluginInfo.PLUGIN_NAME} unloaded!");
        }
    }
}
