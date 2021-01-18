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
        var localPos = buttonExample.transform.localPosition;


        Debug.Log("Position: " + position + " local=" + localPos);

        SimpleUI ui = EditorWindow.GetWindow<SimpleUI>();

        var currentUrl = ui.GetCurrentUrl();

        RenderSubRoutes(ui, currentUrl, position);

        RenderRootLink(ui, currentUrl, position);

        Handles.EndGUI();
    }

    void RenderSubRoutes(SimpleUI ui, string currentUrl, Vector3 position)
    {
        var subRoutes = ui.GetSubUrls(currentUrl).ToList();

        bool hasSubUrl = subRoutes.Any();
        if (hasSubUrl)
        {
            var subUrlPosition = position + Vector3.down * diff;

            for (var i = 0; i < subRoutes.Count(); i++)
            {
                var pref = subRoutes[i];

                var pos = subUrlPosition + new Vector3(i * (size + diff), 0, 0);

                var textSize = pref.Name.Length;

                Handles.Label(pos + new Vector3(-textSize * 2f, 0, 0), pref.Name);

                if (BBBtn(pos))
                {
                    ui.OpenPrefab(pref.Url);
                }
            }
        }
    }

    void RenderRootLink(SimpleUI ui, string currentUrl, Vector3 position)
    {
        var root = ui.GetUpperUrl(currentUrl);

        bool hasRoot = !root.Equals(currentUrl);
        if (hasRoot)
        {
            var rootPosition = position + Vector3.up * diff;

            Handles.Label(rootPosition, $"UP ({root})");

            if (BBBtn(rootPosition))
            {
                ui.OpenPrefab(root);
            }
        }
    }

    bool BBBtn(Vector3 pos)
    {
        float size = 50f;
        float pickSize = size;

        return Handles.Button(pos, Quaternion.identity, size, pickSize, Handles.RectangleHandleCap);
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
