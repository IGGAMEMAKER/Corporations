using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Assets.Core;
using UnityEngine;


using UnityEditor;
using UnityEngine.SceneManagement;

public enum SceneBlahType
{
    Prefab,
    Scene
}

public struct NewSceneTypeBlah
{
    public string Url;
    public string Name;
    public string AssetPath;

    public SceneBlahType SceneBlahType;
}

[ExecuteAlways]
public class UnityUiNavigation : View
{
    private void OnGUI()
    {
        var style = new GUIStyle();

        var leftCorner = 0;
        var rightCorner = 1920;

        var top = 0;
        var bottom = 1080;

        var centerX = (leftCorner + rightCorner) / 2;
        var centerY = (top + bottom) / 2;

        style.fontSize = 40;
        
        GUI.Label(new Rect(centerX, top, 100, 30), Visuals.Positive("Text"), style);

        var prefabs = new List<NewSceneTypeBlah>();
        
        prefabs.Add(new NewSceneTypeBlah { Name = "Holding screen", Url = "/Main", AssetPath = "HoldingScreen.prefab", SceneBlahType = SceneBlahType.Prefab });
        prefabs.Add(new NewSceneTypeBlah { Name = "Acquisition screen", Url = "/Acquisitions", AssetPath = "AcquisitionScreen.prefab", SceneBlahType = SceneBlahType.Prefab });

        for (var i = 0; i < prefabs.Count; i++)
        {
            var p = prefabs[i];
            
            if (GUI.Button(new Rect(rightCorner - 300, top + i * 90, 300, 80), p.Name))
            {
                Debug.Log("Pressed " + p.Name);
                
                // PlaySound(Sound.Bubble1);
            }
        }
    }
}


public class MyWindow : EditorWindow
{
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    public int Width = 400;
    public int Height = 400;
    
    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/UI-NAVIGATION")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(MyWindow));
    }
    
    void OnGUI()
    {
        // GUILayout.Width(Width);
        // GUILayout.Height(Height);
        
        GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField ("Text Field", myString);
        
        // groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", groupEnabled);
        // myBool = EditorGUILayout.Toggle ("Toggle", myBool);
        // myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
        
        // -----------
        var style = new GUIStyle();

        var leftCorner = 0;
        var rightCorner = 1920;

        var top = 0;
        var bottom = 1080;

        var centerX = (leftCorner + rightCorner) / 2;
        var centerY = (top + bottom) / 2;

        style.fontSize = 40;
        
        // GUI.Label(new Rect(centerX, top, 100, 30), Visuals.Positive("Text"), style);

        var prefabs = new List<NewSceneTypeBlah>();
        
        prefabs.Add(new NewSceneTypeBlah { Name = "Holding screen", Url = "/Main", AssetPath = "Assets/_Screens/Main Screens/HoldingScreen___.prefab", SceneBlahType = SceneBlahType.Prefab });
        // prefabs.Add(new NewSceneTypeBlah { Name = "Acquisition screen", Url = "/Acquisitions", AssetPath = "AcquisitionScreen.prefab", SceneBlahType = SceneBlahType.Prefab });

        GUILayout.Space(15);
        GUILayout.Label ("Favorite prefabs", EditorStyles.boldLabel);

        for (var i = 0; i < prefabs.Count; i++)
        {
            var p = prefabs[i];
            
            if (GUILayout.Button($"{p.Url}  - {p.Name}"))
            // if (GUI.Button(new Rect(rightCorner - 300, top + i * 90, 300, 80), p.Name))
            {
                Debug.Log("Pressed " + p.Name);

                Selection.activeObject = Resources.Load<GameObject>(p.AssetPath);
                
                // PrefabUtility.LoadPrefabContentsIntoPreviewScene(p.AssetPath, SceneManager.GetActiveScene()); // .LoadPrefabContents(p.AssetPath);
                // PrefabUtility.LoadPrefabContents(p.AssetPath);
            }
        }
        
        // EditorGUILayout.EndToggleGroup ();
    }
}