using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using Assets.Core;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;

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
    public string GUID;

    public SceneBlahType SceneBlahType;

    public SimpleUISceneType(string url, string assetPath, string name = "")
    {
        SceneBlahType = SceneBlahType.Prefab;

        if (assetPath.EndsWith(".scene"))
            SceneBlahType = SceneBlahType.Scene;

        Url = url;
        AssetPath = assetPath;
        Name = name.Length > 0 ? name : url;
        GUID = "";

        Usages = 0;
        LastOpened = 0;
    }
}

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


public partial class SimpleUI : EditorWindow
{
    private Vector2 recentPrefabsScrollPosition = Vector2.zero;

    public static string newUrl = "";
    public static string newName = "";
    public static string newPath = "";

    private static string draggedUrl = "";
    private static string draggedName = "";
    private static string draggedPath = "";

    static string searchUrl = "";
    static Vector2 searchScrollPosition = Vector2.zero;

    private static int ChosenIndex => prefabs.FindIndex(p => p.Url.Equals(newUrl));
    private static bool hasChosenPrefab => ChosenIndex >= 0;

    private static bool isDraggedPrefabMode = false;
    private static bool isDraggedGameObjectMode = false;
    private static bool isUrlEditingMode = false;
    private static bool isPrefabChosenMode = false;
    bool isShowingUrlDetailsMode = false;

    static bool isConcreteUrlChosen = false;

    bool _isSceneMode = true;
    bool _isPrefabMode => !_isSceneMode;


    private static GameObject PossiblePrefab;
    private static string possiblePrefabName = "";
    
    [MenuItem("Window/SIMPLE UI")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        // EditorWindow.GetWindow(typeof(SimpleUI), false, "Simple UI", true);
        var w = EditorWindow.GetWindow(typeof(SimpleUI));
        // w.minSize = new Vector2(200, 100);

        // EditorWindow.CreateWindow<SimpleUI>();
    }

    public static string GetCurrentUrl() => newUrl.StartsWith("/") ? newUrl : "/" + newUrl;
    public static string GetCurrentAssetPath() => newPath;
    public static IEnumerable<SimpleUISceneType> GetSubUrls(string url, bool recursive) => prefabs.Where(p => isSubRouteOf(p.Url, url, recursive));

    static SimpleUI()
    {
        PrefabStage.prefabStageOpened += PrefabStage_prefabOpened;
        PrefabStage.prefabStageClosing += PrefabStage_prefabClosed;
    }

    private static void PrefabStage_prefabClosed(PrefabStage obj)
    {
        DestroyImmediate(obj.prefabContentsRoot.GetComponent<DisplayConnectedUrls>());
    }

    private static void PrefabStage_prefabOpened(PrefabStage obj)
    {
        Debug.Log("Prefab opened: " + obj.prefabContentsRoot.name);

        obj.prefabContentsRoot.AddComponent<DisplayConnectedUrls>();
        Selection.activeGameObject = obj.prefabContentsRoot;


        newPath = obj.assetPath;
        newName = GetPrettyNameFromAssetPath(newPath); // x.Substring(0, ind);
        
        // choose URL
        ChooseUrlFromPickedPrefab();
        TryToIncreaseCurrentPrefabCounter();
    }

    void OnInspectorUpdate()
    {
        if (PrefabStageUtility.GetCurrentPrefabStage() == null)
        {
            if (_isPrefabMode)
            {
                _isSceneMode = true;
                //Debug.Log("Scene is opened");
            }
        }
        else
        {
            _isSceneMode = false;

            var path = PrefabStageUtility.GetCurrentPrefabStage().assetPath;

            if (!newPath.Equals(path) || newUrl.Length == 0)
            {
                //Print("Prefab chosen: " + path);
                newPath = path;

                ChooseUrlFromPickedPrefab();
            }
        }
    }

    static void ChooseUrlFromPickedPrefab()
    {
        var urls = prefabs.Where(p => p.AssetPath.Equals(newPath));

        if (urls.Count() == 1)
        {
            newUrl = urls.First().Url;
            isConcreteUrlChosen = true;
        }

        if (urls.Count() > 1)
        {
            // pick first automatically or do nothing?
            isConcreteUrlChosen = false;
        }
    }



    public static void OpenPrefabByAssetPath(string path)
    {
        AssetDatabase.OpenAsset(AssetDatabase.LoadMainAssetAtPath(path));
    }

    public static void OpenPrefab(string url)
    {
        Debug.Log("Trying to open prefab by url: " + url);

        if (!url.StartsWith("/"))
            url = "/" + url;

        var p1 = prefabs.First(p => p.Url.Equals(url));

        OpenPrefab(p1);
    }

    static void OpenPrefab(SimpleUISceneType p)
    {
        newPath = p.AssetPath;
        newUrl = p.Url;

        PossiblePrefab = null;
        isDraggedPrefabMode = false;
        isUrlEditingMode = false;
        isConcreteUrlChosen = true;

        var asset = AssetDatabase.LoadMainAssetAtPath(p.AssetPath);
        AssetDatabase.OpenAsset(asset);
    }

