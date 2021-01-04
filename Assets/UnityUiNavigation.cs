using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

public struct SimpleUISceneType
{
    public string Url;
    public string Name;
    public string AssetPath;
    
    public long Usages;
    public long LastOpened;

    public SceneBlahType SceneBlahType;

    public SimpleUISceneType(string url, string assetPath, string name = "")
    {
        SceneBlahType = SceneBlahType.Prefab;

        if (assetPath.EndsWith(".scene"))
            SceneBlahType = SceneBlahType.Scene;
        
        Url = url;
        AssetPath = assetPath;
        Name = name.Length > 0 ? name : url;

        Usages = 0;
        LastOpened = 0;
    }
}
//
// [ExecuteAlways]
// public class UnityUiNavigation : View
// {
//     private void OnGUI()
//     {
//         var style = new GUIStyle();
//
//         var leftCorner = 0;
//         var rightCorner = 1920;
//
//         var top = 0;
//         var bottom = 1080;
//
//         var centerX = (leftCorner + rightCorner) / 2;
//         var centerY = (top + bottom) / 2;
//
//         style.fontSize = 40;
//         
//         GUI.Label(new Rect(centerX, top, 100, 30), Visuals.Positive("Text"), style);
//
//         var prefabs = new List<NewSceneTypeBlah>();
//         
//         prefabs.Add(new NewSceneTypeBlah { Name = "Holding screen", Url = "/Main", AssetPath = "HoldingScreen.prefab" });
//         prefabs.Add(new NewSceneTypeBlah { Name = "Acquisition screen", Url = "/Acquisitions", AssetPath = "AcquisitionScreen.prefab", SceneBlahType = SceneBlahType.Prefab });
//
//         for (var i = 0; i < prefabs.Count; i++)
//         {
//             var p = prefabs[i];
//             
//             if (GUI.Button(new Rect(rightCorner - 300, top + i * 90, 300, 80), p.Name))
//             {
//                 Debug.Log("Pressed " + p.Name);
//                 
//                 // PlaySound(Sound.Bubble1);
//             }
//         }
//     }
// }


public class MyWindow : EditorWindow
{
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    private static string newUrl = "";
    private static string newName = "";
    private static string newPath = "";
    
    static List<SimpleUISceneType> prefabs; // = new List<NewSceneTypeBlah>();

    private static int ChosenIndex => prefabs.FindIndex(p => p.AssetPath.Equals(newPath));
    private static bool hasChosenPrefab => ChosenIndex >= 0;

    private bool showTopPrefabs = false;
    private bool showRecentPrefabs = false;
    private bool showAllPrefabs = false;
    
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

    // void OnDidOpenScene()
    // {
    //     ShowWindow();
    // }

    private static void PrefabStage_prefabSaved(PrefabStage obj)
    {
        Debug.Log("Prefab opened: " + obj.prefabContentsRoot.name);

        newPath = obj.prefabAssetPath;

        var x = newPath.Split('/').Last();
        var ind = x.LastIndexOf(".prefab");
         
        newName = x.Substring(0, ind);;


        TryToIncreaseCurrentPrefabCounter();
    }

    void OnGUI()
    {
        GUILayout.BeginScrollView(new Vector2(0, 0));
        GUILayout.Label ("SIMPLE UI", EditorStyles.largeLabel);

        RenderPrefabs();

        if (hasChosenPrefab)
            RenderEditingPrefab();
        else
            RenderAddingNewRoute();
        
        GUILayout.EndScrollView();
    }

    void RenderChooseMode()
    {
        
    }

    void RenderExample()
    {
        // myString = EditorGUILayout.TextField ("Text Field", myString);
        
        // groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", groupEnabled);
        // myBool = EditorGUILayout.Toggle ("Toggle", myBool);
        // myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
        // EditorGUILayout.EndToggleGroup ();
    }

    void RenderEditingPrefab()
    {
        var index = ChosenIndex;
        var pref = prefabs[index];

        Space();
        GUILayout.Label(pref.Url, EditorStyles.boldLabel);

        Space();
        if (GUILayout.Button("Remove URL"))
        {
            prefabs.RemoveAt(index);
            SaveData();
            
            return;
        }

        newUrl = pref.Url;
        newPath = pref.AssetPath;
        newName = pref.Name;

        var prevUrl = newUrl;
        var prevName = newName;
        var prevPath = newPath;
        
        Space();

        newUrl = EditorGUILayout.TextField("Url", newUrl);

        if (newUrl.Length > 0)
        {
            newName = EditorGUILayout.TextField("Name", newName);

            if (newName.Length > 0)
            {
                newPath = EditorGUILayout.TextField("Asset Path", newPath);

                if (newPath.Length > 0)
                {
                }
            }
        }
        
        pref.Url = newUrl;
        pref.Name = newName;
        pref.AssetPath = newPath;

        // if data changed, save it
        if (!prevUrl.Equals(newUrl) || !prevPath.Equals(newPath) || !prevName.Equals(newName))
        {
            UpdatePrefab(pref);
        }
        
        Space();
        if (pref.Usages > 0 && GUILayout.Button("Reset Counter"))
        {
            pref.Usages = 0;

            UpdatePrefab(pref);
        }

        if (GUILayout.Button("Prioritize"))
        {
            pref.Usages = prefabs.Max(p => p.Usages) + 100;
            
            UpdatePrefab(pref);
        }
    }

