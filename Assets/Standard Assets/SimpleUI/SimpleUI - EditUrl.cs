using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

// editing route mode
public partial class SimpleUI
{
    static string searchUrl = "";
    static Vector2 searchScrollPosition = Vector2.zero;

    public static bool renameSubroutes = true;
    public static string newEditingUrl = "";

    public static string newUrl = "";
    public static string newName = "";
    public static string newPath = "";

    void RenderChosenPrefab()
    {
        if (!isConcreteUrlChosen)
        {
            // pick concrete URL
            RenderUrlsWhichAreAttachedToSamePrefab();
        }
        else
        {
            if (isUrlEditingMode)
                RenderEditingPrefabMode();
            else
                RenderLinkToEditing();
        }
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

    void RenderLinkToEditing()
    {
        var index = ChosenIndex;
        var prefab = prefabs[index];

        Label(prefab.Url);

        if (Button("Edit prefab"))
        {
            isUrlEditingMode = true;

            newUrl = prefab.Url;
            newEditingUrl = newUrl;
            newPath = prefab.AssetPath;
            newName = prefab.Name;
        }

        Space();
        RenderPrefabs();
    }

    void RenameUrl(string route, string from, string to)
    {
        var matches = WhatUsesComponent(route, allReferencesFromAssets);

        //bool savingWillBeSafe = true;
        //try
        //{
        //    AssetDatabase.StartAssetEditing();
        //    AssetDatabase.SaveAssets();
        //}
        //catch (System.Exception ex)
        //{
        //    savingWillBeSafe = false;
        //    Debug.LogError(ex);
        //}
        //finally
        //{
        //    AssetDatabase.StopAssetEditing();
        //}

        //if (!savingWillBeSafe)
        //{
        //    Debug.Log("Renaming failed cause saving assets via AssetDatabase will cause errors (listed above)");
        //    return;
        //}

        try
        {
            AssetDatabase.StartAssetEditing();

            Debug.Log($"Replacing {from} to {to} in {route}");

            foreach (var match in matches)
            {
                if (match.IsNormalPartOfNestedPrefab)
                    continue;

                var asset = match.Asset;
                //var asset = AssetDatabase.LoadAssetAtPath<GameObject>(match.PrefabAssetPath);
                //var asset = AssetDatabase.OpenAsset(AssetDatabase.LoadMainAssetAtPath(match.PrefabAssetPath));

                var component = match.Component;

                // edit URL property
                bool addedSlash = false;
                var formattedUrl = component.Url;
                if (!formattedUrl.StartsWith("/"))
                {
                    addedSlash = true;
                    formattedUrl = "/" + formattedUrl;
                }
                var newUrl2 = formattedUrl.Replace(from, to);
                if (addedSlash)
                    newUrl2 = newUrl2.TrimStart('/');

                Debug.Log($"Renaming {component.Url} => {newUrl2} on component in {match.PrefabAssetPath}");
                component.Url = newUrl2;


                // saving changes
                if (isSceneAsset(match.PrefabAssetPath))
                {
                    // if scene
                    // save change in scene

                    // https://forum.unity.com/threads/scripted-scene-changes-not-being-saved.526453/

                    EditorUtility.SetDirty(component);
                    Debug.Log("Set scene as dirty");
                    //var saved = EditorSceneManager.SaveScene(asset.scene);

                    //if (saved)
                    //    Debug.Log("SUCCEED Save changes in scene: " + match.PrefabAssetPath);
                    //else
                    //    Debug.Log("FAILED Save changes in scene: " + match.PrefabAssetPath);
                }

                if (isPrefabAsset(match.PrefabAssetPath))
                {
                    EditorUtility.SetDirty(component);
                    // if prefab
                    // save change in prefab

                    GameObjectUtility.RemoveMonoBehavioursWithMissingScript(asset);

                    PrefabUtility.SaveAsPrefabAsset(asset, match.PrefabAssetPath);
                    PrefabUtility.UnloadPrefabContents(asset);
                }
            }

        }
        finally
        {
            AssetDatabase.SaveAssets();

            //EditorSceneManager.GetSceneManagerSetup().First()
            AssetDatabase.StopAssetEditing();

            var prefab = GetPrefabByUrl(route);
            prefab.Url = newEditingUrl;

            UpdatePrefab(prefab);
        }
    }

    void RenderStatButtons(SimpleUISceneType pref)
    {
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
    }

    void RenderRenameUrlButton(SimpleUISceneType prefab)
    {
        Space();

        renameSubroutes = EditorGUILayout.ToggleLeft("Rename subroutes too", renameSubroutes);

        Space();
        if (renameSubroutes)
            EditorGUILayout.HelpBox("Renaming this url will lead to renaming these urls too...", MessageType.Warning);
        else
            EditorGUILayout.HelpBox("Will only rename THIS url", MessageType.Warning);

        List<string> RenamingUrls = new List<string>();
        List<string> RenamingCodeUrls = new List<string>();

        if (renameSubroutes)
        {
            Space();
            var subroutes = GetSubUrls(prefab.Url, true);

            RenamingUrls.Add(prefab.Url);

            foreach (var route in subroutes)
            {
                RenamingUrls.Add(route.Url);
            }

            // render
            foreach (var route in RenamingUrls)
            {
                BoldLabel(route);
            }

            //foreach (var script in referencesFromCode)
            //{
            //    RenamingCodeUrls.Add(script)
            //}
        }

        var phrase = renameSubroutes ? "Rename url & subUrls" : "Rename THIS url";

        var matches = WhatUsesComponent(newUrl, allReferencesFromAssets);


        // references from prefabs & scenes
        var names = matches.Select(m => $"<b>{SimpleUI.GetPrettyAssetType(m.PrefabAssetPath)}</b> " + SimpleUI.GetTrimmedPath(m.PrefabAssetPath)).ToList();
        var routes = matches.Select(m => m.PrefabAssetPath).ToList();

        // references from code
        foreach (var occurence in referencesFromCode)
        {
            names.Add($"<b>Code</b> {SimpleUI.GetTrimmedPath(occurence.ScriptName)}");
            routes.Add(occurence.ScriptName);
        }

        Space();
        if (Button(phrase))
        {
            if (EditorUtility.DisplayDialog("Do you want to rename url " + prefab.Url, "This action will rename url and subUrls in X prefabs, Y scenes and Z script files.\n\nPRESS CANCEL IF YOU HAVE UNSAVED PREFAB OR SCENE OR CODE CHANGES", "Rename", "Cancel"))
            {
                Print("Rename starts now!");

                foreach (var url in RenamingUrls)
                {
                    Print("Scanning URL " + url);
                    RenameUrl(url, newUrl, newEditingUrl);
                    Print("----------------");
                }
            }
        }

        //EditorUtility.DisplayProgressBar("Renaming url", "Info", UnityEngine.Random.Range(0, 1f));
    }

    void RenderEditingPrefabMode()
    {
        var index = ChosenIndex;
        var prefab = prefabs[index];

        Label(prefab.Url);

        if (Button("Go back"))
        {
            isUrlEditingMode = false;
        }

        var prevUrl = newUrl;
        var prevName = newName;
        var prevPath = newPath;

        Space();



        Label("Edit url");
        newEditingUrl = EditorGUILayout.TextField("Url", newEditingUrl);

        if (newEditingUrl.Length > 0)
        {
            newName = EditorGUILayout.TextField("Name", newName);

            if (newName.Length > 0)
            {
                newPath = EditorGUILayout.TextField("Asset Path", newPath);
            }
        }



        // if data changed, save it
        if (!prevPath.Equals(newPath) || !prevName.Equals(newName))
        {
            //prefab.Url = newEditingUrl;
            prefab.Name = newName;
            prefab.AssetPath = newPath;

            UpdatePrefab(prefab);
        }

        // if Url changed, rename everything
        if (!newUrl.Equals(newEditingUrl))
        {
            RenderRenameUrlButton(prefab);
        }

        Space();
        RenderRootPrefab();
        RenderSubroutes();

        // TODO url or path?
        // opened another url
        if (!newPath.Equals(prevPath))
            return;

        RenderStatButtons(prefab);

        Space(450);
        if (GUILayout.Button("Remove URL"))
        {
            prefabs.RemoveAt(index);
            SaveData();
        }
    }
}
