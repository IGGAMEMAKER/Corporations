using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleUI
{
    // https://answers.unity.com/questions/489942/how-to-make-a-readonly-property-in-inspector.html
    public class ReadOnlyAttribute : PropertyAttribute
    {

    }

    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property,
                                                GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position,
                                   SerializedProperty property,
                                   GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }

    [CustomEditor(typeof(OpenUrl))]
    public class OpenUrlEditor : Editor
    {
        List<SimpleUISceneType> prefabs;

        private void OnEnable()
        {
            prefabs = SimpleUI.GetPrefabsFromFile();
        }

        public override void OnInspectorGUI()
        {
            GUILayout.Space(15);
            GUILayout.Label("Specify URL manually (NOT RECOMMENDED)", EditorStyles.boldLabel);

            DrawDefaultInspector();

            var openUrl = target as OpenUrl;

            // link to url asset
            GUILayout.Space(15);
            if (Button($"<b>GO TO</b>\n{openUrl.Url}"))
            {
                OpenCurrentUrl();
            }

            // pick new url
            GUILayout.Space(15);
            if (Button("<b>Change url</b>\n"))
            {
                var w = EditorWindow.GetWindow<OpenUrlPickerWindow>();
                w.SetOpenUrl(openUrl);
            }
        }

        bool Button(string name)
        {
            GUIStyle style = GUI.skin.FindStyle("Button");
            style.richText = true;

            return GUILayout.Button(name, style);
        }

        void OpenCurrentUrl()
        {
            SimpleUI.GetInstance().OpenPrefabByUrl((target as OpenUrl).Url);
        }

        void OnSceneGUI()
        {
            Handles.BeginGUI();

            var openUrl = target as OpenUrl;
            var pos = openUrl.transform.position - new Vector3(0, -250, 0);

            Handles.Label(pos, SimpleUI.GetPrettyNameForExistingUrl("/" + openUrl.Url, prefabs)); // transform.position - new Vector3(0, -250, 0)

            if (GUILayout.Button(openUrl.Url))
            {
                OpenCurrentUrl();
            }

            Handles.EndGUI();
        }
    }
}