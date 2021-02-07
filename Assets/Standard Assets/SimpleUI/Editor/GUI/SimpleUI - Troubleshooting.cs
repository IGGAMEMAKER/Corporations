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
    public struct UrlOpeningAttempt
    {
        public string PreviousUrl;
    }

    // Troubleshooting
    public partial class SimpleUIEditor
    {
        static Dictionary<string, List<UrlOpeningAttempt>> UrlOpeningAttempts => SimpleUI.UrlOpeningAttempts;

        void RenderExistingTroubles()
        {
            if (prefabs.Count == 0)
                return;

            if (GUILayout.Button("Find missing assets"))
            {
                SimpleUI.FindMissingAssets();
            }

            RenderMissingUrls();
            RenderMissingAssets();
        }

        void RenderMissingUrls()
        {
            if (UrlOpeningAttempts.Any())
                Label("Failed to open these URLs from code");

            Space();
            foreach (var missing in UrlOpeningAttempts)
            {
                var scripts = string.Join("\n", missing.Value.Select(m => m.PreviousUrl));

                EditorGUILayout.HelpBox($"Tried to open url <b>{missing.Key}</b> from, but failed", MessageType.Error, true);
                EditorGUILayout.HelpBox($"Did that from {scripts}", MessageType.Warning);
            }

            if (UrlOpeningAttempts.Any())
            {
                Space();
                if (Button("Dismiss warnings"))
                {
                    UrlOpeningAttempts.Clear();
                    SaveData();
                }
            }
        }

        void RenderMissingAssets()
        {
            Space();
            var missingAssets = prefabs.FindAll(p => !p.Exists);

            if (missingAssets.Any())
                Label("These urls are missing assets");

            foreach (var missing in missingAssets)
            {
                EditorGUILayout.HelpBox($"Url {missing.Url} is missing an asset {missing.AssetPath}", MessageType.Error, true);
                EditorGUILayout.HelpBox($"You can fix the link to the asset or move the asset to path {missing.AssetPath}", MessageType.Info, true);

                if (GUILayout.Button("Fix the issue"))
                {
                    isUrlEditingMode = true;
                    OpenPrefab(missing);
                }
            }
        }
    }
}