using BepInEx.Configuration;
using UnityEngine;

namespace REPOMod.Config
{
    public static class ModConfig
    {
        public static ConfigEntry<float> MapCameraSize;
        
        public static void Initialize(ConfigFile config)
        {
            MapCameraSize = config.Bind(
                "MapCamera",
                "Size",
                5.0f,
                "The size of the map camera. Higher values show more of the map. Game default: ~5");
                
            Plugin.Log.LogInfo($"[Map Expand] Configuration loaded. Camera size set to: {MapCameraSize.Value}");
        }
    }
}
