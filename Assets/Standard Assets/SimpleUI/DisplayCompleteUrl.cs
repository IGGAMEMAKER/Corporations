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
        if (SimpleUIEventHandler == null)
        {
            SimpleUIEventHandler = GetComponent<SimpleUIEventHandler>();
        }

        if (SimpleUIEventHandler != null)
        {
            var url = SimpleUI.GetCurrentUrl();

            SimpleUIEventHandler.OpenUrl(url);
            SimpleUIEventHandler.HidePrefab(url);
        }
    }
}
