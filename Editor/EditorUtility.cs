using UnityEngine;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Linq;

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

            string content = File.ReadAllText(ManifestPath);

            // Check if "testables" section exists
            if (!content.Contains("\"testables\""))
            {
                // Insert "testables": [] before the last closing brace
                int insertIndex = content.LastIndexOf('}');
                string toInsert = ",\n  \"testables\": [\n    \"" + PackageName + "\"\n  ]\n";
                content = content.Insert(insertIndex, toInsert);
                File.WriteAllText(ManifestPath, content);
                Debug.Log($"Added testables section with '{PackageName}'");
            }
            else
            {
                // If section exists, check if package is already there
                if (!content.Contains(PackageName))
                {
                    int arrayEnd = content.LastIndexOf(']');
                    string toInsert = (content[arrayEnd - 1] == '[' ? "" : ",") + "\n    \"" + PackageName + "\"";
                    content = content.Insert(arrayEnd, toInsert);
                    File.WriteAllText(ManifestPath, content);
                    Debug.Log($"Added '{PackageName}' to existing testables");
                }
                else
                {
                    Debug.Log($"'{PackageName}' already present in testables");
                }
            }

            AssetDatabase.Refresh();
        }
    }
}