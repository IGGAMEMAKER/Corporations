using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleUI
{
    [CustomEditor(typeof(OpenUrl))]
    public class OpenUrlEditor : Editor
    {
        static int _choiceIndex = 0;

        static Vector2 scroll = Vector2.zero;

        List<SimpleUISceneType> prefabs => SimpleUI.instance.prefabs;
        string[] _choices => prefabs.Select(p => MakeProperUrl(p.Url)).ToArray();
        // = { "foo", "foobar" };

        public override void OnInspectorGUI()
        {
            GUILayout.Space(15);
            GUILayout.Label("Specify URL manually (NOT RECOMMENDED)", EditorStyles.boldLabel);

            DrawDefaultInspector();

            var openUrl = target as OpenUrl;

            GUILayout.Space(15);
            GUILayout.Label("OR choose from list", EditorStyles.boldLabel);

            PickFromDropdown(openUrl);

            GUILayout.Space(15);
            GUILayout.Label("OR Choose from RECENTLY added prefabs", EditorStyles.boldLabel);

            PickRecentUrls(openUrl);
        }

        void PickFromDropdown(OpenUrl openUrl)
        {
            var prevValue = openUrl.Url;
            _choiceIndex = Array.IndexOf(_choices, prevValue);

            // If the choice is not in the array then the _choiceIndex will be -1 so set back to 0
            if (_choiceIndex < 0)
                _choiceIndex = 0;


            _choiceIndex = EditorGUILayout.Popup(_choiceIndex, _choices);

            // Update the selected choice in the underlying object
            openUrl.Url = _choices[_choiceIndex];

            if (!prevValue.Equals(openUrl.Url))
            {
                // Save the changes back to the object
                EditorUtility.SetDirty(target);
            }
        }

        void PickRecentUrls(OpenUrl openUrl)
        {
            GUIStyle style = GUI.skin.FindStyle("Button");
            style.richText = true;

            scroll = EditorGUILayout.BeginScrollView(scroll);

            var recent = prefabs.OrderByDescending(pp => pp.LastOpened).Take(15);
            foreach (var r in recent)
            {
                if (GUILayout.Button($"<b>{r.Name}</b>\n", style))
                {
                    openUrl.Url = MakeProperUrl(r.Url);
                }
            }

            EditorGUILayout.EndScrollView();
        }

        private void OnSceneGUI()
        {
            Handles.BeginGUI();

            var openUrl = target as OpenUrl;
            var pos = openUrl.transform.position - new Vector3(0, -250, 0);

            Handles.Label(pos, SimpleUI.instance.GetPrettyNameForExistingUrl("/" + openUrl.Url)); // transform.position - new Vector3(0, -250, 0)

            if (GUILayout.Button(openUrl.Url))
            {
                SimpleUI.instance.OpenPrefabByUrl(openUrl.Url);
            }

            Handles.EndGUI();
        }

        static string MakeProperUrl(string url) => url.Trim('/');
    }
}