    void OnGUI()
    {
        recentPrefabsScrollPosition = GUILayout.BeginScrollView(recentPrefabsScrollPosition);
        GUILayout.Label("SIMPLE UI", EditorStyles.largeLabel);

        //LoadData();

        //Label($"Url={newUrl}\nasset={newPath}\nname={newName}");

        if (!hasChosenPrefab)
            RenderPrefabs();

        if (isDraggedGameObjectMode)
            RenderMakingAPrefabFromGameObject();
        else if (isDraggedPrefabMode)
            RenderAddingNewRouteFromDraggedPrefab();
        else if (hasChosenPrefab)
        {
            if (!isConcreteUrlChosen)
            {
                // pick concrete URL
                RenderUrlsWhichAreAttachedToSamePrefab();
            }
            else
            {
                if (isUrlEditingMode)
                    RenderEditingPrefab();
                else
                    RenderLinkToEditing();
            }
        }
        else
            RenderAddingNewRoute();

        HandleDragAndDrop();

        GUILayout.EndScrollView();
    }

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
        GUILayout.Label(text, EditorStyles.boldLabel);
    }

    public static void Space(int space = 15)
    {
        GUILayout.Space(space);
    }
    #endregion
    
    #region string utils
    /// <summary>
    /// if recursive == false
    /// function will return true ONLY for DIRECT subroutes
    /// 
    /// otherwise it will return true for sub/sub routes too
    /// </summary>
    /// <param name="subUrl"></param>
    /// <param name="root"></param>
    /// <param name="recursive"></param>
    /// <returns></returns>
    public static bool isSubRouteOf(string subUrl, string root, bool recursive)
    {
        if (subUrl.Equals(root))
            return false;

        bool startsWith = subUrl.StartsWith(root);

        if (!startsWith)
            return false;

        var subStr = subUrl.Substring(root.Length);

        if (root.Equals("/"))
            subStr = subUrl;

        bool isUrlItself = subStr.IndexOf('/') == 0;

        if (!isUrlItself)
            return false;

        bool isDirectSubroute = subStr.LastIndexOf('/') <= 0;
        bool isSubSubRoute = subStr.LastIndexOf('/') > 0;

        if (!recursive)
            return isDirectSubroute;
        else
            return isSubSubRoute;
    }

    static string GetPrettyNameFromAssetPath(string path)
    {
        var x = path.Split('/').Last();
        var ind = x.LastIndexOf(".prefab");

        return x.Substring(0, ind);
    }

    public static string GetPrettyNameForExistingUrl(string url)
    {
        var prefab = GetPrefabByUrl(url);

        return prefab.Name;
    }

    public static string GetUpperUrl(string url)
    {
        var index = url.LastIndexOf("/");

        if (index <= 0)
            return "/";

        return url.Substring(0, index);
    }

    bool Contains(string text1, string searching)
    {
        return text1.ToLower().Contains(searching.ToLower());
    }

    string GetValidatedUrl(string url)
    {
        if (!url.StartsWith("/"))
            return url.Insert(0, "/");

        return url;
    }
    #endregion


    void RenderEditingPrefab()
    {
        var index = ChosenIndex;
        var pref = prefabs[index];

        Label(pref.Url);

        if (Button("Go back"))
        {
            isUrlEditingMode = false;
        }

        newUrl = pref.Url;
        newPath = pref.AssetPath;
        newName = pref.Name;

        var prevUrl = newUrl;
        var prevName = newName;
        var prevPath = newPath;

        RenderRootPrefab();

        RenderSubroutes();

        bool changedUrl = !newPath.Equals(prevPath);

        if (changedUrl)
            return;

        Space();

        Label("Edit url");
        newUrl = EditorGUILayout.TextField("Url", newUrl);

        if (newUrl.Length > 0)
        {
            newName = EditorGUILayout.TextField("Name", newName);

            if (newName.Length > 0)
            {
                newPath = EditorGUILayout.TextField("Asset Path", newPath);
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
        Space();
        if (pref.Usages > 0 && GUILayout.Button("Reset Counter"))
        {
            pref.Usages = 0;

            UpdatePrefab(pref);
        }

        var maxUsages = prefabs.Max(p => p.Usages);
        if (pref.Usages < maxUsages && GUILayout.Button("Prioritize"))
        {
            pref.Usages = maxUsages + 1;

            UpdatePrefab(pref);
        }
        
        Space(450);
        if (GUILayout.Button("Remove URL"))
        {
            prefabs.RemoveAt(index);
            SaveData();
        }
    }

    void RenderAddingNewRoute()
    {
        Space();
        GUILayout.Label("Add current prefab", EditorStyles.boldLabel);

        newUrl = EditorGUILayout.TextField("Url", newUrl);

        bool urlOK = newUrl.Length > 0;
        bool newNameOK = newName.Length > 0;
        bool pathOK = newPath.Length > 0;

        if (urlOK)
        {
            newUrl = GetValidatedUrl(newUrl);

            newName = EditorGUILayout.TextField("Name", newName);
        }

        if (urlOK && newNameOK)
        {
            newPath = EditorGUILayout.TextField("Asset Path", newPath);
        }

        if (urlOK && newNameOK && pathOK)
        {
            Space();
            if (GUILayout.Button("Add prefab!")) //  <" + newName + ">
            {
                Debug.Log("Added prefab");

                AddPrefab(newUrl, newPath, newName);

                SaveData();
            }
        }
    }



    #region Render prefabs
    void RenderLinkToEditing()
    {
        var index = ChosenIndex;
        var pref = prefabs[index];

        Label(pref.Url);

        if (Button("Edit prefab"))
        {
            isUrlEditingMode = true;
        }

        Space();
        RenderPrefabs();
    }

    void RenderUrlsWhichAreAttachedToSamePrefab()
    {
        var chosenPrefab = prefabs[ChosenIndex];
        var samePrefabUrls = prefabs.Where(p => p.AssetPath.Equals(chosenPrefab.AssetPath));

        Label("Prefab " + chosenPrefab.Name + " is attached to these urls");
        Label("Choose proper one!");

        Space();
        RenderPrefabs(samePrefabUrls);
    }

    void RenderRecentPrefabs()
    {
        var sortedByOpenings = prefabs.OrderByDescending(pp => pp.LastOpened);
        var recent = sortedByOpenings.Take(6);

        GUILayout.Label("Recent prefabs", EditorStyles.boldLabel);
        searchUrl = EditorGUILayout.TextField("Search", searchUrl);

        if (searchUrl.Length == 0)
            RenderPrefabs(recent);
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

        if (subUrls.Count() > 0)
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


    // ----- utils -------------
    #region Utils
    static SimpleUISceneType GetPrefabByUrl(string url)
    {
        return prefabs.FirstOrDefault(p => p.Url.Equals(url));
    }

    void AddPrefab(string ururu, string papapath, string nananame)
    {
        var p = new SimpleUISceneType(ururu, papapath, nananame) {LastOpened = DateTime.Now.Ticks};

        prefabs.Add(p);
    }

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

    static void TryToIncreaseCurrentPrefabCounter()
    {
        if (hasChosenPrefab)
        {
            var pref = prefabs[ChosenIndex];

            pref.Usages++;
            pref.LastOpened = DateTime.Now.Ticks;

            UpdatePrefab(pref);
        }
    }


    #endregion

    static void Print2(string text)
    {
        //Print(text);
    }
    static void Print(string text)
    {
        Debug.Log(text);
    }

    public static void OpenUrl(string url)
    {
        SimpleUIEventHandler eventHandler = FindObjectOfType<SimpleUIEventHandler>();

        if (eventHandler == null)
        {
            Debug.LogError("SimpleUIEventHandler NOT FOUND");
        }
        else
        {
            var queryIndex = url.IndexOf('?');
            var query = "";

            if (queryIndex >= 0)
            {
                query = url.Substring(queryIndex);
                url = url.Substring(0, queryIndex);
            }

            eventHandler.OpenUrl(url);
        }
    }
}

// dragging prefabs
public partial class SimpleUI
{
    #region dragging prefabs

    void HandleDragAndDrop()
    {
        if (Event.current.type == EventType.DragUpdated)
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
            Event.current.Use();
        }
        else if (Event.current.type == EventType.DragPerform)
        {
            // To consume drag data.
            DragAndDrop.AcceptDrag();

            // GameObjects from hierarchy.
            if (DragAndDrop.paths.Length == 0 && DragAndDrop.objectReferences.Length > 0)
            {
                foreach (var obj in DragAndDrop.objectReferences)
                {
                    var go = obj as GameObject;
                    bool isPrefab = PrefabUtility.GetPrefabAssetType(obj) != PrefabAssetType.NotAPrefab;

                    if (isPrefab)
                    {
                        Debug.Log("prefab - " + obj);

                        HandleDraggedPrefab(go);
                    }
                    else
                    {
                        Debug.Log("GameObject - " + obj);

                        HandleDraggedGameObject(go);
                    }
                }
            }
            // Object outside project. It mays from File Explorer (Finder in OSX).
            else if (DragAndDrop.paths.Length > 0 && DragAndDrop.objectReferences.Length == 0)
            {
                Debug.Log("File");
                foreach (string path in DragAndDrop.paths)
                {
                    Debug.Log("- " + path);
                }
            }
            // Unity Assets including folder.
            else if (DragAndDrop.paths.Length == DragAndDrop.objectReferences.Length)
            {
                Debug.Log("UnityAsset");
                for (int i = 0; i < DragAndDrop.objectReferences.Length; i++)
                {
                    var obj = DragAndDrop.objectReferences[i];
                    string path = DragAndDrop.paths[i];
                    Debug.Log(obj.GetType().Name);

                    // Folder.
                    if (obj is DefaultAsset)
                    {
                        Debug.Log(path);
                    }
                    // C# or JavaScript.
                    else if (obj is MonoScript)
                    {
                        Debug.Log(path + "\n" + obj);
                    }
                    else if (obj is Texture2D)
                    {
                    }
                }
            }
            // Log to make sure we cover all cases.
            else
            {
                Debug.Log("Out of reach");
                Debug.Log("Paths:");
                foreach (string path in DragAndDrop.paths)
                {
                    Debug.Log("- " + path);
                }

                Debug.Log("ObjectReferences:");
                foreach (var obj in DragAndDrop.objectReferences)
                {
                    Debug.Log("- " + obj);
                }
            }
        }
    }

    private void RenderMakingAPrefabFromGameObject()
    {
        const string defaultPrefabName = "Bad prefab name: You cannot name new prefab GameObject, cause it's easy to confuse name";

        Space();
        possiblePrefabName = EditorGUILayout.TextField("Name", possiblePrefabName);

        var path = $"Assets/Prefabs/{possiblePrefabName}.prefab";

        bool hasSameNamePrefabAlready = AssetDatabase.LoadAssetAtPath<GameObject>(path) != null;
        bool isEmpty = possiblePrefabName.Length == 0;
        bool isDefaultName = possiblePrefabName.ToLower().Equals("gameobject");

        bool isNameOK = !isEmpty && !isDefaultName && !hasSameNamePrefabAlready;

        if (!isNameOK)
        {
            if (isDefaultName)
                EditorGUILayout.HelpBox(defaultPrefabName, MessageType.Error);
            //EditorGUILayout.LabelField(defaultPrefabName);

            if (hasSameNamePrefabAlready)
                EditorGUILayout.HelpBox($"Bad prefab name: prefab ({path}) already exists)", MessageType.Error);
            // EditorGUILayout.LabelField($"!!!Bad prefab name: empty: {isEmpty}, name=gameobject: {isDefaultName}, prefab already exists: {hasSameNamePrefabAlready}");
        }

        if (isNameOK && Button("CREATE prefab!"))
        {
            PrefabUtility.SaveAsPrefabAssetAndConnect(PossiblePrefab, path, InteractionMode.UserAction, out var success);

            Debug.Log("Prefab saving " + success);

            if (success)
            {
                isDraggedGameObjectMode = false;
                HandleDraggedPrefab(PossiblePrefab);
            }
        }

        Space();
        if (Button("Go Back"))
        {
            isDraggedGameObjectMode = false;
            isDraggedPrefabMode = false;
        }
    }

    void RenderAddingNewRouteFromDraggedPrefab()
    {
        Space();
        GUILayout.Label("Add DRAGGED prefab", EditorStyles.boldLabel);

        draggedName = EditorGUILayout.TextField("Name", draggedName);
        draggedUrl = EditorGUILayout.TextField("Url", draggedUrl);

        var dataCorrect = draggedUrl.Length > 0 && draggedName.Length > 0;

        if (dataCorrect && GUILayout.Button("Add DRAGGED prefab!"))
        {
            Space();

            draggedUrl = GetValidatedUrl(draggedUrl);

            AddPrefab(draggedUrl, draggedPath, draggedName);

            isDraggedPrefabMode = false;

            SaveData();

            Debug.Log("Added DRAGGED prefab");

            DestroyImmediate(PossiblePrefab);

            PossiblePrefab = null;

            Debug.Log("Removed object too");
        }
    }

    void HandleDraggedPrefab(GameObject go)
    {
        isDraggedPrefabMode = true;
        PossiblePrefab = go;

        var parent = PrefabUtility.GetCorrespondingObjectFromSource(go);
        string prefabPath = AssetDatabase.GetAssetPath(parent);

        Debug.Log("route = " + prefabPath);

        // try to attach this prefab
        // to current prefab

        draggedName = GetPrettyNameFromAssetPath(prefabPath);
        draggedPath = prefabPath;
        draggedUrl = newUrl.TrimEnd('/') + "/" + draggedName.TrimStart('/');
    }

    void HandleDraggedGameObject(GameObject go)
    {
        isDraggedGameObjectMode = true;

        possiblePrefabName = go.name;

        draggedName = go.name;
        draggedPath = "";
        // draggedUrl = newUrl + "/" + draggedName;
        draggedUrl = newUrl.TrimEnd('/') + "/" + draggedName.TrimStart('/');

        PossiblePrefab = go;
    }

    #endregion
}


// Save/Load info
public partial class SimpleUI
{
    static List<SimpleUISceneType> _prefabs;
    public static List<SimpleUISceneType> prefabs
    {
        get
        {
            if (_prefabs == null || _prefabs.Count == 0)
            {
                LoadData();
            }

            return _prefabs;
        }
    }
    // = new List<NewSceneTypeBlah>();

    static void SaveData()
    {
        var fileName = "SimpleUI/SimpleUI.txt";

        Newtonsoft.Json.JsonSerializer serializer = new Newtonsoft.Json.JsonSerializer();
        serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
        serializer.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
        serializer.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
        serializer.Formatting = Newtonsoft.Json.Formatting.Indented;

        var entityData = _prefabs;
        //var entityData = prefabs; // new Dictionary<int, IComponent[]>();

        using (StreamWriter sw = new StreamWriter(fileName))
        using (Newtonsoft.Json.JsonWriter writer = new Newtonsoft.Json.JsonTextWriter(sw))
        {
            if (entityData.Count > 0)
            {
                //Debug.Log("Serializing data " + entityData.Count);
                serializer.Serialize(writer, entityData);
                //Debug.Log("Serialized " + entityData.Count);
            }
        }
    }

    static void LoadData()
    {
        //if (prefabs != null && prefabs.Count == 0)
        //    return;

        var fileName = "SimpleUI/SimpleUI.txt";

        List<SimpleUISceneType> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SimpleUISceneType>>(
            File.ReadAllText(fileName), new Newtonsoft.Json.JsonSerializerSettings
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            });

        _prefabs = obj ?? new List<SimpleUISceneType>();
        //prefabs = obj ?? new List<SimpleUISceneType>();
    }

    static void UpdatePrefab(SimpleUISceneType prefab)
    {
        if (!hasChosenPrefab)
            return;

        prefabs[ChosenIndex] = prefab;
        SaveData();
    }
}

