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


public partial class SimpleUI
{
    public static void OpenAssetByPath(string path)
    {
        AssetDatabase.OpenAsset(AssetDatabase.LoadMainAssetAtPath(path));
    }

    public static void OpenPrefabByAssetPath(string path)
    {
        if (!SimpleUI.IsAssetPathExists(path))
        {
            //Debug.LogError("Failed to OpenPrefabByAssetPath() " + path);
            OpenAssetByPath(path);
            return;
        }

        var p1 = prefabs.First(p => p.AssetPath.Equals(path));

        OpenPrefab(p1);
    }
    public static void OpenPrefabByUrl(string url)
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

        // calculate previous DisplayConnectuedUrlsEditor.OnEnable() here
        LoadReferences(newUrl);

        OpenAssetByPath(newPath);
    }

    //private static void EditorSceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    //{
    //    Debug.Log("Editor scene LOADED");
    //}

    private static void EditorSceneManager_sceneOpened(Scene scene, OpenSceneMode mode)
    {
        Debug.Log("Editor scene OPENED " + scene.name);
        //scene.GetRootGameObjects().First();
    }

    private static void EditorSceneManager_activeSceneChangedInEditMode(Scene arg0, Scene arg1)
    {
        Debug.Log($"Editor scene CHANGED from {arg0.name} to {arg1.name}");

        var obj = WrapSceneWithMenu();
        Selection.activeGameObject = obj;
    }

    //private static void SceneManager_sceneChanged(Scene arg0, Scene arg1)
    //{
    //    Debug.Log($"Scene changed from {arg0.name} to {arg1.name}");

    //    //SceneManager_sceneUnloaded(arg0);
    //    //SceneManager_sceneLoaded(arg0, LoadSceneMode.Additive);
    //}

    //private static void SceneManager_sceneUnloaded(Scene arg0)
    //{
    //    Debug.Log("Scene unloaded");

    //    DestroyImmediate(FindObjectOfType<DisplayConnectedUrls>());
    //}

    //private static void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    //{
    //    Debug.Log("Scene loaded");

    //    var go = WrapSceneWithMenu();
    //    Selection.activeGameObject = go;
    //}

    static GameObject WrapSceneWithMenu()
    {
        if (FindObjectOfType<DisplayConnectedUrls>() != null)
            return null;

        if (!IsAssetPathExists(GetCurrentAssetPath()))
        {
            // this scene was not attached to any url
            return null;
        }

        var go = new GameObject("SimpleUI Menu", typeof(DisplayConnectedUrls));
        go.transform.SetAsFirstSibling();

        return go;
    }

    private static void PrefabStage_prefabClosed(PrefabStage obj)
    {
        Debug.Log("prefab closed");
        DestroyImmediate(obj.prefabContentsRoot.GetComponent<DisplayConnectedUrls>());
    }

    private static void PrefabStage_prefabOpened(PrefabStage obj)
    {
        Debug.Log("Prefab opened: " + obj.prefabContentsRoot.name);

        // Wrap with SimpleUI menues
        obj.prefabContentsRoot.AddComponent<DisplayConnectedUrls>();
        Selection.activeGameObject = obj.prefabContentsRoot;


        newPath = obj.assetPath;
        newName = GetPrettyNameFromAssetPath(newPath); // x.Substring(0, ind);

        // choose URL
        ChooseUrlFromPickedPrefab();
        TryToIncreaseCurrentPrefabCounter();
    }

    static void ChooseUrlFromPickedPrefab()
    {
        var path = GetOpenedAssetPath();
        var urls = prefabs.Where(p => p.AssetPath.Equals(path));

        if (urls.Count() == 0)
        {
            newUrl = "";
        }

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

}