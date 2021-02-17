using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SimpleUI
{
    [CustomEditor(typeof(DisplayConnectedUrls))]
    public class DisplayConnectedUrlsEditor : Editor
    {
        int w = 225;
        int h = 150;
        int off = 5;

        static Vector2 scrollPosition = Vector2.zero;
        static Vector2 scrollPosition2 = Vector2.zero;
        static Vector2 scrollPosition3 = Vector2.zero;

        //Handles.Button(Vector3.one * 10, Quaternion.identity, 200, 200, Handles.RectangleHandleCap);

        // Cached data
        List<FullPrefabMatchInfo> referencesFromAssets;
        List<SimpleUI.UsageInfo> referencesFromCode;

        SimpleUI _instance = null;
        SimpleUI SimpleUI
        {
            get
            {
                if (_instance == null)
                {
                    //Debug.Log("Getting SimpleUI cause it's null");

                    _instance = SimpleUI.GetInstance();
                }

                return _instance;
            }
        }

        private void OnSceneGUI()
        {
            if (EditorApplication.isCompiling || EditorApplication.isUpdating)
                return;

            referencesFromAssets = SimpleUI.allAssetsWithOpenUrl;
            referencesFromCode = SimpleUI.referencesFromCode;

            string currentUrl = SimpleUI.GetCurrentUrl();

            RenderUpperAndLowerRoutes(currentUrl);
            RenderReferencesToUrl(currentUrl);
            RenderReferencesFromUrl(currentUrl);
        }

        void Label(string text)
        {
            GUIStyle localStyle = new GUIStyle(GUI.skin.label);
            localStyle.normal.textColor = Color.white;

            GUILayout.Label(text, localStyle);
        }

        string Urlify(string url)
        {
            if (!url.StartsWith("/"))
                url = "/" + url;

            return url;
        }

        string GetPrettyNameForUrl(string url, string currentUrl)
        {
            url = Urlify(url);

            bool isDirectSubUrl = SimpleUI.isSubRouteOf(url, currentUrl, false);
            bool isSubSubUrl = !isDirectSubUrl && SimpleUI.isSubRouteOf(url, currentUrl, true);

            var prefix = "<- ";

            if (isDirectSubUrl)
            {
                prefix = "\u2198 ";
            }

            if (isSubSubUrl)
            {
                prefix = "\u2198 \u2198 ";
            }


            return prefix + SimpleUI.GetPrettyNameForExistingUrl(url);
        }


        void RenderReferencesFromUrl(string currentUrl)
        {
            GUILayout.BeginArea(new Rect(Screen.width - w - off, off + h + off, w, h));
            //GUILayout.BeginArea(new Rect(off, off + h, w, h));

            var matches = referencesFromAssets.Where(m => m.PrefabAssetPath.Equals(SimpleUI.GetOpenedAssetPath())).ToList();

            if (matches.Any())
                Label("Forward links...");

            scrollPosition3 = GUILayout.BeginScrollView(scrollPosition3);

            var referenceSelected = GUILayout.SelectionGrid(-1, matches.Select(m => GetPrettyNameForUrl(m.URL, currentUrl)).ToArray(), 1);

            GUILayout.EndScrollView();
            GUILayout.EndArea();

            if (referenceSelected != -1)
            {
                SimpleUI.OpenPrefabByUrl(matches[referenceSelected].URL);
            }
        }

        void RenderReferencesToUrl(string currentUrl)
        {
            GUILayout.BeginArea(new Rect(off, off, w, h));

            var matches = SimpleUI.WhatUsesComponent(currentUrl, referencesFromAssets);


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
            var referenceSelected = GUILayout.SelectionGrid(-1, names.ToArray(), 1);

            GUILayout.EndScrollView();
            GUILayout.EndArea();

            if (referenceSelected != -1)
            {
                SimpleUI.OpenPrefabByAssetPath(routes[referenceSelected]);
            }
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

            RenderRootLink(currentUrl, ref routes, ref names);
            RenderSubRoutes(currentUrl, ref routes, ref names);


            var routeSelected = GUILayout.SelectionGrid(-1, names.ToArray(), 1);

            GUILayout.EndVertical();
            GUILayout.EndScrollView();
            GUILayout.EndArea();

            if (routeSelected != -1)
            {
                SimpleUI.OpenPrefabByUrl(routes[routeSelected]);
            }
        }

        void RenderSubRoutes(string currentUrl, ref List<string> routes, ref List<string> names)
        {
            var subRoutes = SimpleUI.GetSubUrls(currentUrl, false).OrderByDescending(p => p.Usages).ToList();

            for (var i = 0; i < subRoutes.Count(); i++)
            {
                var pref = subRoutes[i];
                var name = $"\u2198 {pref.Name}";

                routes.Add(pref.Url);
                names.Add(name);
            }
        }

        void RenderRootLink(string currentUrl, ref List<string> routes, ref List<string> names)
        {
            var root = SimpleUI.GetUpperUrl(currentUrl);

            bool hasRoot = !root.Equals(currentUrl);
            if (hasRoot)
            {
                var name = $"\u2B06 ({root})";

                routes.Add(root);
                names.Add(name);
            }
        }
    }

}