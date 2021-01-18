using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DisplayCompleteUrl))]
public class DisplayCompleteUrlEditor : Editor
{
    private void OnEnable()
    {
        Debug.Log("Enable DisplayCompleteUrlEditor");
    }
    private void OnSceneGUI()
    {
        //Debug.Log("OnGUI: DisplayCompleteUrlEditor");
        Handles.BeginGUI();

        //Handles.Button(Vector3.one * 10, Quaternion.identity, 200, 200, Handles.RectangleHandleCap);
        Handles.Label(Vector3.zero, "Editor");

        Handles.EndGUI();
        var buttonExample = Selection.activeGameObject; // target as GameObject;

        Vector3 position = buttonExample.transform.position; // 
        float size = 100f;
        float pickSize = size;

        SimpleUI ui = EditorWindow.GetWindow<SimpleUI>();

        var currentUrl = ui.GetCurrentUrl();

        var subRoutes = ui.GetSubUrls(currentUrl);
        var root = ui.GetUpperUrl(currentUrl);

        var diff = 75f;

        var subUrlPosition = position + Vector3.down * diff;
        var rootPosition = position + Vector3.up * diff;

        bool hasSubUrl = subRoutes.Any();
        bool hasRoot = !root.Equals(currentUrl);

        if (hasSubUrl)
        {
            Handles.Label(subUrlPosition, "SubRoute");

            if (Handles.Button(subUrlPosition, Quaternion.identity, size, pickSize, Handles.RectangleHandleCap))
            {
                Debug.Log("Forward");

                ui.OpenPrefab(subRoutes.First().Url);
            }
        }


        if (hasRoot)
        {
            Handles.Label(rootPosition, "Root");

            if (Handles.Button(rootPosition, Quaternion.identity, size, pickSize, Handles.RectangleHandleCap))
            {
                Debug.Log("Go UP");

                ui.OpenPrefab(root);
            }
        }
    }
}

[ExecuteAlways]
public class DisplayCompleteUrl : MonoBehaviour
{
    public SimpleUIEventHandler SimpleUIEventHandler;

    // Start is called before the first frame update
    void Start()
    {
        ShowCurrentPrefab();
    }

    void ShowCurrentPrefab()
    {
        SimpleUI ui = EditorWindow.GetWindow<SimpleUI>();

        var url = ui.GetCurrentUrl();

        var subUrls = ui.GetSubUrls(url);
        var root = ui.GetUpperUrl(url);

        Debug.Log("<b>Show url</b>: " + url);
        //return;

        if (SimpleUIEventHandler == null)
        {
            SimpleUIEventHandler = GetComponent<SimpleUIEventHandler>();
        }

        if (SimpleUIEventHandler != null)
        {
            SimpleUIEventHandler.OpenUrl(url);
            SimpleUIEventHandler.HidePrefab(url);
        }
    }

    //[ExecuteAlways]
    private void OnGUI()
    {
        //if (GUI.Button(new Rect(0f, 0f, 350f, 150f), "button"))
        //{
        //    Debug.Log("I am doing something!");
        //}
    }
}
