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
    private static int sameUrlCounter = 0;
    private static int counterThreshold = 8;

    // public void OpenTab(string url)
    // {
    //     var trimmedUrl = url.StartsWith("/") ? url.TrimStart('/') : url; 
    //     
    //     OpenUrl(CurrentUrl + "/" + trimmedUrl);
    // }

    private void Update()
    {
        counter = 0;
        sameUrlCounter = 0;
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

    void PrintParsedRoute(List<string> urls, string label)
    {
        Debug.Log(label + $": ({urls.Count})" + string.Join("\n", urls));
    }
    
    public void OpenUrl(string NextUrl)
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
        
        LoadData();
        
        Debug.Log($"<b>OpenUrl {NextUrl}</b> (from {CurrentUrl})");

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

        // PrintParsedRoute(commonUrls, "no change");
        //
        // PrintParsedRoute(willHide, "will HIDE");
        // PrintParsedRoute(willRender, "will RENDER");
        
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

        CurrentUrl = NextUrl;
    }

    void DrawPrefab(string url, bool show)
    {
        var p = GetPrefab(url);

        if (p != null)
        {
            if (p.activeSelf != show) p.SetActive(show);
        }
    }

    void RenderPrefab(string url)
    {
        Debug.Log("Render prefab by url: " + url);

        DrawPrefab(url, true);
    }

    public void HidePrefab(string url)
    {
        Debug.Log("HIDE prefab by url: " + url);

        DrawPrefab(url, false);
    }

    GameObject GetPrefab(string url)
    {
        try
        {
            if (url.Length == 0)
                return null;
            
            if (!Objects.ContainsKey(url))
            {
                if (!prefabs.Any(p => p.Url.Equals(url)))
                {
                    Debug.LogError("URL " + url + " not found");
                    return null;
                }

                var pre = prefabs.First(p => p.Url.Equals(url));
                
                var obj = AssetDatabase.LoadAssetAtPath<GameObject>(pre.AssetPath);
                if (obj == null)
                {
                    Debug.LogError("Prefab in route " + pre.AssetPath + " not found");
                    return null;
                }
                
                // Objects[url] = AssetDatabase.GetMainAssetTypeAtPath(pre.AssetPath));

                Objects[url] = Instantiate(obj, transform);
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