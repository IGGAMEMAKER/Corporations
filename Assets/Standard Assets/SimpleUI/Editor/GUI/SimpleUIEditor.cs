using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEngine.SceneManagement;

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

        // chosen asset
        static bool isPrefabMode => PrefabStageUtility.GetCurrentPrefabStage() != null;
        static bool isSceneMode => !isPrefabMode;

        int ChosenIndex => prefabs.FindIndex(p => p.Url.Equals(GetCurrentUrl())); // GetCurrentUrl()
        bool hasChosenPrefab => ChosenIndex >= 0;

        public string GetCurrentUrl() => newUrl.StartsWith("/") ? newUrl : "/" + newUrl;

        // skipping first frame to reduce recompile time
        //static bool isFirstGUI = true;
        //static bool isFirstInspectorGUI = true;

        [MenuItem("Tools/SIMPLE UI")]
        public static void ShowWindow()
        {
            //Show existing window instance. If one doesn't exist, make one.
            // EditorWindow.GetWindow(typeof(SimpleUI), false, "Simple UI", true);
            //var w = EditorWindow.GetWindow(typeof(SimpleUI));


            var w = GetInstance(); // EditorWindow.GetWindow<SimpleUI>("Simple UI");
            // w.minSize = new Vector2(200, 100);
        }

        string GetPrettyNameForAsset(CountableAsset m)
        {
            string assetPath = m.AssetPath;

            var trimmedName = assetPath.Substring(assetPath.LastIndexOf('/') + 1).Replace(".prefab", "").Replace(".unity", "");

            bool isOpened = assetPath.Equals(SimpleUI.GetOpenedAssetPath());

            if (isOpened)
                return $"<b>{trimmedName}</b>";
            else
                return trimmedName;
        }

        void RenderRecentAssets()
        {
            var path = SimpleUI.GetOpenedAssetPath();

            var ass = countableAssets;

            var favorite = ass.OrderByDescending(a => a.Usages).Take(4).ToList();
            var favoriteCounter = favorite.Any() ? favorite.Last().Usages : 0;

            var recent = ass
                //.Where(c => !c.AssetPath.Equals(path))
                .OrderByDescending(c => c.LastOpened)
                .ToList();
            ;

            if (recent.Count() >= 2)
                RenderCountableAssets(recent, favorite, path);
        }
        void RenderCountableAssets(List<CountableAsset> recent, List<CountableAsset> favorite, string path)
        {
            //GUILayout.BeginArea(new Rect(Screen.width - w - off, off, w, h));

            //scrollPosition3 = GUILayout.BeginScrollView(scrollPosition3);

            Label("Favorite assets");
            var favoriteID = GUILayout.SelectionGrid(-1, favorite.Select(GetPrettyNameForAsset).ToArray(), 1);

            Label("Recent assets");
            var recentID = GUILayout.SelectionGrid(-1, recent.Select(GetPrettyNameForAsset).ToArray(), 1);

            //GUILayout.EndScrollView();
            //GUILayout.EndArea();

            if (favoriteID != -1)
            {
                OpenCountableAsset(favorite.ToList()[favoriteID].AssetPath);
            }

            if (recentID != -1)
            {
                OpenCountableAsset(recent.ToList()[recentID].AssetPath);
            }
        }

        void OpenCountableAsset(string path)
        {
            SimpleUI.OpenAsset(path);
        }

        void DeferredAssetSwitch()
        {
            if (deferredPath.Length > 0)
            {
                OpenAsset(deferredPath);
                deferredPath = "";
            }
        }

        void OnGUI()
        {
            RenderGUI2();
            DeferredAssetSwitch();
        }

        public void Update()
        {
            DeferredAssetSwitch();
        }

        void OnInspectorUpdate()
        {
            RenderInspectorGUI();

            DeferredAssetSwitch();
        }

        void RenderGUI2()
        {
            GUILayout.Label("SIMPLE UI", EditorStyles.largeLabel);
            RenderRecentAssets();

            //if (Button("Load countable assets"))
            //{
            //    LoadCountableAssets();
            //}
        }

        void RenderGUI()
        {
            recentPrefabsScrollPosition = GUILayout.BeginScrollView(recentPrefabsScrollPosition);
            GUILayout.Label("SIMPLE UI", EditorStyles.largeLabel);

            //RenderExistingTroubles();

            Space();
            //myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);


            Label("Assets with OpenUrl component: " + GetAllAssetsWithOpenUrl().Count);
            if (Button("Refresh"))
            {
                ScanProject(true);
            }
            Space();

            //RenderMakeGuidButton();
            //AttachGUIDsToOpenUrlComponents();

            bool addingRouteModeBlah = !(isDraggedPrefabMode || isDraggedGameObjectMode || hasChosenPrefab);
            if (addingRouteModeBlah)
                RenderAddingNewRoute();

            if (!hasChosenPrefab)
                RenderPrefabs();

            if (isDraggedGameObjectMode)
                RenderMakingAPrefabFromGameObject();
            else if (isDraggedPrefabMode)
                RenderAddingNewRouteFromDraggedPrefab();
            else if (hasChosenPrefab)
                RenderChosenPrefab();
            //else
            //    RenderAddingNewRoute();

            RenderAnchorsButton();

            HandleDragAndDrop();

            GUILayout.EndScrollView();
        }

        void RenderInspectorGUI()
        {
            var path = GetOpenedAssetPath();
            //ChooseUrlFromPickedPrefab(this);

            //// no matching urls
            //if (newUrl.Equals(""))
            //    SetAddingRouteMode();

            bool objectChanged = !newPath.Equals(path); // || !newUrl.Equals(url);

            if (objectChanged)
            {
                OnAssetChange(path);
            }
        }

        void OnAssetChange(string path)
        {
            Debug.Log("Object changed");
            SetNewPath(path);

            TryToIncreaseCurrentPrefabCounter(this);

            if (isSceneMode)
            {
                Selection.activeGameObject = SceneManager.GetActiveScene().GetRootGameObjects().First();
            }
        }


        void RenderMakeGuidButton()
        {
            bool hasNoGuidAssets = prefabs.Any(p => p.ID == null || p.ID.Length == 0) || GetAllAssetsWithOpenUrl().Any(a => a.URL_ID == null || a.URL_ID.Length == 0);

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
                    OpenAsset(path);
                }
            }
        }

        // path is asset path like blah/test.prefab
        // or url
        // or GUID
        void RenderClipboardButton(string name, string path)
        {
            bool isAssetPath = path.Contains(".");
            bool isUrl = path.Contains("/") && !isAssetPath;
            bool isGuid = System.Guid.TryParse(path, out var guid);

            if (Button(name))
            {
                if (isAssetPath)
                    OpenPrefabByAssetPath(path);

                if (isUrl)
                    OpenPrefabByUrl(path);

                if (isGuid)
                    OpenPrefabByUrl(GetPrefabByGuid(path).Url);
            }
        }

        void AnalyzeClipboard()
        {
            var clipboard = GUIUtility.systemCopyBuffer;

            bool isUrl = clipboard.Contains("/");
            bool isGuid = System.Guid.TryParse(clipboard, out var guid);

            if (isUrl && IsUrlExist(clipboard))
            {
                var name = GetPrefabByUrl(clipboard).Name;
                RenderClipboardButton(name, clipboard);
            }

            if (isGuid && IsGUIDExist(clipboard))
            {
                var name = GetPrefabByGuid(clipboard).Name;
                RenderClipboardButton(name, clipboard);
            }
        }

        void RenderAnchorsButton()
        {
            Space();
            if (Button("Attach anchors"))
            {
                AttachAnchorsToUrl();
            }
        }

    }
}