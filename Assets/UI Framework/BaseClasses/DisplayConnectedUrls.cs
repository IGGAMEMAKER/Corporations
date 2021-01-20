using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DisplayConnectedUrls))]
public class DisplayCompleteUrlEditor : Editor
{
    static int routeSelected = -1;

    static Vector2 scrollPosition = Vector2.zero;

    SimpleUI ui;

    private void OnSceneGUI()
    {
        //Handles.Button(Vector3.one * 10, Quaternion.identity, 200, 200, Handles.RectangleHandleCap);

        ui = EditorWindow.GetWindow<SimpleUI>();

        var w = 250;
        var h = 150;

        var off = 5;

        GUILayout.Label("Navigation", EditorStyles.boldLabel);

        GUILayout.BeginArea(new Rect(Screen.width - w - off, off, w, h));
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        GUILayout.BeginVertical();

        RenderRoutes();

        GUILayout.EndVertical();
        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    void RenderRoutes()
    {
        var routes = new List<string>();
        var names = new List<string>();

        var currentUrl = ui.GetCurrentUrl();

        RenderRootLink(ui, currentUrl, ref routes, ref names);
        RenderSubRoutes(ui, currentUrl, ref routes, ref names);


        var prevRoute = routeSelected;
        routeSelected = GUILayout.SelectionGrid(routeSelected, names.ToArray(), 1);

        if (prevRoute != routeSelected)
        {
            ui.OpenPrefab(routes[routeSelected]);
            routeSelected = -1;
        }
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

public class DisplayConnectedUrls : MonoBehaviour
{
}
