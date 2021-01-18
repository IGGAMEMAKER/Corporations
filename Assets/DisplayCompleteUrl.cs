using System.Collections;
using System.Collections.Generic;
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
        Debug.Log("OnGUI: DisplayCompleteUrlEditor");
        Handles.BeginGUI();

        Handles.Button(Vector3.one * 10, Quaternion.identity, 200, 200, Handles.RectangleHandleCap);
        Handles.Label(Vector3.zero, "Editor");

        Handles.EndGUI();
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
