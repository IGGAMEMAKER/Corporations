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
        if (Url.StartsWith("/"))
            OpenUrl(Url);
        else
            OpenUrl("/" + Url);
    }
}

[CustomEditor(typeof(OpenUrl))]
public class UrlPickerEditor : Editor
{
    static string[] _choices; // = { "foo", "foobar" };
    static int _choiceIndex = 0;
 
    public override void OnInspectorGUI ()
    {
        // Draw the default inspector
        DrawDefaultInspector();
        
        PrintChoices(_choices);
        
        var someClass = target as OpenUrl;

        var prevValue = someClass.Url;
        _choiceIndex = Array.IndexOf(_choices, prevValue);
 
        // If the choice is not in the array then the _choiceIndex will be -1 so set back to 0
        if (_choiceIndex < 0)
            _choiceIndex = 0;
        
        EditorGUILayout.Separator();
        _choiceIndex = EditorGUILayout.Popup(_choiceIndex, _choices);

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
        Debug.Log("OnEnable");
        
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

        var x = prefabs.Select(p => p.Url.Trim('/')).ToArray();
        PrintChoices(x);

        _choices = new [] { "foo", "foobar" };
        _choices = x;
        
        PrintChoices(_choices);
    }

    static void PrintChoices(string[] ch)
    {
        // Debug.Log(string.Join("\n", ch));
    }
}
