using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public static class EditorTools 
    {
        [MenuItem("My Menu/Find Missing Scripts")]
        static void FindMissingScriptsInProjectFromMenu()
        {
            string[] prefabPaths = AssetDatabase.GetAllAssetPaths()
                .Where(path => path.EndsWith(".prefab", System.StringComparison.OrdinalIgnoreCase)).ToArray();

            foreach (string path in prefabPaths)
            {
                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

                foreach (Component component in prefab.GetComponentsInChildren<Component>())
                {
                    if (component == null)
                    {
                        Debug.Log("Prefab found with missing script " + path, prefab);
                        break;
                    }
                }
            }
        }
    }
}
