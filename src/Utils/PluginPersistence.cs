using BepInEx;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace REPOMod.Utils
{
    public static class PluginPersistence
    {
        private static GameObject persistentObject;
        
        public static void SetupPersistenceMonitor()
        {
            persistentObject = new GameObject("MapExpandBootstrapper");
            Object.DontDestroyOnLoad(persistentObject);
            
            var bootstrapper = persistentObject.AddComponent<MapExpandBootstrapper>();
            
            Plugin.Log.LogInfo("[Map Expand] Bootstrapper setup complete");
            
            SceneManager.sceneLoaded += OnSceneLoadedStatic;
        }
        
        private static void OnSceneLoadedStatic(Scene scene, LoadSceneMode mode)
        {
            Plugin.Log.LogInfo($"[Map Expand] Static scene load detected: {scene.name}");
            
            if (persistentObject == null)
            {
                persistentObject = new GameObject("MapExpandBootstrapper");
                Object.DontDestroyOnLoad(persistentObject);
                persistentObject.AddComponent<MapExpandBootstrapper>();
                Plugin.Log.LogInfo("[Map Expand] Recreated bootstrapper after unload");
            }
            
            if (scene.name == "Main")
            {
                var delayer = persistentObject.AddComponent<DelayedCameraProcessor>();
                delayer.SceneName = scene.name;
            }
        }
    }
    
    public class MapExpandBootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Plugin.Log.LogInfo("[Map Expand] Bootstrapper awakened");
            
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Plugin.Log.LogInfo($"[Map Expand] Bootstrapper detected scene: {scene.name}");
            
            if (scene.name == "Main")
            {
                Invoke("ProcessMapCamera", 2.0f);
            }
        }
        
        private void ProcessMapCamera()
        {
            try
            {
                Plugin.Log.LogInfo("[Map Expand] Attempting to resize map camera...");
                
                var cameras = Resources.FindObjectsOfTypeAll<Camera>();
                Plugin.Log.LogInfo($"[Map Expand] Found {cameras.Length} cameras");
                
                foreach (var cam in cameras)
                {
                    if (cam != null && cam.gameObject.name.Contains("Dirt Finder Map Camera"))
                    {
                        float oldSize = cam.orthographicSize;
                        float targetSize = 5f;
                        
                        try
                        {
                            if (REPOMod.Config.ModConfig.MapCameraSize != null)
                            {
                                targetSize = REPOMod.Config.ModConfig.MapCameraSize.Value;
                            }
                        }
                        catch (System.Exception)
                        {
                            Plugin.Log.LogWarning("[Map Expand] Couldn't access config, using default size 5");
                        }
                        
                        cam.orthographicSize = targetSize;
                        
                        Plugin.Log.LogInfo($"[Map Expand] Modified map camera size from {oldSize} to {cam.orthographicSize}");
                        return;
                    }
                }
                
                Plugin.Log.LogWarning("[Map Expand] Could not find Dirt Finder Map Camera");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Map Expand] Error processing camera: {ex.Message}");
            }
        }
        
        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Plugin.Log.LogInfo("[Map Expand] Bootstrapper destroyed");
        }
    }
    
    public class DelayedCameraProcessor : MonoBehaviour
    {
        public string SceneName;
        private float timer = 0f;
        private const float DELAY = 2.0f;
        private bool processed = false;
        
        private void Update()
        {
            if (processed) return;
            
            timer += Time.deltaTime;
            if (timer >= DELAY)
            {
                processed = true;
                ProcessMapCamera();
                Destroy(this);
            }
        }
        
        private void ProcessMapCamera()
        {
            try
            {
                Plugin.Log.LogInfo($"[Map Expand] Processing camera in scene {SceneName} after delay");
                
                var cameras = Resources.FindObjectsOfTypeAll<Camera>();
                
                foreach (var cam in cameras)
                {
                    if (cam != null && cam.gameObject.name.Contains("Dirt Finder Map Camera"))
                    {
                        float oldSize = cam.orthographicSize;
                        float targetSize = 5f;
                        
                        cam.orthographicSize = targetSize;
                        
                        Plugin.Log.LogInfo($"[Map Expand] Modified map camera size from {oldSize} to {cam.orthographicSize}");
                        return;
                    }
                }
                
                Plugin.Log.LogWarning("[Map Expand] Delayed processor could not find Dirt Finder Map Camera");
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"[Map Expand] Error in delayed processor: {ex.Message}");
            }
        }
    }
}
