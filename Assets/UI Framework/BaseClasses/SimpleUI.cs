using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Core;
using NUnit.Framework;
using UnityEngine;


using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using Object = UnityEngine.Object;

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

// void RenderExample()
// {
// // myString = EditorGUILayout.TextField ("Text Field", myString);
//         
// // groupEnabled = EditorGUILayout.BeginToggleGroup ("Optional Settings", groupEnabled);
// // myBool = EditorGUILayout.Toggle ("Toggle", myBool);
// // myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
// // EditorGUILayout.EndToggleGroup ();
// }


public class SimpleUI : EditorWindow
{
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    private Vector2 recentPrefabsScrollPosition = Vector2.zero;

    private static string newUrl = "";
    private static string newName = "";
    private static string newPath = "";
    
    private static string draggedUrl = "";
    private static string draggedName = "";
    private static string draggedPath = "";
    
    static List<SimpleUISceneType> prefabs; // = new List<NewSceneTypeBlah>();

    private static int ChosenIndex => prefabs.FindIndex(p => p.AssetPath.Equals(newPath));
    private static bool hasChosenPrefab => ChosenIndex >= 0;

    private bool showTopPrefabs = false;
    private bool showRecentPrefabs = false;
    private bool showAllPrefabs = false;
    private bool isDraggedPrefabMode = false;
    private bool isDraggedGameObjectMode = false;

    private static GameObject CurrentObject;