// Scripts, attached to prefab
public partial class SimpleUI
{
    void RenderScriptsAttachedToThisPrefab(SimpleUISceneType p)
    {
        var GO = Selection.activeObject as GameObject;
        var scripts = new Dictionary<string, int>();

        RenderScriptsAttachedToThisGameObject(GO.transform, ref scripts);

        Debug.Log("Scripts, attached to PREFAB");

        foreach (var s in scripts)
            Debug.Log(s.Key);
    }

    void RenderScriptsAttachedToThisGameObject(Transform GO, ref Dictionary<string, int> scripts)
    {
        foreach (Transform go in GO.transform)
        {
            foreach (Component c in go.GetComponents<Component>())
            {
                string name = ObjectNames.GetInspectorTitle(c);
                if (name.EndsWith("(Script)"))
                {
                    string formated = name.Replace("(Script)", String.Empty).Replace(" ", String.Empty) + ".cs";
                    scripts[formated] = 1;
                }
            }

            RenderScriptsAttachedToThisGameObject(go, ref scripts);
        }
    }
}

// what uses component OpenUrl
public partial class SimpleUI : EditorWindow
{
    #region What Uses Component OpenUrl
    public static bool HasNoPrefabsBetweenObjects(MonoBehaviour component, GameObject root)
    {
        // is directly attached to root prefab object with no in between prefabs

        // root GO
        // -> NonPrefab1 GO
        // -> NonPrefab2 GO
        // -> -> NonPrefab3 GO with our component
        // returns true

        // -> NonPrefab1 GO
        // -> Prefab2
        // -> -> NonPrefab3 Go with our component
        // returns false

        // PrefabUtility.IsAnyPrefabInstanceRoot(component.gameObject);
        // PrefabUtility.IsOutermostPrefabInstanceRoot(component.gameObject);
        // PrefabUtility.IsPartOfAnyPrefab(component.gameObject);
        // PrefabUtility.IsPartOfPrefabAsset(component.gameObject);
        // PrefabUtility.IsPartOfPrefabInstance(component.gameObject);
        // PrefabUtility.IsPartOfRegularPrefab(component.gameObject);
        // PrefabUtility.IsPartOfNonAssetPrefabInstance(component.gameObject);

        //var nearestRoot = PrefabUtility.GetNearestPrefabInstanceRoot(component);
        //var outerRoot = PrefabUtility.GetOutermostPrefabInstanceRoot(component);

        //var rootId = root.GetInstanceID();

        //var nearestId = nearestRoot.GetInstanceID();
        //var outerId = outerRoot.GetInstanceID();

        //var result = nearestId == outerId;

        //// Debug.Log($"isDirectly attached to root prefab? <b>{result}</b> c={component.gameObject.name}, root={root.gameObject.name}\n" 
        ////           + $"\n<b>nearest prefab ({nearestId}): </b>" + nearestRoot.name 
        ////           + $"\n<b>outer prefab ({outerId}): </b>" + outerRoot.name 
        ////           + $"\n\nroot instance ID={rootId}");

        //return result;

        var rootOf = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(root);
        var pathOfComponent = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(component);

        return rootOf.Equals(pathOfComponent);
    }

