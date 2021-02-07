using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using Assets.Core;
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

    //public partial class SimpleUI : EditorWindow
    //{

    //}
    //public partial class SimpleUIEditor : EditorWindow
    public partial class SimpleUI : EditorWindow
    {
        static Vector2 recentPrefabsScrollPosition = Vector2.zero;

        static bool isDraggedPrefabMode = false;
        static bool isDraggedGameObjectMode = false;
        static bool isUrlEditingMode = false;
        static bool isUrlRemovingMode = false;
        static bool isPrefabChosenMode = false;
        static bool isUrlAddingMode = false;
        static bool isConcreteUrlChosen = false;

        public float myFloat = 1f;


        public static List<PrefabMatchInfo> allAssetsWithOpenUrl = new List<PrefabMatchInfo>();
        public static Dictionary<string, MonoScript> allScripts = new Dictionary<string, MonoScript>();
        public static List<UsageInfo> referencesFromCode = new List<UsageInfo>();

        static bool isPrefabMode => PrefabStageUtility.GetCurrentPrefabStage() != null;

        static int ChosenIndex => prefabs.FindIndex(p => p.Url.Equals(GetCurrentUrl())); // GetCurrentUrl()
        static bool hasChosenPrefab => ChosenIndex >= 0;

        public static string GetCurrentUrl() => newUrl.StartsWith("/") ? newUrl : "/" + newUrl;
        public static string GetCurrentAssetPath() => GetOpenedAssetPath(); // newPath

        public static bool isFirstGUI = true;
        public static bool isFirstInspectorGUI = true;

        private static bool isProjectScanned = false;

        static string GetOpenedAssetPath()
        {
            if (isPrefabMode)
            {
                return PrefabStageUtility.GetCurrentPrefabStage().assetPath;
            }

            return SceneManager.GetActiveScene().path;
        }

        [MenuItem("Window/SIMPLE UI")]
        public static void ShowWindow()
        {
            //Show existing window instance. If one doesn't exist, make one.
            // EditorWindow.GetWindow(typeof(SimpleUI), false, "Simple UI", true);
            //var w = EditorWindow.GetWindow(typeof(SimpleUI));
            var w = EditorWindow.GetWindow<SimpleUI>("Simple UI");
            // w.minSize = new Vector2(200, 100);
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
        }

        private void Update()
        {
            ScanProject();
        }

        void OnGUI()
        {
            if (!isFirstGUI)
                RenderGUI();

            isFirstGUI = false;
        }

        void OnInspectorUpdate()
        {
            if (!isFirstInspectorGUI)
                RenderInspectorGUI();

            isFirstInspectorGUI = false;
        }

        static string Measure(DateTime start) => Measure(start, DateTime.Now);
        static string Measure(DateTime start, DateTime end)
        {
            var milliseconds = (end - start).TotalMilliseconds;

            return $"{milliseconds:0}ms";
        }

        static void LoadScripts()
        {
            allScripts = GetAllScripts();

        }
        static void LoadAssets()
        {
            allAssetsWithOpenUrl = WhatUsesComponent<OpenUrl>();
        }

        static void ScanProject()
        {
            if (!isProjectScanned)
            {
                BoldPrint("Loading assets & scripts");

                var start = DateTime.Now;

                LoadAssets();

                var assetsEnd = DateTime.Now;

                LoadScripts();

                BoldPrint($"Loaded assets & scripts in {Measure(start)} (assets: {Measure(start, assetsEnd)}, code: {Measure(assetsEnd)})");

                isProjectScanned = true;
            }
        }

        static void LoadReferences(string url)
        {
            referencesFromCode = WhichScriptReferencesConcreteUrl(url);
        }

        void RenderGUI()
        {
            recentPrefabsScrollPosition = GUILayout.BeginScrollView(recentPrefabsScrollPosition);
            GUILayout.Label("SIMPLE UI", EditorStyles.largeLabel);
            //GUILayout.Label("Url");
            //GUILayout.Label(newUrl);
            //GUILayout.Label("Path");
            //GUILayout.Label(newPath);

            //RenderRefreshButton();

            //RenderExistingTroubles();
            //Space();
            //if (Button("Print OpenUrl info"))
            //{
            //    PrintMatchInfo(WhatUsesComponent<OpenUrl>());
            //}

            Space();
            myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
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
            ChooseUrlFromPickedPrefab();

            // no matching urls
            if (newUrl.Equals(""))
                SetAddingRouteMode();

            bool objectChanged = !newPath.Equals(path); // || !newUrl.Equals(url);

            if (objectChanged)
            {
                Debug.Log("Object changed");
                newPath = path;

                TryToIncreaseCurrentPrefabCounter();
            }

            if (!isPrefabMode)
            {
                WrapSceneWithMenu();
            }
        }

        void RenderRefreshButton()
        {
            if (Button("Refresh"))
            {
                AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);

                var u = newUrl;
                LoadData();

                LoadReferences(u);

                OpenPrefabByUrl(u);
            }
        }

        #region Render prefabs
        void RenderRecentPrefabs()
        {
            var sortedByOpenings = prefabs.OrderByDescending(pp => pp.LastOpened);
            var recent = sortedByOpenings.Take(6);

            GUILayout.Label("Recent prefabs", EditorStyles.boldLabel);
            searchUrl = EditorGUILayout.TextField("Search", searchUrl);

            if (searchUrl.Length == 0)
            {
                RenderPrefabs(recent);
            }
            else
            {
                if (Button("Clear"))
                {
                    searchUrl = "";
                }

                Space();
                RenderPrefabs(sortedByOpenings.Where(p => Contains(p.Url, searchUrl) || Contains(p.Name, searchUrl)));
            }
        }

        void RenderFavoritePrefabs()
        {
            var top = prefabs.OrderByDescending(pp => pp.Usages).Take(4);

            GUILayout.Label("Favorite prefabs", EditorStyles.boldLabel);
            RenderPrefabs(top);
        }

        void RenderAllPrefabs()
        {
            var top = prefabs.OrderByDescending(pp => pp.Url);

            GUILayout.Label("All prefabs", EditorStyles.boldLabel);
            RenderPrefabs(top);
        }

        void RenderRootPrefab()
        {
            var upperUrl = GetUpperUrl(newUrl);

            bool isTopRoute = newUrl.Equals("/");

            if (!isTopRoute)
            {
                var root = GetPrefabByUrl(upperUrl);

                Label($"Root");

                RenderPrefabs(new List<SimpleUISceneType> { root });
            }
        }

        void RenderSubroutes()
        {
            var subUrls = GetSubUrls(newUrl, false);

            if (subUrls.Any())
            {
                Label("SubRoutes");
                RenderPrefabs(subUrls, newUrl);
            }
        }

        void RenderPrefabs()
        {
            Space();

            RenderFavoritePrefabs();
            RenderRecentPrefabs();
        }
        #endregion
    }
}