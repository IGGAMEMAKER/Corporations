using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DisplayConnectedUrls))]
public class DisplayConnectedUrlsEditor : Editor
{
    int w = 225;
    int h = 150;
    int off = 5;

    static int routeSelected = -1;
    static int referenceSelected = -1;
    static int referenceFromSelected = -1;

    static Vector2 scrollPosition = Vector2.zero;
    static Vector2 scrollPosition2 = Vector2.zero;
    static Vector2 scrollPosition3 = Vector2.zero;

    SimpleUI ui;
    //Handles.Button(Vector3.one * 10, Quaternion.identity, 200, 200, Handles.RectangleHandleCap);

    // Cached data
    List<SimpleUI.PrefabMatchInfo> matches1;

    private void OnSceneGUI()
    {
        ui = EditorWindow.GetWindow<SimpleUI>();
        var currentUrl = ui.GetCurrentUrl();


        RenderUpperAndLowerRoutes(currentUrl);

        RenderReferencesToUrl(currentUrl);
        RenderReferencesFromUrl(currentUrl);
    }

    private void OnEnable()
    {
        ui = EditorWindow.GetWindow<SimpleUI>();
        var currentUrl = ui.GetCurrentUrl();

        matches1 = SimpleUI.WhatUsesComponent<OpenUrl>();
    }

    private void OnDisable()
    {
        ClearData();
    }

    void ClearData()
    {
        matches1.Clear();

        routeSelected = -1;
        referenceSelected = -1;
        referenceFromSelected = -1;
    }

    void Label(string text)
    {
        SimpleUI.LightLabel(text);
    }

    void RenderReferencesFromUrl(string currentUrl)
    {
        GUILayout.BeginArea(new Rect(Screen.width - w - off, off + h + off, w, h));
        //GUILayout.BeginArea(new Rect(off, off + h, w, h));
        var matches = matches1.Where(m => m.PrefabAssetPath.Equals(ui.GetCurrentAssetPath())).ToList();

        if (matches.Any())
            Label("References FROM url");

        scrollPosition3 = GUILayout.BeginScrollView(scrollPosition3);

        var prevRoute = referenceSelected;
        referenceSelected = GUILayout.SelectionGrid(referenceSelected, matches.Select(m => SimpleUI.GetPrettyNameForExistingUrl("/" + m.URL)).ToArray(), 1);

        if (prevRoute != referenceSelected)
        {
            ui.OpenPrefab(matches[referenceSelected].URL);
            referenceSelected = -1;
        }

        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    void RenderReferencesToUrl(string currentUrl)
    {
        GUILayout.BeginArea(new Rect(off, off, w, h));

        var matches = matches1.Where(m => m.URL.Equals(ui.GetCurrentUrl().TrimStart('/'))).ToList();

        if (matches.Any())
            Label("References to THIS url");
        scrollPosition2 = GUILayout.BeginScrollView(scrollPosition2);


        var prevRoute = referenceFromSelected;
        referenceFromSelected = GUILayout.SelectionGrid(referenceFromSelected, matches.Select(m => m.PrefabAssetPath).ToArray(), 1);

        if (prevRoute != referenceFromSelected)
        {
            ui.OpenPrefabByAssetPath(matches[referenceFromSelected].PrefabAssetPath);
            referenceFromSelected = -1;
        }

        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    void RenderUpperAndLowerRoutes(string currentUrl)
    {
        GUILayout.BeginArea(new Rect(Screen.width - w - off, off, w, h));

        Label("Navigation");
        Label(currentUrl);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        GUILayout.BeginVertical();

        //
        var routes = new List<string>();
        var names = new List<string>();

        RenderRootLink(ui, currentUrl, ref routes, ref names);
        RenderSubRoutes(ui, currentUrl, ref routes, ref names);


        var prevRoute = routeSelected;
        routeSelected = GUILayout.SelectionGrid(routeSelected, names.ToArray(), 1);

        if (prevRoute != routeSelected)
        {
            ui.OpenPrefab(routes[routeSelected]);
            routeSelected = -1;
        }

        GUILayout.EndVertical();
        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    void RenderSubRoutes(SimpleUI ui, string currentUrl, ref List<string> routes, ref List<string> names)
    {
        var subRoutes = ui.GetSubUrls(currentUrl, false).OrderByDescending(p => p.Usages).ToList();

        for (var i = 0; i < subRoutes.Count(); i++)
        {
            var pref = subRoutes[i];
            var name = $"\u2198 {pref.Name}";

            routes.Add(pref.Url);
            names.Add(name);
        }
    }
    void RenderRootLink(SimpleUI ui, string currentUrl, ref List<string> routes, ref List<string> names)
    {
        var root = ui.GetUpperUrl(currentUrl);

        bool hasRoot = !root.Equals(currentUrl);
        if (hasRoot)
        {
            var name = $"\u2B06 ({root})";

            routes.Add(root);
            names.Add(name);
        }
    }
}