    public static bool IsAddedAsOverridenComponent(MonoBehaviour component)
    {
        var outermost = PrefabUtility.GetOutermostPrefabInstanceRoot(component);

        //// return !PrefabUtility.GetAddedComponents(outermost).Any(ac => ac.GetType() == component.GetType() && component.GetInstanceID() == ac.instanceComponent.GetInstanceID());

        //return PrefabUtility.IsAddedComponentOverride(component);

        return PrefabUtility.GetAddedComponents(outermost).Any(ac => component.GetInstanceID() == ac.instanceComponent.GetInstanceID());
    }


    public static bool IsRootOverridenProperties2(MonoBehaviour component, GameObject root, string[] properties)
    {
        var outermost = PrefabUtility.GetOutermostPrefabInstanceRoot(component);

        // var propertyOverrides = PrefabUtility.GetPropertyModifications(outermost)
        var objectOverrides = PrefabUtility.GetObjectOverrides(outermost)
            // .Where(modification => modification.instanceObject.GetType() == typeof(OpenUrl))
            .Where(modification => modification.instanceObject.GetInstanceID() == component.GetInstanceID());
        // .Where(modification => modification.target.GetType() == typeof(OpenUrl));
        // modification.target.GetInstanceID() == component.GetInstanceID() &&
        // properties.Contains(modification.propertyPath)
        // );

        var propsFormatted = string.Join("\n", objectOverrides.Select(modification => modification.instanceObject.GetType()));
        // Print("IsRoot overriding properties: " + propsFormatted);

        return objectOverrides.Any();
    }

