using UnityEngine;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Omnilatent.Utils.EditorNS
{
    public static class EditorUtility
    {
        private const string PackageName = "com.omnilatent.utils"; // Change this to your package name
        private const string ManifestPath = "Packages/manifest.json";

        [MenuItem("Tools/Omnilatent/Enable Utility Tests")]
        public static void AddPackageToTestables()
        {
            if (!File.Exists(ManifestPath))
            {
                Debug.LogError("manifest.json not found at " + ManifestPath);
                return;
            }

            string json = File.ReadAllText(ManifestPath);
            var manifest = JObject.Parse(json);

            JArray testables = manifest["testables"] as JArray;
            if (testables == null)
            {
                testables = new JArray();
                manifest["testables"] = testables;
            }

            if (!testables.Contains(PackageName))
            {
                testables.Add(PackageName);
                File.WriteAllText(ManifestPath, manifest.ToString());
                Debug.Log($"Added '{PackageName}' to testables in manifest.json");
                AssetDatabase.Refresh();
            }
            else
            {
                Debug.Log($"'{PackageName}' is already in testables");
            }
        }
    }
}