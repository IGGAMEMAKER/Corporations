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

    public NewSceneTypeBlah(SceneBlahType blahType, string url, string assetPath, string name = "")
    {
        SceneBlahType = blahType;
        Url = url;
        AssetPath = assetPath;
        Name = name.Length > 0 ? name : url;
    }
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
        
        prefabs.Add(new NewSceneTypeBlah { Name = "Holding screen", Url = "/Main", AssetPath = "HoldingScreen.prefab" });
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

    private string newUrl;
    private string newName;
    private string newRoute;

    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/UI-NAVIGATION")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(MyWindow));
    }
    
    void OnGUI()
    {
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

        RenderPrefabs();
        RenderAddingNewRoute();
        
        // EditorGUILayout.EndToggleGroup ();
    }

    void RenderAddingNewRoute()
    {
        GUILayout.Space(15);
        GUILayout.Label("Add current prefab", EditorStyles.boldLabel);

        newUrl = EditorGUILayout.TextField ("Url", newUrl);

        if (newUrl.Length > 0)
        {
            newName = EditorGUILayout.TextField ("Name", newName);

            if (GUILayout.Button("Add <" + newName + "> prefab!"))
            {
                Debug.Log("Added prefab");
            }   
        }
    }

    void RenderPrefabs()
    {
        var prefabs = new List<NewSceneTypeBlah>();
        
        prefabs.Add(new NewSceneTypeBlah(SceneBlahType.Prefab, "/Main", "Assets/_Screens/Main Screens/HoldingScreen___.prefab", "Holding screen"));
        // prefabs.Add(new NewSceneTypeBlah { Name = "Holding screen", Url = "/Main", AssetPath = "Assets/_Screens/Main Screens/HoldingScreen___.prefab", SceneBlahType = SceneBlahType.Prefab });
        prefabs.Add(new NewSceneTypeBlah(SceneBlahType.Prefab, "/Project", "Assets/_Screens/Main Screens/ProjectScreen.prefab", "Project screen"));
        // prefabs.Add(new NewSceneTypeBlah { Name = "Project screen", Url = "/Project", AssetPath = "Assets/_Screens/Main Screens/ProjectScreen.prefab", SceneBlahType = SceneBlahType.Prefab });
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

                Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(p.AssetPath);
                
                // Selection.activeObject = AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath(p.AssetPath, typeof(GameObject)));
                // Selection.activeObject.name += "!!!";
                // Selection.activeObject.name += "!!!";
                ; // = Resources.Load<GameObject>(p.AssetPath);
                
                // PrefabUtility.LoadPrefabContentsIntoPreviewScene(p.AssetPath, SceneManager.GetActiveScene()); // .LoadPrefabContents(p.AssetPath);
                // PrefabUtility.LoadPrefabContents(p.AssetPath);
            }
        }
    }
}