    public static bool HasOverridenProperties(MonoBehaviour component, string[] properties)
    {
        var result = PrefabUtility.HasPrefabInstanceAnyOverrides(component.gameObject, false);


        var overrides = PrefabUtility.GetObjectOverrides(component.gameObject);

        // var wat = overrides.First().coupledOverride.GetAssetObject();
        // Debug.Log("first override " + wat);

        var nearestPrefab = PrefabUtility.GetCorrespondingObjectFromSource(component);

        // Debug.Log($"Check overrides for component {component.gameObject.name}");

        foreach (var modification in PrefabUtility.GetPropertyModifications(component))
        {
            if (modification.propertyPath.Contains("m_"))
                continue;

            if (properties.Contains(modification.propertyPath))
                return true;
            // Debug.Log($"Property: {modification.propertyPath}");
            // Debug.Log($"Value: {modification.value}");
            // Debug.Log(modification.target);
        }

        //Debug.Log("Corresponding object for " + component.gameObject.name + " is " + nearestPrefab.name);

        //var str = result ? "HAS" : "Has NO";

        //// PrintArbitraryInfo(null, $"{component.gameObject.name} {str} overrides"); // ({root.gameObject.name})

        //return result;

        return false;
    }

    static string ParseAddedComponents(GameObject parent)
    {
        var addedComponents = PrefabUtility.GetAddedComponents(parent);

        var formattedAddedComponents = addedComponents.Where(FilterNecessaryComponents)
            .Select(ac => ac.instanceComponent.GetType() + " " + ac.instanceComponent.GetInstanceID() + " " + ac.instanceComponent.gameObject.GetInstanceID());

        return string.Join(", ", formattedAddedComponents);
    }

