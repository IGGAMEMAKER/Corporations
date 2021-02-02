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


    static void RenderUrlRemovingMode()
    {
        var index = ChosenIndex;
        var prefab = prefabs[index];

        Label(prefab.Url);

        if (Button("Go back"))
        {
            isUrlRemovingMode = false;
        }

        EditorGUILayout.HelpBox("Remove these references from code", MessageType.Error);
        var references = referencesFromCode;
        Label("This url is referenced from CODE");

        var currentUrl = prefab.Url;

        // TODO copied from DisplayConnectedUrlsEditor
        // ------------
        var matches = SimpleUI.WhatUsesComponent(currentUrl, referencesFromAssets); // referencesFromAssets.Where(m => m.URL.Equals(currentUrl.TrimStart('/'))).ToList();


        if (matches.Any() || referencesFromCode.Any())
            Label("References to THIS url");

        scrollPosition2 = GUILayout.BeginScrollView(scrollPosition2);

        // references from prefabs & scenes
        var names = matches.Select(m => $"<b>{SimpleUI.GetPrettyAssetType(m.PrefabAssetPath)}</b> " + SimpleUI.GetTrimmedPath(m.PrefabAssetPath)).ToList();
        var routes = matches.Select(m => m.PrefabAssetPath).ToList();

        // references from code
        foreach (var occurence in referencesFromCode)
        {
            names.Add($"<b>Code</b> {SimpleUI.GetTrimmedPath(occurence.ScriptName)}");
            routes.Add(occurence.ScriptName);
        }

        // navigate to one of these assets
        var prevRoute = referenceFromSelected;
        referenceFromSelected = GUILayout.SelectionGrid(referenceFromSelected, names.ToArray(), 1);

        GUILayout.EndScrollView();
        GUILayout.EndArea();

        if (prevRoute != referenceFromSelected)
        {
            SimpleUI.OpenPrefabByAssetPath(routes[referenceFromSelected]);
            referenceFromSelected = -1;
        }
        // ------------
    }

}
