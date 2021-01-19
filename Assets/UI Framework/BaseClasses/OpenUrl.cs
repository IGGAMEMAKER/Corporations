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
    // [HideInInspector]
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

    //private void OnGUI()
    //{
    //    Handles.Button(transform.position, Quaternion.identity, 50, 50, Handles.RectangleHandleCap);
    //}

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(transform.position, Vector3.one * 20);
    }
}

[CustomEditor(typeof(OpenUrl))]
public class UrlPickerEditor : Editor
{
    static string[] _choices; // = { "foo", "foobar" };
    static int _choiceIndex = 0;

    static Vector2 scroll = Vector2.zero;

    static List<SimpleUISceneType> prefabs;

    public override void OnInspectorGUI ()
    {
        GUILayout.Space(15);
        GUILayout.Label("Specify URL manually (WORST scenario)", EditorStyles.boldLabel);
        // Draw the default inspector
        DrawDefaultInspector();
        
        var someClass = target as OpenUrl;

        var prevValue = someClass.Url;
        _choiceIndex = Array.IndexOf(_choices, prevValue);
 
        // If the choice is not in the array then the _choiceIndex will be -1 so set back to 0
        if (_choiceIndex < 0)
            _choiceIndex = 0;
        
        //EditorGUILayout.Separator();
        GUILayout.Space(15);
        GUILayout.Label("OR choose from list", EditorStyles.boldLabel);
        _choiceIndex = EditorGUILayout.Popup(_choiceIndex, _choices);

        // Update the selected choice in the underlying object
        someClass.Url = _choices[_choiceIndex];
        
        if (!prevValue.Equals(someClass.Url))
        {
            // Save the changes back to the object
            EditorUtility.SetDirty(target);
        }

        var sortedByOpenings = prefabs.OrderByDescending(pp => pp.LastOpened);
        var recent = sortedByOpenings.Take(15);

        GUILayout.Space(15);
        GUILayout.Label("OR Choose from RECENTLY added prefabs", EditorStyles.boldLabel);

        GUIStyle style = GUI.skin.FindStyle("Button");
        style.richText = true;

        scroll = EditorGUILayout.BeginScrollView(scroll);
        foreach (var r in recent)
        {
            if (GUILayout.Button($"<b>{r.Name}</b>\n", style)) // \n{r.Url}
            {
                someClass.Url = MakeProperUrl(r.Url);
            }
        }

        //var obj = (target as OpenUrl);
        //Handles.Button(obj.transform.position, Quaternion.identity, 50, 50, Handles.RectangleHandleCap);

        EditorGUILayout.EndScrollView();
    }

    static string MakeProperUrl(string url) => url.Trim('/');

    private void OnEnable()
    {
        LoadData();
    }

    static void LoadData()
    {
        var fileName = "SimpleUI/SimpleUI.txt";

        List<SimpleUISceneType> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SimpleUISceneType>>(
            File.ReadAllText(fileName), new Newtonsoft.Json.JsonSerializerSettings
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            });

        prefabs = obj ?? new List<SimpleUISceneType>();

        _choices = prefabs.Select(p => MakeProperUrl(p.Url)).ToArray();
    }
}

[CustomEditor(typeof(ProxyToUrl))]
public class ProxyUrlPickerEditor : Editor
{
    static string[] _choices; // = { "foo", "foobar" };
    static int _choiceIndex = 0;
 
    public override void OnInspectorGUI ()
    {
        // Draw the default inspector
        DrawDefaultInspector();
        
        var someClass = target as ProxyToUrl;

        var prevValue = someClass.Url;
        _choiceIndex = Array.IndexOf(_choices, prevValue);
 
        // If the choice is not in the array then the _choiceIndex will be -1 so set back to 0
        if (_choiceIndex < 0)
            _choiceIndex = 0;
        
        EditorGUILayout.Separator();
        _choiceIndex = EditorGUILayout.Popup("Choose Url", _choiceIndex, _choices);

        // Update the selected choice in the underlying object
        someClass.Url = _choices[_choiceIndex];
        
        if (!prevValue.Equals(someClass.Url))
        {
            // Save the changes back to the object
            EditorUtility.SetDirty(target);
        }
    }

    private void OnEnable()
    {
        LoadData();
    }

    static void LoadData()
    {
        var fileName = "SimpleUI/SimpleUI.txt";

        List<SimpleUISceneType> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SimpleUISceneType>>(
            File.ReadAllText(fileName), new Newtonsoft.Json.JsonSerializerSettings
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            });

        var prefabs = obj ?? new List<SimpleUISceneType>();

        _choices = prefabs.Select(p => p.Url.Trim('/')).ToArray();
    }
}
