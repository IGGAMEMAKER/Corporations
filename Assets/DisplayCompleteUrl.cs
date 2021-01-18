using UnityEditor;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(SimpleUIEventHandler))]
public class DisplayCompleteUrl : MonoBehaviour
{
    public SimpleUIEventHandler SimpleUIEventHandler;

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
}
