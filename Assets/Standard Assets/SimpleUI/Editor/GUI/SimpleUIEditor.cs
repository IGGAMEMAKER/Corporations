using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;

namespace SimpleUI
{
    public partial class SimpleUI : EditorWindow
    {
        static Vector2 recentPrefabsScrollPosition = Vector2.zero;

        bool isDraggedPrefabMode = false;
        bool isDraggedGameObjectMode = false;
        bool isUrlEditingMode = false;
        bool isUrlRemovingMode = false;
        bool isUrlAddingMode = false;


        public float myFloat = 1f;

        public static bool IsDataLoaded = false;



        // chosen asset
        static bool isPrefabMode => PrefabStageUtility.GetCurrentPrefabStage() != null;

        int ChosenIndex => prefabs.FindIndex(p => p.Url.Equals(GetCurrentUrl())); // GetCurrentUrl()
        bool hasChosenPrefab => ChosenIndex >= 0;

        public string GetCurrentUrl() => newUrl.StartsWith("/") ? newUrl : "/" + newUrl;

        // skipping first frame to reduce recompile time
        static bool isFirstGUI = true;
        static bool isFirstInspectorGUI = true;

        [MenuItem("Window/SIMPLE UI")]
        public static void ShowWindow()
        {
            //Show existing window instance. If one doesn't exist, make one.
            // EditorWindow.GetWindow(typeof(SimpleUI), false, "Simple UI", true);
            //var w = EditorWindow.GetWindow(typeof(SimpleUI));
            var w = GetInstance(); // EditorWindow.GetWindow<SimpleUI>("Simple UI");
            // w.minSize = new Vector2(200, 100);
        }



        void OnGUI()
        {
            if (!isFirstGUI)
            {
                ScanProject();

                RenderGUI();
            }

            isFirstGUI = false;
        }

        void OnInspectorUpdate()
        {
            if (!isFirstInspectorGUI)
                RenderInspectorGUI();

            isFirstInspectorGUI = false;
        }

        void RenderMakeGuidButton()
        {
            bool hasNoGuidAssets = prefabs.Any(p => p.ID == null || p.ID.Length == 0);

            if (hasNoGuidAssets)
                Space();

            if (hasNoGuidAssets && Button("Make GUIDS for SimpleUI.txt!"))
            {
                var prefs = SimpleUI.GetPrefabsFromFile();
                for (var i = 0; i < prefs.Count; i++)
                {
                    var pref = prefs[i];

                    pref.SetGUID();
                    BoldPrint($"Setting GUID for {pref.Name} {pref.ID}");

                    UpdatePrefab(pref, i);
                }
            }
        }

        void AttachGUIDsToOpenUrlComponents()
        {
            Space();

            var matches = GetAllAssetsWithOpenUrl();

            var paths = new List<string>();

            foreach (var m in matches)
            {
                if (!paths.Contains(m.PrefabAssetPath))
                    paths.Add(m.PrefabAssetPath);
            }

            for (var i = 0; i < paths.Count; i++)
            {
                var path = paths[i];

                if (Button(path))
                {
                    OpenAssetByPath(path);
                }
            }
        }

        void AttachAnchorsToUrl()
        {
            Space();
            if (Button("Attach anchors"))
            {
                foreach (var p in prefabs)
                {
                    BoldPrint("Trying to rename " + p.Url);

                    foreach (var script in allScripts)
                    {
                        if (p.Url.Equals("/"))
                            continue;

                        RenameUrlInScript(script.Key, p.Url, $"simplelink:{p.Url}");
                    }
                }
            }
        }

        void RenderGUI()
        {
            recentPrefabsScrollPosition = GUILayout.BeginScrollView(recentPrefabsScrollPosition);
            GUILayout.Label("SIMPLE UI", EditorStyles.largeLabel);

            //RenderExistingTroubles();

            Space();
            myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);


            Label("Assets with OpenUrl component: " + GetAllAssetsWithOpenUrl().Count);
            if (Button("Refresh"))
            {
                ScanProject(true);
            }
            Space();

            RenderMakeGuidButton();
            //AttachGUIDsToOpenUrlComponents();
            AttachAnchorsToUrl();

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
                SetNewPath(path);

                TryToIncreaseCurrentPrefabCounter();
            }

            //if (!isPrefabMode)
            //{
            //    WrapSceneWithMenu();
            //}
        }
    }
}