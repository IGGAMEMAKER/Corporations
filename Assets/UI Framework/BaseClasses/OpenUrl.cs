using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OpenUrl : ButtonController
{
    public string Url;

    public override void Execute()
    {
        var newUrl = "";
        if (Url.StartsWith("/"))
            newUrl = Url;
        else
            newUrl = "/" + Url;
        
        OpenUrl(newUrl);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(transform.position, Vector3.one * 20);
    }
}

[CustomEditor(typeof(OpenUrl))]
public class UrlPickerEditor : Editor
{
    static int _choiceIndex = 0;

    static Vector2 scroll = Vector2.zero;

    static List<SimpleUISceneType> prefabs => SimpleUI.prefabs;
    static string[] _choices => prefabs.Select(p => MakeProperUrl(p.Url)).ToArray();
    // = { "foo", "foobar" };

    public override void OnInspectorGUI ()
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

    static string MakeProperUrl(string url) => url.Trim('/');
}
