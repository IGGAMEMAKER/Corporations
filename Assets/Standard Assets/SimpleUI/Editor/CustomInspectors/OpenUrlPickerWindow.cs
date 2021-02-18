using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SimpleUI
{
    public class OpenUrlPickerWindow : EditorWindow
    {
        public OpenUrl OpenUrl;

        static int _choiceIndex = 0;
        static Vector2 scroll = Vector2.zero;

        string previousUrl;

        List<SimpleUISceneType> prefabs; // => SimpleUI.prefabs;
        string[] _choices; // => prefabs.Select(p => MakeProperUrl(p.Url)).ToArray();
        static string searchUrl = "";

        // = { "foo", "foobar" };

        public void SetOpenUrl(OpenUrl openUrl)
        {
            OpenUrl = openUrl;

            prefabs = SimpleUI.GetPrefabsFromFile();
            _choices = prefabs.Select(p => MakeProperUrl(p.Url)).ToArray();

            previousUrl = openUrl.Url;
        }

        private void OnGUI()
        {
            if (OpenUrl == null)
                return;

            bool changed = !previousUrl.Equals(OpenUrl.Url);

            var changedLabel = changed ? "(changed)" : "";
            GUILayout.Label($"Current url is {changedLabel}");
            GUILayout.Label($"{previousUrl}", EditorStyles.boldLabel);


            if (changed)
            {
                GUILayout.Space(15);
                if (GUILayout.Button("Restore url"))
                {
                    ChangeUrl(previousUrl, false);
                }
            }

            GUILayout.Space(15);
            GUILayout.Label("OR choose from list", EditorStyles.boldLabel);

            PickFromDropdown(OpenUrl);

            GUILayout.Space(15);
            GUILayout.Label("OR Choose from RECENTLY added prefabs", EditorStyles.boldLabel);

            PickRecentUrls(OpenUrl);
        }

        void PickFromDropdown(OpenUrl openUrl)
        {
            _choiceIndex = Array.IndexOf(_choices, openUrl.Url);

            // If the choice is not in the array then the _choiceIndex will be -1 so set back to 0
            if (_choiceIndex < 0)
                _choiceIndex = 0;

            var prevIndex = _choiceIndex;
            _choiceIndex = EditorGUILayout.Popup(_choiceIndex, _choices);

            if (prevIndex != _choiceIndex)
            {
                ChangeUrl(_choices[_choiceIndex]);
            }
        }

        void ChangeUrl(string newUrl, bool forceDirty = true)
        {
            OpenUrl.Url = MakeProperUrl(newUrl);

            // Save the changes back to the object
            if (forceDirty)
                EditorUtility.SetDirty(OpenUrl);
            else
                EditorUtility.ClearDirty(OpenUrl);
        }

        void PickRecentUrls(OpenUrl openUrl)
        {
            GUIStyle style = GUI.skin.FindStyle("Button");
            style.richText = true;

            scroll = EditorGUILayout.BeginScrollView(scroll);

            var sortedByOpenings = prefabs.OrderByDescending(pp => pp.LastOpened);
            var recent = sortedByOpenings.Take(10);

            //var recent = prefabs.OrderByDescending(pp => pp.LastOpened).Take(15);

            GUILayout.Space(15);
            GUILayout.Label("Recent prefabs", EditorStyles.boldLabel);
            searchUrl = EditorGUILayout.TextField("Search", searchUrl);
            GUILayout.Space(15);

            if (searchUrl.Length == 0)
            {
                foreach (var r in recent)
                {
                    if (GUILayout.Button($"<b>{r.Name}</b>\n{r.Url}", style))
                    {
                        ChangeUrl(r.Url);
                    }
                }
            }

            if (searchUrl.Length != 0)
            {
                if (GUILayout.Button("Clear"))
                {
                    searchUrl = "";
                    Repaint();
                }

                GUILayout.Space(15);
                foreach (var r in sortedByOpenings.Where(p => SimpleUI.Contains(p.Url, searchUrl) || SimpleUI.Contains(p.Name, searchUrl)))
                {
                    if (GUILayout.Button($"<b>{r.Name}</b>\n{r.Url}", style))
                    {
                        ChangeUrl(r.Url);
                    }
                }
            }

            EditorGUILayout.EndScrollView();
        }

        static string MakeProperUrl(string url) => url.Trim('/');
    }
}