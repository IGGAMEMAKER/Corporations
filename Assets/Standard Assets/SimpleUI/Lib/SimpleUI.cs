﻿using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace SimpleUI
{
    // Read
    // Как-работает-editorwindow-ongui-в-unity-3d
    // https://ru.stackoverflow.com/questions/515395/%D0%9A%D0%B0%D0%BA-%D1%80%D0%B0%D0%B1%D0%BE%D1%82%D0%B0%D0%B5%D1%82-editorwindow-ongui-%D0%B2-unity-3d

    // https://answers.unity.com/questions/37180/how-to-highlight-or-select-an-asset-in-project-win.html
    // https://gist.github.com/rutcreate/0af3c34abd497a2bceed506f953308d7
    // https://stackoverflow.com/questions/36850296/get-a-prefabs-file-location-in-unity
    // https://forum.unity.com/threads/dropdown-in-inspector.468739/
    // https://forum.unity.com/threads/editorguilayout-scrollview-not-working.143502/

    // optional
    // https://answers.unity.com/questions/201848/how-to-create-a-drop-down-menu-in-editor.html
    // https://gist.github.com/bzgeb/3800350
    // GUID http://www.unity3d.ru/distribution/viewtopic.php?f=18&t=4640


    //string myString = "Hello World";
    //bool groupEnabled;
    //bool myBool = true;
    //float myFloat = 1.23f;
    // void RenderExample()
    // {
    // // myString = EditorGUILayout.TextField ("Text Field", myString);
    //         
    // // groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", groupEnabled);
    // // myBool = EditorGUILayout.Toggle ("Toggle", myBool);
    // // myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
    // // EditorGUILayout.EndToggleGroup ();
    // }

    public struct SimpleUISceneType
    {
        public string Url;
        public string Name;
        public string AssetPath;
        public bool Exists;

        public long Usages;
        public long LastOpened;

        public SimpleUISceneType(string url, string assetPath, string name = "")
        {
            Url = url;
            AssetPath = assetPath;
            Name = name.Length > 0 ? name : url;
            Exists = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetPath) != null;

            Usages = 0;
            LastOpened = 0;
        }
    }

    public class PrefabMatchInfo
    {
        public string PrefabAssetPath;
        public string ComponentName;
        public int ComponentID;
        public string URL;

        public GameObject Asset;
        public OpenUrl Component;

        public bool IsDirectMatch; // with no nested prefabs, can apply changes directly. (Both on root and it's childs)
        public bool IsNormalPartOfNestedPrefab; // absolutely normal prefab part with NO overrides. No actions required

        public bool IsOverridenAsComponentProperty;
        public bool IsOverridenAsAddedComponent;
    }

    public partial class SimpleUI : ScriptableObject
    {
        public static bool isConcreteUrlChosen = false;

        public static string newUrl = "";
        public static string newName = "";
        public static string newPath = "";

        public static List<PrefabMatchInfo> allAssetsWithOpenUrl = new List<PrefabMatchInfo>();
        public static Dictionary<string, MonoScript> allScripts = new Dictionary<string, MonoScript>();
        public static List<UsageInfo> referencesFromCode = new List<UsageInfo>();

        static bool isPrefabMode => PrefabStageUtility.GetCurrentPrefabStage() != null;

        static int ChosenIndex => prefabs.FindIndex(p => p.Url.Equals(GetCurrentUrl())); // GetCurrentUrl()
        static bool hasChosenPrefab => ChosenIndex >= 0;

        public static string GetCurrentUrl() => newUrl.StartsWith("/") ? newUrl : "/" + newUrl;


        internal static string GetOpenedAssetPath()
        {
            if (isPrefabMode)
            {
                return PrefabStageUtility.GetCurrentPrefabStage().assetPath;
            }

            return SceneManager.GetActiveScene().path;
        }

        static SimpleUI()
        {
            PrefabStage.prefabStageOpened += PrefabStage_prefabOpened;
            PrefabStage.prefabStageClosing += PrefabStage_prefabClosed;

            //EditorSceneManager.activeSceneChangedInEditMode += EditorSceneManager_activeSceneChangedInEditMode;
            //EditorSceneManager.sceneOpened += EditorSceneManager_sceneOpened;
            ////EditorSceneManager.sceneLoaded += EditorSceneManager_sceneLoaded;

            ////SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            ////SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
            ////SceneManager.activeSceneChanged += SceneManager_sceneChanged;

            //ScanProject();
        }

        private void OnEnable()
        {
            ScanProject();
        }

        private void Update()
        {
            ScanProject();
        }
    }
}