    private static bool FilterNecessaryComponents(AddedComponent arg)
    {
        return arg.instanceComponent.GetType() == typeof(OpenUrl);
    }

    // path - RootAssetPath
    public static PrefabMatchInfo GetPrefabMatchInfo2(MonoBehaviour component, GameObject asset, string path, string[] properties)
    {
        var matchingComponent = new PrefabMatchInfo { PrefabAssetPath = path, ComponentName = component.gameObject.name };

        bool isAttachedToRootPrefab = HasNoPrefabsBetweenObjects(component, asset);
        bool isAttachedToNestedPrefab = !isAttachedToRootPrefab;

        if (isAttachedToRootPrefab)
        {
            // directly appears in prefab
            // so you can upgrade it and safely save ur prefab

            matchingComponent.IsDirectMatch = true;
            Print2($"Directly occurs as {matchingComponent.ComponentName} in {matchingComponent.PrefabAssetPath}");
        }

        if (isAttachedToNestedPrefab)
        {
            bool isAddedByRoot = IsAddedAsOverridenComponent(component);
            bool isPartOfNestedPrefab = !isAddedByRoot;

            if (isAddedByRoot)
            {
                // added, but not saved in that prefab
                // so prefab will not see this component in itself

                // you need to update URL of this component here, but don't accidentally apply changes to prefab, which this component sits on
                // you can safely save changes in root prefab as well
                var outer = PrefabUtility.GetOutermostPrefabInstanceRoot(component);

                matchingComponent.IsOverridenAsAddedComponent = true;
                Print2(
                    $"{matchingComponent.ComponentName} Is <b>ATTACHED</b> to some nested prefab\n\nouter={outer.name} {ParseAddedComponents(outer)}, {component.GetInstanceID()}");
            }

            if (isPartOfNestedPrefab)
            {
                // so you might need to check Overriden Properties

                if (IsRootOverridenProperties2(component, asset, properties))
                {
                    // update property and just save root prefab

                    matchingComponent.IsOverridenAsComponentProperty = true;
                    Print2($"Root <b>OVERRIDES VALUES</b> on {matchingComponent.ComponentName}");
                }
                else
                {
                    // you will upgrade value in it's own prefab
                    // no actions are necessary for root prefab

                    matchingComponent.IsNormalPartOfNestedPrefab = true;
                    Print2($"{matchingComponent.ComponentName} is <b>part of a nested prefab</b>");
                }
            }
        }

        return matchingComponent;
    }

