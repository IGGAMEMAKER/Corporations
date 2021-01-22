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
        var url = SimpleUI.GetCurrentUrl();

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