    private GameObject PossiblePrefab;
    private static string possiblePrefabName = "";
    
    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/SIMPLE UI")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(SimpleUI), false, "Simple UI", true);
    }
    
    static SimpleUI()
    {
        PrefabStage.prefabStageOpened += PrefabStage_prefabOpened;
    }

    private static void PrefabStage_prefabOpened(PrefabStage obj)
    {
        Debug.Log("Prefab opened: " + obj.prefabContentsRoot.name);

        newPath = obj.prefabAssetPath;
        newName = GetPrettyNameFromAssetPath(newPath); // x.Substring(0, ind);

        TryToIncreaseCurrentPrefabCounter();
    }

    void OnGUI()
    {
        recentPrefabsScrollPosition = GUILayout.BeginScrollView(recentPrefabsScrollPosition);
        GUILayout.Label ("SIMPLE UI", EditorStyles.largeLabel);

        RenderPrefabs();

        if (isDraggedGameObjectMode)
            RenderMakingAPrefabFromGameObject();
        else if (isDraggedPrefabMode)
            RenderAddingNewRouteFromDraggedPrefab();
        else if (hasChosenPrefab)
            RenderEditingPrefab();
        else
            RenderAddingNewRoute();

        HandleDragAndDrop();
        
        GUILayout.EndScrollView();
    }

    private void RenderMakingAPrefabFromGameObject()
    {
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
                EditorGUILayout.LabelField($"!!!Bad prefab name: You cannot name new prefab GameObject, cause it's easy to confuse name");
            
            if (hasSameNamePrefabAlready)
                EditorGUILayout.LabelField($"!!!Bad prefab name: prefab ({path}) already exists)");
            // EditorGUILayout.LabelField($"!!!Bad prefab name: empty: {isEmpty}, name=gameobject: {isDefaultName}, prefab already exists: {hasSameNamePrefabAlready}");
        }

        if (isNameOK && GUILayout.Button("CREATE prefab!"))
        {
            PrefabUtility.SaveAsPrefabAssetAndConnect(PossiblePrefab, path, InteractionMode.UserAction, out var success);
        
            Debug.Log("Prefab saving " + success);

            if (success)
            {
                isDraggedGameObjectMode = false;
                HandleDraggedPrefab(PossiblePrefab);
            }
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

    void OpenPrefab(SimpleUISceneType p)
    {
        newPath = p.AssetPath;

        var asset = AssetDatabase.LoadMainAssetAtPath(p.AssetPath);

        PossiblePrefab = null;
        isDraggedPrefabMode = false;
                
        AssetDatabase.OpenAsset(asset);
        Selection.activeObject = asset;
        // AssetDatabase.OpenAsset(AssetDatabase.LoadMainAssetAtPath(p.AssetPath));

        WhatUsesComponent<OpenUrl>();
    }



    void RenderRecentPrefabs()
    {
        var recent = prefabs.OrderByDescending(pp => pp.LastOpened).Take(6);

        GUILayout.Label ("Recent prefabs", EditorStyles.boldLabel);
        RenderPrefabs(recent);
    }

    void RenderFavoritePrefabs()
    {
        var top = prefabs.OrderByDescending(pp => pp.Usages).Take(4);

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
    string GetValidatedUrl(string url)
    {
        if (!url.StartsWith("/"))
            return url.Insert(0, "/");
        
        return url;
    }
    
    void AddPrefab(string ururu, string papapath, string nananame)
    {
        var p = new SimpleUISceneType(ururu, papapath, nananame) {LastOpened = DateTime.Now.Ticks};

        prefabs.Add(p);
    }
    
    static string GetPrettyNameFromAssetPath(string assetPa)
    {
        var x = assetPa.Split('/').Last();
        var ind = x.LastIndexOf(".prefab");
         
        return x.Substring(0, ind);
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

            
            // GUIStyle style = new GUIStyle ();
            GUIStyle style = GUI.skin.FindStyle("Button");
            style.richText = true;

            // if (GUILayout.Button(p.Name))
            // if (GUILayout.Button($"{p.Name}   ---   <b>{p.Url}</b>", style))
            if (GUILayout.Button($"<b>{p.Name}</b>\n{p.Url}", style))
            {
                OpenPrefab(p);
            }
            
            GUI.contentColor = c;
            GUI.color = c;
            GUI.backgroundColor = c;            
        }
    }
    
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
        if (prefabs != null && prefabs.Count == 0)
            return;
        
        var fileName = "SimpleUI/SimpleUI.txt";

        List<SimpleUISceneType> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SimpleUISceneType>>(File.ReadAllText(fileName), new Newtonsoft.Json.JsonSerializerSettings
        {
            TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
        });

        prefabs = obj ?? new List<SimpleUISceneType>();
    }
    
    
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


    public static bool HasNoPrefabsBetweenObjects(MonoBehaviour component, GameObject root)
    {
        // is directly attached to root prefab object with no in between prefabs
        
        // root GO
        // -> NonPrefab1 GO
        // -> NonPrefab2 GO
        // -> -> NonPrefab3 GO with our component

        // PrefabUtility.IsAnyPrefabInstanceRoot(component.gameObject);
        // PrefabUtility.IsOutermostPrefabInstanceRoot(component.gameObject);
        // PrefabUtility.IsPartOfAnyPrefab(component.gameObject);
        // PrefabUtility.IsPartOfPrefabAsset(component.gameObject);
        // PrefabUtility.IsPartOfPrefabInstance(component.gameObject);
        // PrefabUtility.IsPartOfRegularPrefab(component.gameObject);
        // PrefabUtility.IsPartOfNonAssetPrefabInstance(component.gameObject);

        var rootOf = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(root);
        var pathOfComponent = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(component);

        var r = rootOf.Equals(pathOfComponent);

        var attachedStr = r ? "IS" : "Is NOT";
        // PrintArbitraryInfo(null, $"{component.gameObject.name} {attachedStr} Directly attached to {root.gameObject.name}");

        if (!r)
        {
            // PrintArbitraryInfo(null, $"cause it's part of {pathOfComponent} prefab");
        }

        return r;

        var nearestRoot = PrefabUtility.GetNearestPrefabInstanceRoot(component);
        var outerRoot = PrefabUtility.GetOutermostPrefabInstanceRoot(component);

        var rootId = root.GetInstanceID();
        
        var nearestId = nearestRoot.GetInstanceID();
        var outerId = outerRoot.GetInstanceID();
        
        var result = nearestId == outerId;
        
        // Debug.Log($"isDirectly attached to root prefab? <b>{result}</b> c={component.gameObject.name}, root={root.gameObject.name}\n" 
        //           + $"\n<b>nearest prefab ({nearestId}): </b>" + nearestRoot.name 
        //           + $"\n<b>outer prefab ({outerId}): </b>" + outerRoot.name 
        //           + $"\n\nroot instance ID={rootId}");

        return result;
    }

    public static bool IsAddedAsOverridenComponent(MonoBehaviour component, GameObject root)
    {
        return PrefabUtility.IsAddedComponentOverride(component);
    }
    
    public static bool HasOverridenProperties(MonoBehaviour component, GameObject root)
    {
        var result = PrefabUtility.HasPrefabInstanceAnyOverrides(component.gameObject, false);
        var nearestPrefab = PrefabUtility.GetCorrespondingObjectFromSource(component);
        // PrefabUtility.GetPropertyModifications()
        
        Debug.Log("Corresponding object for " + component.gameObject.name + " is " + nearestPrefab.name);

        var str = result ? "HAS" : "Has NO";
        
        // PrintArbitraryInfo(null, $"{component.gameObject.name} {str} overrides"); // ({root.gameObject.name})
        
        return result;
    }
    
    public class PrefabComponentMatcher
    {
        
    }
    
    public class PrefabMatchInfo
    {
        public string PrefabAssetPath;
        public string ComponentName;

        public bool IsDirectMatch; // with no nested prefabs, can apply changes directly. (Both on root and it's childs)
        
        public bool IsNormalPartOfNestedPrefab; // absolutely normal prefab part with NO overrides. No actions required
        public bool IsOverridenPartOfNestedPrefab => IsOverridenAsAddedComponent || IsOverridenAsComponentProperty; // is overriden somehow: maybe there is not saved Added component or Overriden Parameters in component itself

        public bool IsOverridenAsComponentProperty;
        public bool IsOverridenAsAddedComponent;
    }
    
    public static void WhatUsesComponent<T>()
    {
        var typeToSearch = typeof(T);

        Debug.Log("Finding all Prefabs and scenes that have the component" + typeToSearch + "…");

        var excludeFolders = new[] {"Assets/Standard Assets"};
        var guids = AssetDatabase.FindAssets("t:prefab", new []{ "Assets"});
        // var guids = AssetDatabase.FindAssets("t:scene t:prefab", new []{ "Assets"});

        var paths = guids.Select(AssetDatabase.GUIDToAssetPath).ToList();
        var removedPaths = paths.RemoveAll(guid => excludeFolders.Any(guid.Contains));

        var matchingPrefabs = new List<string>();
        List<PrefabMatchInfo> matchingComponents = new List<PrefabMatchInfo>();
        
        var componentNotes = new List<string>();
        
        foreach (var path in paths)
        {
            // Debug.Log("Found prefab: " + path);

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
                matchingPrefabs.Add(path);
                // PrintBlah(componentNotes, "<b>----------------------------</b>");
                PrintBlah(componentNotes, "Found component " + typeToSearch + " in file <b>" + path + "</b>");
            }

            foreach (var component1 in components)
            {
                var component = component1 as MonoBehaviour;

                var matchInfo = new PrefabMatchInfo {PrefabAssetPath = path, ComponentName = component.gameObject.name};

                bool isAttachedToRootPrefab = HasNoPrefabsBetweenObjects(component, asset);
                bool isAttachedToNestedPrefab = !isAttachedToRootPrefab;

                if (isAttachedToRootPrefab)
                {
                    // PrintBlah(componentNotes, $"{component.gameObject.name}.{typeToSearch} is attached to <b>ROOT</b> without any overrides and complex prefab stuff");
                    matchInfo.IsDirectMatch = true;
                }

                if (isAttachedToNestedPrefab)
                {
                    bool isAddedAndMarkedBlue = IsAddedAsOverridenComponent(component, asset);
                    bool isPartOfNestedPrefab = !isAddedAndMarkedBlue;

                    if (isAddedAndMarkedBlue)
                    {
                        // added, but not saved in that prefab
                        // so prefab will not see this component in itself
                        
                        // but you need to keep an eye if you will make changes in this component
                 
                        // PrintBlah(componentNotes, $"{component.gameObject.name}.{typeToSearch} is <b>{Visuals.Colorize("ADDITIVELY", Color.blue)}</b> attached to <b>nested prefab</b>");
                        matchInfo.IsOverridenAsAddedComponent = true;
                    }

                    if (isPartOfNestedPrefab)
                    {
                        // so you might need to check Overriden Properties
                        
                        // PrintBlah(componentNotes, $"{component.gameObject.name}.{typeToSearch} is <b>Normally</b> attached to <b>nested prefab</b>");

                        if (HasOverridenProperties(component, asset))
                        {
                            // PrintBlah(componentNotes, "Has some overriden properties");
                            matchInfo.IsOverridenAsComponentProperty = true;
                        }
                        else
                        {
                            matchInfo.IsNormalPartOfNestedPrefab = true;
                        }
                    }
                }
                
                matchingComponents.Add(matchInfo);
            }
        }
        
        Debug.Log($"<b>Found {removedPaths} removed guids</b>");
        foreach (var matchingPrefab in matchingPrefabs)
        {
            // Debug.Log("Found component " + typeToSearch + " in file <b>" + matchingPrefab + "</b>");
        }

        foreach (var componentNote in componentNotes)
        {
            Debug.Log(componentNote);
        }

        foreach (var matchingComponent in matchingComponents)
        {
            if (matchingComponent.IsDirectMatch)
            {
                PrintBlah(null, $"Directly occurs as {matchingComponent.ComponentName} in " + matchingComponent.PrefabAssetPath + " so you can upgrade it and safely save ur prefab");
            }
            else if (matchingComponent.IsOverridenPartOfNestedPrefab)
            {
                if (matchingComponent.IsOverridenAsAddedComponent)
                {
                    PrintBlah(null, $"{matchingComponent.ComponentName} Is <b>ADDITIONALLY ATTACHED</b> to some nested prefab in {matchingComponent.PrefabAssetPath}");
                }
                else if (matchingComponent.IsOverridenAsComponentProperty)
                {
                    PrintBlah(null, $"root prefab ({matchingComponent.PrefabAssetPath}) <b>OVERRIDES VALUES</b> of component {matchingComponent.ComponentName}");
                }
            }
            else if (matchingComponent.IsNormalPartOfNestedPrefab)
            {
                PrintBlah(null, "Occurs as nested prefab in " + matchingComponent.PrefabAssetPath);
                PrintArbitraryInfo(null, "No changes necessary");
            }
        }


        // int count = 0;

    //     for (int i = 0; i g.GetComponentsInChildren(typeToSearch, true)).ToArray();
    // }
    // else {
    //     myObjs = AssetDatabase.LoadAllAssetsAtPath(myObjectPath);
    // }

    // if (EditorUtility.DisplayCancelableProgressBar($"Searching in scenes/prefabs… {count} matches. Progress: {i} / {guids.Length}. Current: {asset.name}, {i} / (guids.Length – 1f))) {
    //     goto End;
    // }

    // foreach (Object thisObject in myObjs) {
    //     if (typeToSearch.IsAssignableFrom(thisObject.GetType())) {
    //         Debug.Log(“” + typeString + ” Found in ” + thisObject.name + ” at ” + myObjectPath, asset);
    //         count++;
    //         break;
    //     }
    // }
    //
    // if (asset is SceneAsset) {
    //     EditorSceneManager.ClosePreviewScene(s);
    // }

}

    static void PrintArbitraryInfo(List<string> list, string text)
    {
        Debug.Log(Visuals.Colorize(text, Color.gray));
    }
    static void PrintBlah(List<string> list, string text)
    {
        Debug.Log(text);
        // componentNotes.Add($"{typeToSearch} appears in {path}, cause it is <b>STRONGLY attached</b> to <b>subPrefab</b>");
    }

    static void UpdatePrefab(SimpleUISceneType prefab)
    {
        if (!hasChosenPrefab)
            return;
        
        prefabs[ChosenIndex] = prefab;
        SaveData();
    }
}