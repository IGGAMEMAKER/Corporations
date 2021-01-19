using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DisplayConnectedUrls))]
public class DisplayCompleteUrlEditor : Editor
{
    float size = 50f;
    float pickSize = 50f;
    float diff = 75f;

    static int routeSelected = -1;

    static Vector2 scrollPosition = Vector2.zero;

    private void OnEnable()
    {
        Debug.Log("Enable DisplayCompleteUrlEditor");
    }

    private void OnSceneGUI()
    {
        Handles.BeginGUI();

        //Handles.Button(Vector3.one * 10, Quaternion.identity, 200, 200, Handles.RectangleHandleCap);
        //Handles.Label(Vector3.zero, "Editor");

        var trg = target as DisplayConnectedUrls;

        var buttonExample = Selection.activeGameObject; // target as GameObject;

        SimpleUI ui = EditorWindow.GetWindow<SimpleUI>();

        var currentUrl = ui.GetCurrentUrl();

        var w = 250;
        var h = 150;

        var off = 5;

        GUILayout.BeginArea(new Rect(Screen.width - w - off, off, w, h));
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        GUILayout.Label("Navigation", EditorStyles.boldLabel);
        GUILayout.BeginVertical();

        // content
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
        // content

        GUILayout.EndVertical();
        GUILayout.EndScrollView();
        GUILayout.EndArea();

        Handles.EndGUI();
    }

    void RenderSubRoutes(SimpleUI ui, string currentUrl, ref List<string> routes, ref List<string> names)
    {
        //GUILayout.Space(15);

        var subRoutes = ui.GetSubUrls(currentUrl, false).OrderByDescending(p => p.Usages).ToList();

        for (var i = 0; i < subRoutes.Count(); i++)
        {
            var pref = subRoutes[i];
            var name = $"\u2198 {pref.Name}";

            routes.Add(pref.Url);
            names.Add(name);

            //if (BBBtn(name))
            //{
            //    ui.OpenPrefab(pref.Url);
            //}
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

            //if (BBBtn(name))
            //{
            //    ui.OpenPrefab(root);
            //}
        }
    }

    bool BBBtn(string text)
    {
        return GUILayout.Button(text);
        
        //return Handles.Button(pos, Quaternion.identity, size, pickSize, Handles.RectangleHandleCap);
    }
}

public class DisplayConnectedUrls : MonoBehaviour
{
}
