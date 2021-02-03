using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

// remove route mode
public partial class SimpleUI
{
    static Vector2 scrollPosition2 = Vector2.zero;
    static int referenceFromSelected = -1;
    static bool removeUrlRecursively = false;

    private static bool isEndedRemoveUrlScrollView = false;
    private static int removingUrlObstacles = 0;

    static void RemoveUrl(string url)
    {
        prefabs.RemoveAll(p => p.Url.Equals(url));

        if (removeUrlRecursively)
        {
            foreach (var r in GetSubUrls(url, false))
            {
                RemoveUrl(r.Url);
            }
        }
    }

    static void RenderUrlRemovingMode()
    {
        var index = ChosenIndex;
        var prefab = prefabs[index];

        Label(prefab.Url);

        if (Button("Go back"))
        {
            isUrlRemovingMode = false;
        }

        Space();
        if (removeUrlRecursively)
            EditorGUILayout.HelpBox("Will remove this route and ALL subroutes too", MessageType.Info);
        else
            EditorGUILayout.HelpBox("Will remove ONLY this route", MessageType.Info);

        removeUrlRecursively = EditorGUILayout.ToggleLeft("Remove subroutes", removeUrlRecursively);

        var currentUrl = prefab.Url;

        // TODO copied from DisplayConnectedUrlsEditor
        // ------------
        scrollPosition2 = GUILayout.BeginScrollView(scrollPosition2);

        // references from prefabs & scenes
        var names = new List<string>(); // matches.Select(m => $"<b>{SimpleUI.GetPrettyAssetType(m.PrefabAssetPath)}</b> " + SimpleUI.GetTrimmedPath(m.PrefabAssetPath)).ToList();
        var routes = new List<string>(); // matches.Select(m => m.PrefabAssetPath).ToList();

        if (removingUrlObstacles > 0)
        {
            Space();
            EditorGUILayout.HelpBox("Remove all these references to remove url", MessageType.Warning);
        }

        FillRoutes(names, routes, currentUrl, removeUrlRecursively);

        if (removingUrlObstacles == 0)
        {
            EditorGUILayout.HelpBox("You can safely remove this url", MessageType.Info);

            Space();
            if (Button("REMOVE URL!"))
            {
                isUrlRemovingMode = false;
                isUrlEditingMode = false;

                RemoveUrl(currentUrl);
                SaveData();

                //prefabs.RemoveAt(index);
                //SaveData();
            }
        }

        if (!isEndedRemoveUrlScrollView)
        {
            GUILayout.EndScrollView();
        }

        isEndedRemoveUrlScrollView = false;
        // ------------
    }

    static void FillRoutes(List<string> names, List<string> routes, string url, bool recursive)
    {
        var matches = SimpleUI.WhatUsesComponent(url, allReferencesFromAssets);

        Label($"References to {url}");

        names = new List<string>();
        routes = new List<string>();

        // references from code
        foreach (var occurence in WhichScriptReferencesConcreteUrl(url))
        {
            names.Add($"<b>Code</b> {SimpleUI.GetTrimmedPath(occurence.ScriptName)}");
            routes.Add(occurence.ScriptName);
        }

        // references from assets
        names.AddRange(matches.Select(m => $"<b>{SimpleUI.GetPrettyAssetType(m.PrefabAssetPath)}</b> " + SimpleUI.GetTrimmedPath(m.PrefabAssetPath)));
        routes.AddRange(matches.Select(m => m.PrefabAssetPath));

        removingUrlObstacles += names.Count;

        // navigate to one of these assets
        var selected = GUILayout.SelectionGrid(-1, names.ToArray(), 1);

        if (selected != -1)
        {
            GUILayout.EndScrollView();
            isEndedRemoveUrlScrollView = true;

            SimpleUI.OpenPrefabByAssetPath(routes[selected]);
            //referenceFromSelected = -1;
        }

        if (!recursive)
            return;

        foreach (var r in GetSubUrls(url, false))
        {
            FillRoutes(names, routes, r.Url, recursive);
        }
    }
}
