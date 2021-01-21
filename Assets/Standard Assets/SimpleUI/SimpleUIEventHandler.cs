﻿using System;
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

        RenderUrls(NextUrl);
        
        CurrentUrl = NextUrl;
    }

    void DrawPrefab(string url, bool show)
    {
        try
        {
            var p = GetPrefab(url);

            if (p != null)
            {
                if (p.activeSelf != show) p.SetActive(show);
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

            var path = matching.First().AssetPath;

            var obj = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (obj == null)
            {
                Debug.LogError("Prefab in route " + path + " not found");
                return null;
            }

            // Objects[url] = AssetDatabase.GetMainAssetTypeAtPath(pre.AssetPath));

            Objects[url] = Instantiate(obj, transform);
        }

        return Objects[url];
    }
}