using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleUIEventHandler : MonoBehaviour
{
    // string - url
    // GameObject - prefab
    public Dictionary<string, GameObject> Objects = new Dictionary<string, GameObject>();

    public string CurrentUrl;
    static List<SimpleUISceneType> prefabs => SimpleUI.prefabs; // = new List<NewSceneTypeBlah>();

    private static int counter = 0;
    private static int sameUrlCounter = 0;
    private static int counterThreshold = 8;

    private void Update()
    {
        counter = 0;
        sameUrlCounter = 0;
    }

    void RenderUrls(string NextUrl)
    {
        Print($"<b>OpenUrl {NextUrl}</b> (from {CurrentUrl})");

        var newUrls = ParseUrlToSubRoutes(NextUrl);
        var oldUrls = ParseUrlToSubRoutes(CurrentUrl);

        var commonUrls = oldUrls.Where(removableUrl => newUrls.Contains(removableUrl)).ToList();

        var willRender = newUrls;
        var willHide = oldUrls;

        foreach (var commonUrl in commonUrls)
        {
            willRender.RemoveAll(u => u.Equals(commonUrl));
            willHide.RemoveAll(u => u.Equals(commonUrl));
        }

        foreach (var removableUrl in willHide)
        {
            HidePrefab(removableUrl);
        }

        foreach (var commonUrl in commonUrls)
        {
            RenderPrefab(commonUrl);
        }

        foreach (var newUrl in willRender)
        {
            RenderPrefab(newUrl);
        }

        // RenderPrefab(url);
    }

    public void OpenUrl(string NextUrl, string scriptName)
    {
        counter++;

        if (counter > counterThreshold)
        {
            Debug.LogError($"INFINITE LOOP: {NextUrl} => {CurrentUrl}");
            return;
        }

        if (NextUrl.Equals(CurrentUrl))
        {
            sameUrlCounter++;

            if (sameUrlCounter > counterThreshold / 2)
            {
                Debug.LogError($"SAME URL INFINITE LOOP: {CurrentUrl}");
            }

            return;
        }
        else
        {
            sameUrlCounter = 0;
        }

        if (!SimpleUI.IsUrlExist(NextUrl))
        {
            SimpleUI.AddMissingUrl(NextUrl, scriptName, "");
        }

        RenderUrls(NextUrl);
        
        CurrentUrl = NextUrl;
    }

    void DrawPrefab(string url, bool show)
    {
        var asset = SimpleUI.prefabs.Find(p => p.Url.Equals(url));

        try
        {
            if (asset.AssetType == SceneBlahType.Prefab)
            {
                // prefabs
                var p = GetPrefab(url);

                if (p != null)
                {
                    if (p.activeSelf != show) p.SetActive(show);
                }
            }
            else
            {
                // scenes
                if (asset.AssetType == SceneBlahType.Scene)
                {
                    //Debug.Log("DRAW SCENE " + asset.AssetPath);

                    //var asset2 = AssetDatabase.LoadMainAssetAtPath(asset.AssetPath);
                    var scene = SceneManager.GetSceneByPath(asset.AssetPath);

                    var sceneName = scene.name;
                    var buildIndex = scene.buildIndex;

                    if (show)
                    {
                        if (!scene.isLoaded)
                        {
                            EditorSceneManager.LoadScene(buildIndex, LoadSceneMode.Additive);
                            SceneManager.LoadScene(buildIndex, LoadSceneMode.Additive);

                            EditorSceneManager.SetActiveScene(scene);
                        }
                    }
                    else
                    {
                        if (scene.isLoaded)
                        {
                            EditorSceneManager.UnloadScene(buildIndex);
                            SceneManager.UnloadScene(buildIndex);
                            //SceneManager.LoadScene(buildIndex, LoadSceneMode.Additive);
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    void RenderPrefab(string url)
    {
        Print("Render prefab by url: " + url);

        DrawPrefab(url, true);
    }

    public void HidePrefab(string url)
    {
        Print("HIDE prefab by url: " + url);

        DrawPrefab(url, false);
    }

    void Print(string text)
    {
        //Debug.Log(text);
    }

    List<string> ParseUrlToSubRoutes(string url)
    {
        var urls = new List<string>();

        var cUrl = "/";

        // hide opened stuff
        foreach (var subPath in url.Split('/'))
        {
            if (subPath.StartsWith("/") || cUrl.EndsWith("/"))
                cUrl += subPath;
            else
                cUrl += "/" + subPath;

            urls.Add(cUrl);
        }

        return urls;
    }

    // get asset
    GameObject GetPrefab(string url)
    {
        if (url.Length == 0)
            return null;

        if (!Objects.ContainsKey(url))
        {
            var matching = prefabs.Where(p => p.Url.Equals(url));

            if (!matching.Any())
            {
                Debug.LogError("URL " + url + " not found");
                return null;
            }

            var asset = matching.First();

            var path = asset.AssetPath;

            var obj = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (obj == null)
            {
                Debug.LogError("Prefab in route " + path + " not found");
                return null;
            }

            Objects[url] = Instantiate(obj, transform);
        }

        return Objects[url];
    }
}