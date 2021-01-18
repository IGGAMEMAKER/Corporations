using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DisplayConnectedUrls))]
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
        //Handles.Label(Vector3.zero, "Editor");

        var buttonExample = Selection.activeGameObject; // target as GameObject;

        Vector3 position = buttonExample.transform.position;
        float size = 50f;
        float pickSize = size;

        SimpleUI ui = EditorWindow.GetWindow<SimpleUI>();

        var currentUrl = ui.GetCurrentUrl();

        var subRoutes = ui.GetSubUrls(currentUrl).ToList();
        var root = ui.GetUpperUrl(currentUrl);

        var diff = 75f;

        var subUrlPosition = position + Vector3.down * diff;
        var rootPosition = position + Vector3.up * diff;

        bool hasSubUrl = subRoutes.Any();
        bool hasRoot = !root.Equals(currentUrl);

        if (hasSubUrl)
        {
            for (var i = 0; i < subRoutes.Count(); i++)
            {
                var pref = subRoutes[i];

                var pos = subUrlPosition + new Vector3(i * (size + diff), 0, 0);

                var textSize = pref.Name.Length;

                Handles.Label(pos + new Vector3(-textSize * 2f, 0, 0), pref.Name);

                if (Handles.Button(pos, Quaternion.identity, size, pickSize, Handles.RectangleHandleCap))
                {
                    ui.OpenPrefab(pref.Url);
                }
            }
        }


        if (hasRoot)
        {
            Handles.Label(rootPosition, $"UP ({root})");

            if (Handles.Button(rootPosition, Quaternion.identity, size, pickSize, Handles.RectangleHandleCap))
            {
                ui.OpenPrefab(root);
            }
        }

        Handles.EndGUI();
    }
}

public class DisplayConnectedUrls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
