using HarmonyLib;
using UnityEngine;
using System.Collections;
using System.Reflection;

namespace REPOMod.Patches
{
    public static class MapCameraPatch
    {
        private static GameObject patchObject;
        private static CameraResizer resizer;

        public static void ApplyMapCameraPatches(Harmony harmony)
        {
            patchObject = new GameObject("MapExpand_CameraResizer");
            Object.DontDestroyOnLoad(patchObject);
            
            resizer = patchObject.AddComponent<CameraResizer>();
            
            Plugin.Log.LogInfo("[Map Expand] Camera patch registered. Ready to expand map view!");
        }
        
        public static void ForceUpdateCamera()
        {
            if (resizer != null)
                resizer.ResizeCamerasInScene();
        }
    }

    public class CameraResizer : MonoBehaviour
    {
        private int processId;
        
        private void Awake()
        {
            processId = UnityEngine.Random.Range(1000, 9999);
            Plugin.Log.LogInfo($"[Map Expand] Camera resizer {processId} created");
            
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
        {
            Plugin.Log.LogInfo($"[Map Expand] Scene '{scene.name}' loaded - checking for map camera");
            
            StartCoroutine(ResizeCamerasAfterDelay());
        }
        
        private IEnumerator ResizeCamerasAfterDelay()
        {
            yield return new WaitForSeconds(0.5f);
            ResizeCamerasInScene();
        }
        
        public void ResizeCamerasInScene()
        {
            Plugin.Log.LogInfo($"[Map Expand] Resizing map cameras in current scene");
            
            Camera[] allCameras = Resources.FindObjectsOfTypeAll<Camera>();
            bool foundMapCamera = false;
            
            foreach (Camera cam in allCameras)
            {
                if (cam.gameObject.name.Contains("Dirt Finder Map Camera"))
                {
                    foundMapCamera = true;
                    ResizeCamera(cam);
                }
            }
            
            if (!foundMapCamera)
            {
                foreach (Camera cam in allCameras)
                {
                    string name = cam.gameObject.name.ToLower();
                    if ((name.Contains("map") && (name.Contains("camera") || name.Contains("cam"))) || 
                        (name.Contains("dirt") && name.Contains("finder")))
                    {
                        if (cam.orthographic)
                        {
                            ResizeCamera(cam);
                        }
                    }
                }
            }
        }
        
        private void ResizeCamera(Camera cam)
        {
            if (cam == null)
                return;
                
            float originalSize = cam.orthographicSize;
            float targetSize = REPOMod.Config.ModConfig.MapCameraSize.Value;
            
            Plugin.Log.LogInfo($"[Map Expand] Found map camera '{cam.gameObject.name}', size: {originalSize}, changing to: {targetSize}");
            
            cam.orthographicSize = targetSize;
            
            Plugin.Log.LogInfo($"[Map Expand] Camera size now: {cam.orthographicSize}");
        }

        private void OnDestroy()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
            Plugin.Log.LogInfo($"[Map Expand] Camera resizer {processId} destroyed");
        }
    }
}