    void RenderAddingNewRoute()
    {
        Space();
        GUILayout.Label("Add current prefab", EditorStyles.boldLabel);

        newUrl = EditorGUILayout.TextField("Url", newUrl);

        if (newUrl.Length > 0)
        {
            if (!newUrl.StartsWith("/"))
                newUrl = newUrl.Insert(0, "/");
            
            newName = EditorGUILayout.TextField("Name", newName);

            if (newName.Length > 0)
            {
                newPath = EditorGUILayout.TextField("Asset Path", newPath);
                
                if (newPath.Length > 0)
                {
                    Space();
                    if (GUILayout.Button("Add prefab!")) //  <" + newName + ">
                    {
                        Debug.Log("Added prefab");
                        
                        prefabs.Add(new SimpleUISceneType(newUrl, newPath, newName));

                        SaveData();
                    }
                }
            }
        }
    }

    void RenderPrefabs(IEnumerable<SimpleUISceneType> list)
    {
        // prefabs.OrderByDescending(pp => pp.Usages).Take(7)
        foreach (var p in list)
        {
            var c = GUI.color;

            bool isChosen = hasChosenPrefab && prefabs[ChosenIndex].AssetPath.Equals(p.AssetPath);
            var color = Visuals.GetColorFromString(isChosen ? Colors.COLOR_YOU : Colors.COLOR_NEUTRAL);
            GUI.contentColor = color;
            GUI.color = color;
            GUI.backgroundColor = color;
            
            
            if (GUILayout.Button(p.Name))
            // if (GUILayout.Button($"{p.Name}   ---   {p.Url}"))
            {
                Debug.Log("Pressed " + p.Name);

                newPath = p.AssetPath;

                var asset = AssetDatabase.LoadMainAssetAtPath(p.AssetPath);
                
                AssetDatabase.OpenAsset(asset);
                Selection.activeObject = asset;
                // AssetDatabase.OpenAsset(AssetDatabase.LoadMainAssetAtPath(p.AssetPath));
            }
            
            GUI.contentColor = c;
            GUI.color = c;
            GUI.backgroundColor = c;            
        }
    }

    void RenderRecentPrefabs()
    {
        var recent = prefabs.OrderByDescending(pp => pp.LastOpened).Take(7);

        GUILayout.Label ("Recent prefabs", EditorStyles.boldLabel);
        RenderPrefabs(recent);
    }

    void RenderFavoritePrefabs()
    {
        var top = prefabs.OrderByDescending(pp => pp.Usages).Take(5);

        GUILayout.Label ("Favorite prefabs", EditorStyles.boldLabel);
        RenderPrefabs(top);
    }

    void RenderAllPrefabs()
    {
        var top = prefabs.OrderByDescending(pp => pp.Url);

        GUILayout.Label ("All prefabs", EditorStyles.boldLabel);
        RenderPrefabs(top);
    }
    
    void RenderPrefabs()
    {
        LoadData();
        
        Space();
        
        RenderFavoritePrefabs();
        RenderRecentPrefabs();
    }
    
    
    // ----- utils -------------
    void Space(int space = 15)
    {
        GUILayout.Space(space);
    }
    
    static void TryToIncreaseCurrentPrefabCounter()
    {
        var index = ChosenIndex;

        if (hasChosenPrefab)
        {
            var pref = prefabs[index];

            pref.Usages++;
            pref.LastOpened = DateTime.Now.Ticks;

            UpdatePrefab(pref);
        }
    }
    
    static void SaveData()
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

    static void LoadData()
    {
        var fileName = "SimpleUI/SimpleUI.txt";

        List<SimpleUISceneType> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SimpleUISceneType>>(File.ReadAllText(fileName), new Newtonsoft.Json.JsonSerializerSettings
        {
            TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
        });

        prefabs = obj ?? new List<SimpleUISceneType>();
    }
    
    static void UpdatePrefab(SimpleUISceneType prefab)
    {
        if (!hasChosenPrefab)
            return;
        
        prefabs[ChosenIndex] = prefab;
        SaveData();
    }
}