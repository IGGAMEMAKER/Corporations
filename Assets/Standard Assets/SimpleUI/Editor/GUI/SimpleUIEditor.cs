using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using static SimpleUI.SimpleUI;

namespace SimpleUI
{
    public partial class SimpleUIEditor : EditorWindow
    {
        static Vector2 recentPrefabsScrollPosition = Vector2.zero;

        bool isDraggedPrefabMode = false;
        bool isDraggedGameObjectMode = false;
        bool isUrlEditingMode = false;
        bool isUrlRemovingMode = false;
        bool isUrlAddingMode = false;


        public float myFloat = 1f;

        public List<SimpleUISceneType> prefabs => SimpleUI.prefabs;

        public List<FullPrefabMatchInfo> allAssetsWithOpenUrl2 = new List<FullPrefabMatchInfo>();

        public List<FullPrefabMatchInfo> allAssetsWithOpenUrl => SimpleUI.allAssetsWithOpenUrl;
        public Dictionary<string, MonoScript> allScripts => SimpleUI.allScripts;

        // refs to concrete url
        public List<UsageInfo> referencesFromCode => SimpleUI.referencesFromCode;

        // chosen asset
        static bool isPrefabMode => PrefabStageUtility.GetCurrentPrefabStage() != null;

        bool isConcreteUrlChosen => SimpleUI.isConcreteUrlChosen;
        int ChosenIndex => prefabs.FindIndex(p => p.Url.Equals(GetCurrentUrl())); // GetCurrentUrl()
        bool hasChosenPrefab => ChosenIndex >= 0;

        public string GetCurrentUrl() => newUrl.StartsWith("/") ? newUrl : "/" + newUrl;

        // skipping first frame to reduce recompile time
        static bool isFirstGUI = true;
        static bool isFirstInspectorGUI = true;

        SimpleUI _instance = null;
        public SimpleUI SimpleUI
        {
            get
            {
                if (_instance == null)
                    _instance = SimpleUI.GetInstance();


                return _instance;
            }
        }

        [MenuItem("Window/SIMPLE UI")]
        public static void ShowWindow()
        {
            //Show existing window instance. If one doesn't exist, make one.
            // EditorWindow.GetWindow(typeof(SimpleUI), false, "Simple UI", true);
            //var w = EditorWindow.GetWindow(typeof(SimpleUI));
            var w = EditorWindow.GetWindow<SimpleUIEditor>("Simple UI");
            // w.minSize = new Vector2(200, 100);
        }



        void OnGUI()
        {
            if (!isFirstGUI)
            {
                SimpleUI.ScanProject();

                RenderGUI();
            }

            if (allAssetsWithOpenUrl2.Count == 0)
            {
                BoldPrint("Load all assets in EDITOR");
                allAssetsWithOpenUrl2 = WhatUsesComponent<OpenUrl>();
            }

            isFirstGUI = false;
        }

        void OnInspectorUpdate()
        {
            if (!isFirstInspectorGUI)
                RenderInspectorGUI();

            isFirstInspectorGUI = false;
        }



        void RenderGUI()
        {
            recentPrefabsScrollPosition = GUILayout.BeginScrollView(recentPrefabsScrollPosition);
            GUILayout.Label("SIMPLE UI", EditorStyles.largeLabel);

            //RenderExistingTroubles();

            Space();
            myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);


            Label("Assets with OpenUrl component: " + allAssetsWithOpenUrl2.Count);
            if (Button("Refresh"))
            {
                SimpleUI.ScanProject(true);
            }
            Space();

            if (!hasChosenPrefab)
                RenderPrefabs();

            if (isDraggedGameObjectMode)
                RenderMakingAPrefabFromGameObject();
            else if (isDraggedPrefabMode)
                RenderAddingNewRouteFromDraggedPrefab();
            else if (hasChosenPrefab)
                RenderChosenPrefab();
            else
                RenderAddingNewRoute();

            HandleDragAndDrop();

            GUILayout.EndScrollView();
        }



        void RenderInspectorGUI()
        {
            var path = GetOpenedAssetPath();
            ChooseUrlFromPickedPrefab(SimpleUI);

            // no matching urls
            if (newUrl.Equals(""))
                SetAddingRouteMode();

            bool objectChanged = !newPath.Equals(path); // || !newUrl.Equals(url);

            if (objectChanged)
            {
                Debug.Log("Object changed");
                SetNewPath(path);

                TryToIncreaseCurrentPrefabCounter(SimpleUI);
            }

            //if (!isPrefabMode)
            //{
            //    WrapSceneWithMenu();
            //}
        }
    }
}