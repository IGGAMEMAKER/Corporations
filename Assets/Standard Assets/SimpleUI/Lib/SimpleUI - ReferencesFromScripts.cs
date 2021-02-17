using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace SimpleUI
{
    // what uses component OpenUrl
    public partial class SimpleUI
    {
        public struct UsageInfo
        {
            public string ScriptName;
        }

        public List<UsageInfo> WhichScriptReferencesConcreteUrl(string url)
        {
            // Debug.Log("Finding all scrips, that call " + url);

            var list = new List<UsageInfo>();

            //foreach (var path in GetAllScriptPaths())
            foreach (var script in allScripts)
            {
                //var asset = AssetDatabase.LoadAssetAtPath<MonoScript>(path);
                var asset = script.Value;
                var path = script.Key;


                var txt = asset != null ? ("\n" + asset.text) : "";

                //if (asset == null)
                //{
                //    Debug.LogError("Cannot load prefab at path: " + path);

                //    continue;
                //}

                if (IsTextContainsUrl(txt, url, true))
                {
                    // Debug.Log($"Found url {url} in text " + path);

                    list.Add(new UsageInfo { ScriptName = path });
                }
            }

            return list;
        }


        /// <summary>
        /// returns array of matching indicies
        /// </summary>
        /// <param name="text"></param>
        /// <param name="url"></param>
        /// <param name="directMatch"></param>
        /// <returns></returns>
        static bool IsTextContainsUrl(string text, string url, bool directMatch)
        {
            //bool directMatch = true;
            var searchString = '"' + url;

            if (directMatch)
                searchString += '"';

            return text.Contains(searchString);
        }

        static Dictionary<string, MonoScript> GetAllScripts()
        {
            var dict = new Dictionary<string, MonoScript>();

            foreach (var path in GetAllScriptPaths())
            {
                dict[path] = AssetDatabase.LoadAssetAtPath<MonoScript>(path);
            }

            return dict;
        }

        static List<string> GetAllScriptPaths()
        {
            var directory = "Assets/";

            var excludeFolders = new[] { "Assets/Standard Assets/Frost UI", "Assets/Standard Assets/SimpleUI", "Assets/Standard Assets/Libraries", "Assets/Systems", "Assets/Core" };
            var guids = AssetDatabase.FindAssets("t:Script", new[] { "Assets" });

            //Debug.Log($"Found {guids.Length} scripts");
            var paths = guids.Select(AssetDatabase.GUIDToAssetPath).ToList();

            paths.RemoveAll(guid => excludeFolders.Any(guid.Contains));

            return paths;
        }

    }
}