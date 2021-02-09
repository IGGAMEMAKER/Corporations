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

        bool isConcreteUrlChosen => SimpleUI.isConcreteUrlChosen;

        public float myFloat = 1f;

        public List<SimpleUISceneType> prefabs => SimpleUI.prefabs;

        public List<PrefabMatchInfo> allAssetsWithOpenUrl => SimpleUI.allAssetsWithOpenUrl;
        public Dictionary<string, MonoScript> allScripts => SimpleUI.allScripts;
        public List<UsageInfo> referencesFromCode => SimpleUI.referencesFromCode;

        // skipping first frame to reduce recompile time
        public static bool isFirstGUI = true;
        public static bool isFirstInspectorGUI = true;

        // chosen asset
        static bool isPrefabMode => PrefabStageUtility.GetCurrentPrefabStage() != null;

        int ChosenIndex => prefabs.FindIndex(p => p.Url.Equals(GetCurrentUrl())); // GetCurrentUrl()
        bool hasChosenPrefab => ChosenIndex >= 0;

        public string GetCurrentUrl() => newUrl.StartsWith("/") ? newUrl : "/" + newUrl;

        SimpleUI _instance = null;
        public SimpleUI SimpleUI
        {
            get
            {
                if (_instance == null)
                    LoadSimpleUI();

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


        bool loadedSimpleUI = false;
        //private void OnEnable()
        //{
        //    if (!loadedSimpleUI)
        //    {
        //        LoadSimpleUI();
        //    }
        //}

        void LoadSimpleUI()
        {
            _instance = SimpleUI.GetInstance(); // GetAllInstances<SimpleUI>().FirstOrDefault(); // new SimpleUI();
            loadedSimpleUI = true;
        }

        void OnGUI()
        {
            return;
            if (!isFirstGUI)
                RenderGUI();

            isFirstGUI = false;
        }

        void OnInspectorUpdate()
        {
            return;
            if (!isFirstInspectorGUI)
                RenderInspectorGUI();

            isFirstInspectorGUI = false;
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
            ChooseUrlFromPickedPrefab(SimpleUI);

            // no matching urls
            if (newUrl.Equals(""))
                SetAddingRouteMode();

            bool objectChanged = !newPath.Equals(path); // || !newUrl.Equals(url);

            if (objectChanged)
            {
                Debug.Log("Object changed");
                SimpleUI.newPath = path;

                TryToIncreaseCurrentPrefabCounter();
            }

            if (!isPrefabMode)
            {
                WrapSceneWithMenu();
            }
        }

        //void RenderRefreshButton()
        //{
        //    if (Button("Refresh"))
        //    {
        //        AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);

        //        var u = newUrl;
        //        LoadData();

        //        LoadReferences(u);

        //        OpenPrefabByUrl(u);
        //    }
        //}

        void SaveData()
        {
            SimpleUI.SaveData();
        }

        #region Render prefabs

        void RenderPrefabs(IEnumerable<SimpleUISceneType> list, string trimStart = "")
        {
            foreach (var p in list)
            {
                var c = GUI.color;

                // set color
                bool isChosen = hasChosenPrefab && prefabs[ChosenIndex].AssetPath.Equals(p.AssetPath);

                var color = isChosen ? Color.yellow : Color.white;

                //ColorUtility.TryParseHtmlString(isChosen ? "gold" : "white", out Color color);
                //var color = ColorUtility.TryParseHtmlString(isChosen ? "#FFAB04" Visuals.GetColorFromString(isChosen ? Colors.COLOR_YOU : Colors.COLOR_NEUTRAL);

                GUI.contentColor = color;
                GUI.color = color;
                GUI.backgroundColor = color;


                GUIStyle style = GUI.skin.FindStyle("Button");
                style.richText = true;

                string trimmedUrl = p.Url;

                if (trimStart.Length > 0)
                {
                    var lastDashIndex = trimmedUrl.LastIndexOf('/');

                    trimmedUrl = trimmedUrl.Substring(lastDashIndex);
                    //trimmedUrl = trimmedUrl.Trim(trimStart.ToCharArray());
                }

                if (GUILayout.Button($"<b>{p.Name}</b>\n{trimmedUrl}", style))
                {
                    OpenPrefab(p);
                }

                // restore colors
                GUI.contentColor = c;
                GUI.color = c;
                GUI.backgroundColor = c;
            }
        }

        void OpenPrefab(SimpleUISceneType prefab)
        {
            PossiblePrefab = null;
            isDraggedPrefabMode = false;
            isUrlEditingMode = false;

            SimpleUI.OpenPrefab(prefab);
        }

        //static void OpenPrefab(SimpleUISceneType p)
        //{
        //    newPath = p.AssetPath;
        //    newUrl = p.Url;


        //    isConcreteUrlChosen = true;


        //    LoadReferences(newUrl);

        //    OpenAssetByPath(newPath);
        //}

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
                var root = SimpleUI.GetPrefabByUrl(upperUrl);

                Label($"Root");

                RenderPrefabs(new List<SimpleUISceneType> { root });
            }
        }

        void RenderSubroutes()
        {
            var subUrls = SimpleUI.GetSubUrls(newUrl, false);

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

        #region UI shortcuts
        public static bool Button(string text)
        {
            GUIStyle style = GUI.skin.FindStyle("Button");
            style.richText = true;

            if (!text.Contains("\n"))
                text += "\n";

            return GUILayout.Button($"<b>{text}</b>", style);
        }

        public static void Label(string text)
        {
            Space();
            BoldLabel(text);
        }

        public static void BoldLabel(string text)
        {
            GUILayout.Label(text, EditorStyles.boldLabel);
        }

        public static void Space(int space = 15)
        {
            GUILayout.Space(space);
        }

        #endregion

        void Print(string str)
        {
            Debug.Log(str);
        }
    }
}