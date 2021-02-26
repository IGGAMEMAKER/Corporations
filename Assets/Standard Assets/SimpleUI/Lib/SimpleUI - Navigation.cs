using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace SimpleUI
{
    public partial class SimpleUI
    {
        // called from GAME UI libs
        public static void OpenUrl_static(string url)
        {
            var eventHandler = FindObjectOfType<SimpleUIEventHandler>();


            if (eventHandler == null)
            {
                Debug.LogError("SimpleUIEventHandler NOT FOUND");

                return;
            }

            var queryIndex = url.IndexOf('?');
            var query = "";

            if (queryIndex >= 0)
            {
                query = url.Substring(queryIndex);
                url = url.Substring(0, queryIndex);
            }

            eventHandler.OpenUrl(url);
        }

        public static void OpenAsset(string path)
        {
            var start = DateTime.Now;

            try
            {
                AssetDatabase.OpenAsset(AssetDatabase.LoadMainAssetAtPath(path));
            }
            catch
            {
                Error("Failed while opening " + path);
            }

            Print($"Opening asset (<b>{Measure(start)}</b>) {path}");
        }

        public void OpenPrefabByAssetPath(string path)
        {
            if (!IsAssetPathExists(path))
            {
                //Debug.LogError("Failed to OpenPrefabByAssetPath() " + path);
                OpenAsset(path);
                return;
            }

            var p1 = prefabs.First(p => p.AssetPath.Equals(path));

            OpenPrefab(p1);
        }

        public void OpenPrefabByGuid(string guid)
        {
            var p = GetPrefabByGuid(guid);

            OpenPrefabByUrl(p.Url);
        }

        public void OpenPrefabByUrl(string url)
        {
            if (!url.StartsWith("/"))
                url = "/" + url;

            var p = GetPrefabByUrl(url);

            OpenPrefab(p);
        }

        public void OpenPrefab(SimpleUISceneType p)
        {
            newPath = p.AssetPath;
            newUrl = p.Url;

            PossiblePrefab = null;
            isDraggedPrefabMode = false;
            isUrlEditingMode = false;
            isConcreteUrlChosen = true;


            LoadReferences(newUrl);

            OpenAsset(newPath);
        }

        //EditorSceneManager.activeSceneChangedInEditMode += EditorSceneManager_activeSceneChangedInEditMode;
        //EditorSceneManager.sceneOpened += EditorSceneManager_sceneOpened;
        ////EditorSceneManager.sceneLoaded += EditorSceneManager_sceneLoaded;

        ////SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        ////SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
        ////SceneManager.activeSceneChanged += SceneManager_sceneChanged;

        //private static void EditorSceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        //{
        //    Debug.Log("Editor scene LOADED");
        //}

        //private static void EditorSceneManager_sceneOpened(Scene scene, OpenSceneMode mode)
        //{
        //    Debug.Log("Editor scene OPENED " + scene.name);
        //    //scene.GetRootGameObjects().First();
        //}

        //private static void EditorSceneManager_activeSceneChangedInEditMode(Scene arg0, Scene arg1)
        //{
        //    Debug.Log($"Editor scene CHANGED from {arg0.name} to {arg1.name}");

        //    var obj = WrapSceneWithMenu();
        //    Selection.activeGameObject = obj;
        //}

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

        //internal static GameObject WrapSceneWithMenu()
        //{
        //    if (FindObjectOfType<DisplayConnectedUrls>() != null)
        //        return null;

        //    if (!IsAssetPathExists(GetOpenedAssetPath()))
        //    {
        //        // this scene was not attached to any url
        //        return null;
        //    }

        //    var go = new GameObject("SimpleUI Menu", typeof(DisplayConnectedUrls));
        //    go.transform.SetAsFirstSibling();

        //    return go;
        //}

        private static void PrefabStage_prefabClosed(PrefabStage obj)
        {
            //Debug.Log("prefab closed");
            DestroyImmediate(obj.prefabContentsRoot.GetComponent<DisplayConnectedUrls>());
        }

        private static void PrefabStage_prefabOpened(PrefabStage obj)
        {
            Debug.Log("Prefab opened: " + obj.prefabContentsRoot.name);

            // Wrap with SimpleUI menus
            obj.prefabContentsRoot.AddComponent<DisplayConnectedUrls>();
            Selection.activeGameObject = obj.prefabContentsRoot;

            var path = obj.assetPath;
            var instance = GetInstance();

            instance.newPath = path;
            instance.newName = GetPrettyNameFromAssetPath(path); // x.Substring(0, ind);

            // choose URL
            ChooseUrlFromPickedPrefab();
            TryToIncreaseCurrentPrefabCounter();
        }

        public static void ChooseUrlFromPickedPrefab()
        {
            //return;
            var path = GetOpenedAssetPath();
            var instance = SimpleUI.GetInstance();

            var urls = instance.prefabs.Where(p => p.AssetPath.Equals(path));

            if (!urls.Any())
            {
                //BoldPrint("NO URLS MATCHING PATH " + path);
                instance.newUrl = "";
            }

            if (urls.Count() == 1)
            {
                instance.newUrl = urls.First().Url;
                instance.isConcreteUrlChosen = true;
            }

            if (urls.Count() > 1)
            {
                // pick first automatically or do nothing?
                instance.isConcreteUrlChosen = false;
            }
        }

        public static void TryToIncreaseCurrentPrefabCounter()
        {
            var instance = SimpleUI.GetInstance();

            if (instance.hasChosenPrefab)
            {
                var pref = instance.prefabs[instance.ChosenIndex];

                pref.Usages++;
                pref.LastOpened = DateTime.Now.Ticks;

                instance.UpdatePrefab(pref);
            }
        }
    }
}