using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets;
using Assets.Core;
using Entitas;
using UnityEngine;


using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine.SceneManagement;

// https://answers.unity.com/questions/37180/how-to-highlight-or-select-an-asset-in-project-win.html


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
    private static string newName = "";
    private static string newPath = "";
    
    List<NewSceneTypeBlah> prefabs; // = new List<NewSceneTypeBlah>();

    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/UI-NAVIGATION")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(MyWindow));
    }
    
    static MyWindow()
    {
        PrefabStage.prefabStageOpened += PrefabStage_prefabSaved;
    }

    private static void PrefabStage_prefabSaved(PrefabStage obj)
    {
        Debug.Log("Prefab opened: " + obj.prefabContentsRoot.name);

        newPath = obj.prefabAssetPath;
        newName = obj.prefabAssetPath;
    }

    // static void PrefabStage_prefabSaved(GameObject obj)
    // {
    //     Debug.Log("Prefab edited: " + obj.name);
    //
    //     if (Application.isPlaying)
    //     {
    //         ScheduleUtils.PauseGame(Contexts.sharedInstance.game);
    //
    //         //SceneManager.UnloadScene(1);
    //         //Task.Run(() => SceneManager.UnloadSceneAsync(1));
    //         SceneManager.UnloadScene(1);
    //         //State.LoadGameScene();
    //         SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    //
    //
    //         //ScreenUtils.UpdateScreen(Contexts.sharedInstance.game);
    //     }
    // }
    
    void OnGUI()
    {
        GUILayout.Label ("SIMPLE UI", EditorStyles.largeLabel);
        GUILayout.Space(10);
        // myString = EditorGUILayout.TextField ("Text Field", myString);
        
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

        newUrl = EditorGUILayout.TextField("Url", newUrl);

        if (newUrl.Length > 0)
        {
            newName = EditorGUILayout.TextField("Name", newName);

            if (newName.Length > 0)
            {
                newPath = EditorGUILayout.TextField("Asset Path", newPath);

                if (newPath.Length > 0)
                {
                    GUILayout.Space(15);
                    if (GUILayout.Button("Add prefab!")) //  <" + newName + ">
                    {
                        Debug.Log("Added prefab");
                    
                        AddNewRoute();
                    }
                }
            }
        }
    }

    void AddNewRoute()
    {
        prefabs.Add(new NewSceneTypeBlah(SceneBlahType.Prefab, newUrl, newPath, newName));

        SaveData();
    }

    void RenderPrefabs()
    {
        LoadData();
        
        GUILayout.Space(15);
        GUILayout.Label ("Favorite prefabs", EditorStyles.boldLabel);

        for (var i = 0; i < prefabs.Count; i++)
        {
            var p = prefabs[i];
            
            if (GUILayout.Button($"{p.Name}   --- {p.Url}"))
            {
                Debug.Log("Pressed " + p.Name);

                Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(p.AssetPath);
            }
        }
    }
    
    void SaveData()
    {
        var fileName = "SimpleUI/SimpleUI.txt";

        Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
        serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
        serializer.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
        serializer.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
        serializer.Formatting = Newtonsoft.Json.Formatting.Indented;

        var entityData = prefabs; // new Dictionary<int, IComponent[]>();
        
        using (StreamWriter sw = new StreamWriter(fileName))
        using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw))
        {
            if (entityData.Count > 0)
            {
                Debug.Log("Serializing data " + entityData.Count);
                serializer.Serialize(writer, entityData);

                Debug.Log("Serialized " + entityData.Count);
            }
        }
    }

    void LoadData()
    {
        var fileName = "SimpleUI/SimpleUI.txt";

        List<NewSceneTypeBlah> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<NewSceneTypeBlah>>(File.ReadAllText(fileName), new Newtonsoft.Json.JsonSerializerSettings
        {
            TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
        });

        prefabs = obj;
        return;
        ;
        prefabs = new List<NewSceneTypeBlah>();
        
        prefabs.Add(new NewSceneTypeBlah(SceneBlahType.Prefab, "/Main", "Assets/_Screens/Main Screens/HoldingScreen___.prefab", "Holding screen"));
        // prefabs.Add(new NewSceneTypeBlah { Name = "Holding screen", Url = "/Main", AssetPath = "Assets/_Screens/Main Screens/HoldingScreen___.prefab", SceneBlahType = SceneBlahType.Prefab });
        prefabs.Add(new NewSceneTypeBlah(SceneBlahType.Prefab, "/Project", "Assets/_Screens/Main Screens/ProjectScreen.prefab", "Project screen"));
        // prefabs.Add(new NewSceneTypeBlah { Name = "Project screen", Url = "/Project", AssetPath = "Assets/_Screens/Main Screens/ProjectScreen.prefab", SceneBlahType = SceneBlahType.Prefab });
        // prefabs.Add(new NewSceneTypeBlah { Name = "Acquisition screen", Url = "/Acquisitions", AssetPath = "AcquisitionScreen.prefab", SceneBlahType = SceneBlahType.Prefab });
    }
}