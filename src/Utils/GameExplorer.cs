using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace REPOMod.Utils
{
    public static class GameExplorer
    {
        public static void LogGameAssemblies()
        {
            Plugin.Log.LogInfo("=== Assemblies loded ===");
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.FullName.StartsWith("System") && 
                           !a.FullName.StartsWith("Microsoft") && 
                           !a.FullName.StartsWith("mscorlib") &&
                           !a.FullName.StartsWith("Unity"));
                           
            foreach (var assembly in assemblies)
            {
                Plugin.Log.LogInfo($"Assembly: {assembly.FullName}");
            }
        }

        public static void LogGameNamespaces()
        {
            Plugin.Log.LogInfo("=== Namespaces ===");
            var gameAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(a => a.GetName().Name == "Assembly-CSharp");
                
            if (gameAssembly != null)
            {
                var namespaces = gameAssembly.GetTypes()
                    .Select(t => t.Namespace)
                    .Where(n => !string.IsNullOrEmpty(n))
                    .Distinct()
                    .OrderBy(n => n);
                    
                foreach (var ns in namespaces)
                {
                    Plugin.Log.LogInfo($"Namespace: {ns}");
                }
            }
            else
            {
                Plugin.Log.LogError("Assembly-CSharp non trouvée!");
            }
        }

        public static void LogMainClasses()
        {
            Plugin.Log.LogInfo("=== Classes principales ===");
            var gameAssembly = AppDomain.CurrentDomain.GetAssemblies()
                .FirstOrDefault(a => a.GetName().Name == "Assembly-CSharp");
                
            if (gameAssembly != null)
            {
                // Recherche de classes potentiellement importantes
                var keywordsList = new List<string> 
                { 
                    "Game", "Manager", "Player", "Map", "World", "Controller", 
                    "Camera", "UI", "HUD", "Menu", "Save", "Inventory" 
                };
                
                var mainClasses = gameAssembly.GetTypes()
                    .Where(t => keywordsList.Any(k => t.Name.Contains(k)))
                    .OrderBy(t => t.Name);
                    
                foreach (var cls in mainClasses)
                {
                    Plugin.Log.LogInfo($"Classe: {cls.FullName}");
                }
            }
        }
    }
}