    public static void PrintMatchInfo(IEnumerable<PrefabMatchInfo> matches)
    {
        foreach (var matchingComponent in matches)
        {
            if (matchingComponent.IsDirectMatch)
            {
                // directly appears in prefab
                // so you can upgrade it and safely save ur prefab

                Print($"Directly occurs as {matchingComponent.ComponentName} in {matchingComponent.PrefabAssetPath}");
            }
            else
            {
                // appears somewhere in nested prefabs

                if (matchingComponent.IsOverridenAsAddedComponent)
                {
                    // is added by root component

                    Print($"{matchingComponent.ComponentName} Is <b>ATTACHED BY ROOT</b> to some nested prefab\n");
                }
                else
                {
                    // is part of nested prefab
                    if (matchingComponent.IsOverridenAsComponentProperty)
                    {
                        Print($"Root <b>OVERRIDES VALUES</b> on {matchingComponent.ComponentName}");
                    }

                    if (matchingComponent.IsNormalPartOfNestedPrefab)
                    {
                        Print($"{matchingComponent.ComponentName} is <b>part of a nested prefab</b>");
                    }
                }
            }
        }
    }

    public class PrefabMatchInfo
    {
        public string PrefabAssetPath;
        public string ComponentName;
        public string URL;

        public bool IsDirectMatch; // with no nested prefabs, can apply changes directly. (Both on root and it's childs)

        public bool IsNormalPartOfNestedPrefab; // absolutely normal prefab part with NO overrides. No actions required

        public bool IsOverridenPartOfNestedPrefab =>
            IsOverridenAsAddedComponent ||
            IsOverridenAsComponentProperty; // is overriden somehow: maybe there is not saved Added component or Overriden Parameters in component itself

        public bool IsOverridenAsComponentProperty;
        public bool IsOverridenAsAddedComponent;
    }

    public static List<PrefabMatchInfo> WhatUsesComponent<T>()
    {
        var typeToSearch = typeof(T);

        Debug.Log("Finding all Prefabs and scenes that have the component" + typeToSearch + "…");

        var excludeFolders = new[] { "Assets/Standard Assets" };
        var guids = AssetDatabase.FindAssets("t:prefab", new[] { "Assets" });
        // var guids = AssetDatabase.FindAssets("t:scene t:prefab", new []{ "Assets"});

        var paths = guids.Select(AssetDatabase.GUIDToAssetPath).ToList();
        // var removedPaths = paths.RemoveAll(guid => excludeFolders.Any(guid.Contains));

        List<PrefabMatchInfo> matchingComponents = new List<PrefabMatchInfo>();

        var properties = new[] { "Url" };

        foreach (var path in paths)
        {
            var asset = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (asset == null)
            {
                Debug.LogError("Cannot load prefab at path: " + path);
                continue;
            }

            List<T> components = asset.GetComponentsInChildren<T>(true).ToList();
            var self = asset.GetComponent<T>();

            if (self != null)
            {
                components.Add(self);
            }

            if (components.Any())
            {
                Print2("<b>----------------------------------------</b>");
                Print2("Found component(s) " + typeToSearch + $" ({components.Count}) in file <b>" + path + "</b>");
            }

            foreach (var component1 in components)
            {
                var component = component1 as MonoBehaviour;

                var matchingComponent = GetPrefabMatchInfo2(component, asset, path, properties);
                matchingComponent.URL = (component as OpenUrl).Url;


                matchingComponents.Add(matchingComponent);
            }
        }

        return matchingComponents;
    }

    public struct UsageInfo
    {
        public string ScriptName;
        public int Line;
    }

    public static List<UsageInfo> WhatReferencesConcreteUrl(string url)
    {
        var directory = "Assets/";
        var list = new List<UsageInfo>();

        Debug.Log("Finding all scrips, that call " + url);

        var excludeFolders = new[] { "Assets/Standard Assets/Frost UI", "Assets/Standard Assets/SimpleUI", "Assets/Standard Assets/Libraries", "Assets/Systems", "Assets/Core" };
        var guids = AssetDatabase.FindAssets("t:Script", new[] { "Assets" });

        Debug.Log($"Found {guids.Count()} scripts");
        var paths = guids.Select(AssetDatabase.GUIDToAssetPath).ToList();

        paths.RemoveAll(guid => excludeFolders.Any(guid.Contains));

        foreach (var path in paths)
        {
            var asset = AssetDatabase.LoadAssetAtPath<MonoScript>(path);

            var txt = asset != null ? ("\n" + asset.text) : "";

            //Debug.Log("found script: " + path + txt);

            if (asset == null)
            {
                Debug.LogError("Cannot load prefab at path: " + path);

                continue;
            }

            bool directMatch = true;
            var searchString = '"' + url;

            if (directMatch)
                searchString += '"';

            if (txt.Contains(searchString))
            {
                Debug.Log($"Found url {url} in text " + path);
                list.Add(new UsageInfo { Line = 1, ScriptName = path });
            }
        }

        return list;
    }
    #endregion


