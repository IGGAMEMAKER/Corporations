using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class SimpleUIEventHandler : MonoBehaviour
{
    // string - url
    // GameObject - prefab
    public Dictionary<string, GameObject> Objects = new Dictionary<string, GameObject>();

    public string CurrentUrl;
    static List<SimpleUISceneType> prefabs; // = new List<NewSceneTypeBlah>();

    private static int counter = 0; 

    // void Start()
    // {
    //     LoadData();
    // }

    // public void OpenTab(string url)
    // {
    //     var trimmedUrl = url.StartsWith("/") ? url.TrimStart('/') : url; 
    //     
    //     OpenUrl(CurrentUrl + "/" + trimmedUrl);
    // }

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

    void PrintParsedRoute(List<string> urls, string label)
    {
        Debug.Log(label + $": ({urls.Count})" + string.Join("\n", urls));
    }
    
    public void OpenUrl(string NextUrl)
    {
        counter++;

        if (counter > 10)
        {
            Debug.LogError("INFINITE LOOP");
            return;
        }
        
        if (NextUrl.Equals(CurrentUrl))
            return;
        
        LoadData();
        
        Debug.Log($"<b>OpenUrl {NextUrl}</b> (from {CurrentUrl})");

        var newUrls = ParseUrlToSubRoutes(NextUrl);
        var oldUrls = ParseUrlToSubRoutes(CurrentUrl);

        // PrintParsedRoute(oldUrls, "OLD routes");
        // PrintParsedRoute(newUrls, "NEW routes");

        var commonUrls = oldUrls.Where(removableUrl => newUrls.Contains(removableUrl)).ToList();

        var willRender = newUrls;
        var willHide = oldUrls;
        
        foreach (var commonUrl in commonUrls)
        {
            willRender.RemoveAll(u => u.Equals(commonUrl));
            willHide.RemoveAll(u => u.Equals(commonUrl));
        }

        PrintParsedRoute(commonUrls, "no change");

        PrintParsedRoute(willHide, "will HIDE");
        PrintParsedRoute(willRender, "will RENDER");
        
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
        
        // if attempt overflow, render only necessary stuff
        // if (!canRenderStuff)
            // RenderPrefab(url);

        CurrentUrl = NextUrl;
    }

    void RenderPrefab(string url, bool isTest = false)
    {
        Debug.Log("Render prefab by url: " + url);

        var p = GetPrefab(url, isTest);
        
        if (p != null && !p.activeSelf)
            p.SetActive(true);
    }

    void HidePrefab(string url, bool isTest = false)
    {
        Debug.Log("HIDE prefab by url: " + url);

        var p = GetPrefab(url, isTest);
        
        if (p != null && p.activeSelf)
            p.SetActive(false);
    }

    GameObject GetPrefab(string url, bool isTestUrl = false)
    {
        try
        {
            if (url.Length == 0)
                return null;
            
            if (!Objects.ContainsKey(url))
            {
                if (isTestUrl) Debug.Log("Never loaded ROOT as prefab");
                
                if (!prefabs.Any(p => p.Url.Equals(url)))
                {
                    Debug.LogError("URL " + url + " not found");
                    return null;
                }

                var pre = prefabs.First(p => p.Url.Equals(url));
                
                if (isTestUrl) Debug.Log("Found data for ROOT prefab");

                var obj = AssetDatabase.LoadAssetAtPath<GameObject>(pre.AssetPath);
                if (obj == null)
                {
                    Debug.LogError("Prefab in route " + pre.AssetPath + " not found");
                    return null;
                }
                
                if (isTestUrl) Debug.Log("Loaded ROOT prefab from assets " + pre.AssetPath + " " + obj.name);
                if (isTestUrl) return null;
                
                // Objects[url] = Instantiate(AssetDatabase.GetMainAssetTypeAtPath(pre.AssetPath));

                Objects[url] = Instantiate(obj, transform);
                
                if (isTestUrl) Debug.Log("INSTANTIATED ROOT prefab");
                if (isTestUrl) return null;
            }
            
            return Objects[url];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            
            return null;
        }
    }

    static void LoadData()
    {
        if (prefabs != null)
            return;
        
        var fileName = "SimpleUI/SimpleUI.txt";

        List<SimpleUISceneType> obj = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SimpleUISceneType>>(
            File.ReadAllText(fileName), new Newtonsoft.Json.JsonSerializerSettings
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            });

        prefabs = obj ?? new List<SimpleUISceneType>();
    }
}