    //
    public static bool IsRootOverridenProperties(MonoBehaviour component, GameObject root, string[] properties)
    {
        var fastFilter = new Func<PropertyModification, bool>(p => properties.Contains(p.propertyPath));
        var print = new Func<IEnumerable<PropertyModification>, string>(p => string.Join(", ", p.Select(pp => pp.propertyPath).ToList()));

        var outermost = PrefabUtility.GetOutermostPrefabInstanceRoot(component);
        var outermostPropertyChanges = PrefabUtility.GetPropertyModifications(outermost).Where(fastFilter);

        var outermostPath = AssetDatabase.GetAssetPath(outermost);
        var outermostAsset = AssetDatabase.LoadMainAssetAtPath(outermostPath);

        // var objectOverrides = PrefabUtility.GetObjectOverrides(component.gameObject).Where(change => change.instanceObject.GetType() == typeof(OpenUrl));
        // var propertyChanges = PrefabUtility.GetPropertyModifications(component.gameObject).Where(p => !p.propertyPath.Contains("m_") && properties.Contains(p.propertyPath));
        // PrefabUtility.HasPrefabInstanceAnyOverrides()
        var propertyChanges = PrefabUtility.GetPropertyModifications(component.gameObject).Where(fastFilter);

        var text = $"Outermost changes {outermost.name} hasAnyChanges={PrefabUtility.HasPrefabInstanceAnyOverrides(outermost, false)}, {outermostPath}\n";
        text += print(outermostPropertyChanges);
        text += "\nComponent changes\n";
        text += print(propertyChanges);

        Debug.Log(text);

        return propertyChanges.Any();
    }

    public static PrefabMatchInfo GetPrefabMatchInfo(MonoBehaviour component, GameObject root, string path, string[] matchingProperties)
    {
        var matchInfo = new PrefabMatchInfo { PrefabAssetPath = path, ComponentName = component.gameObject.name };
        return matchInfo;
        string text;


        var objectOverrides = PrefabUtility.GetObjectOverrides(component.gameObject)
            .Where(change => change.instanceObject.GetType() == typeof(OpenUrl));
        var propertyChanges = PrefabUtility.GetPropertyModifications(component.gameObject)
            .Where(p => !p.propertyPath.Contains("m_"));

        var parent = PrefabUtility.GetCorrespondingObjectFromSource(component.gameObject);
        var nearest = PrefabUtility.GetNearestPrefabInstanceRoot(component);
        var outermost = PrefabUtility.GetOutermostPrefabInstanceRoot(component);

        var selfAddedComponents = ParseAddedComponents(component.gameObject);
        var parentAddedComponents = ParseAddedComponents(parent);
        var nearestAddedComponents = ParseAddedComponents(nearest);
        var outermostAddedComponents = ParseAddedComponents(outermost);

        var urlChanges = propertyChanges
                // .Where(p => p.target.GetInstanceID() == c.GetInstanceID())
                .Where(p => matchingProperties.Contains(p.propertyPath))
            // .Where(p => p.target.GetType() == typeof(OpenUrl))
            ;

        if (urlChanges.Any())
            text = $"<b>HAS</b> {urlChanges.Count()} URL overrides of {component.gameObject.name} in {root.name}";
        else
            text =
                $"<b>NO</b> url overrides of {component.gameObject.name} in {root.name}, while propertyChanges={propertyChanges.Count()}";

        var concatObjectOverrides = string.Join(", \n",
            objectOverrides.Select(change =>
                (change.instanceObject.name + " (" + change.instanceObject.name + ")")));

        text += "\n\n" + $"({objectOverrides.Count()}) Object Overrides on: {component.gameObject.name}" + "\n\n" +
                concatObjectOverrides;

        text += $"\n\nAdded Components self={component.gameObject}\n({selfAddedComponents})";
        text += $"\n\nAdded Components parent={parent}\n({parentAddedComponents})";
        text += $"\n\nAdded Components nearest={nearest}\n({nearestAddedComponents})";
        text += $"\n\nAdded Components outermost={outermost}\n({outermostAddedComponents})";

        Debug.Log(text);

        // PrintBlah(null, $"<b>NO</b> url overrides of {component.gameObject.name} in {root.name}. propertyChanges={urlChanges.Count()} hasOverrides=<b>{hasOverrides}</b>");


        // var c = component.gameObject;
        // var routeToRoot = new List<GameObject>();
        //
        // routeToRoot.Add(c);
        //
        // int counter = 0;
        // while (c.transform.parent != null && counter < 10)
        // {
        //     c = c.transform.parent.gameObject; 
        //     
        //     routeToRoot.Add(c);
        //
        //     counter++;
        // }
        //
        // if (counter == 10)
        // {
        //     PrintBlah(null, "<B>OVERFLOW</B>");
        // }
        // else
        // {
        //     routeToRoot.Reverse();
        //     foreach (var o in routeToRoot)
        //     {
        //         bool isRoot = root.GetInstanceID() == o.GetInstanceID();
        //         bool isPrefabSelf = PrefabUtility.IsAnyPrefabInstanceRoot(o);
        //         bool isPrefabVariantSelf = PrefabUtility.IsPartOfVariantPrefab(o);
        //
        //         var propertyChanges = PrefabUtility.GetPropertyModifications(o).ToList()
        //             .Where(p => !p.propertyPath.Contains("m_"))
        //             .Where(p => properties.Contains(p.propertyPath))
        //             .Where(p => p.target.GetType() == typeof(OpenUrl));
        //
        //         bool hasOverrides = false;
        //         
        //         PrintBlah(null, $"{o.name} - {o.GetInstanceID()}. isRoot={isRoot}, isPrefab={isPrefabSelf}, hasOverrides={hasOverrides}");
        //     }
        // }

        return matchInfo;
    }
}