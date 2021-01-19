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

    static Vector2 scrollPosition = Vector2.zero;

    private void OnEnable()
    {
        Debug.Log("Enable DisplayCompleteUrlEditor");
    }

    private void OnSceneGUI()
    {
        //Debug.Log("OnGUI: DisplayCompleteUrlEditor");
        Handles.BeginGUI();

        //Handles.DrawSolidRectangleWithOutline(new Rect(0, 0, 250, 150), Color.red, Color.blue);

        //Handles.Button(Vector3.one * 10, Quaternion.identity, 200, 200, Handles.RectangleHandleCap);
        //Handles.Label(Vector3.zero, "Editor");

        var trg = target as DisplayConnectedUrls;

        var buttonExample = Selection.activeGameObject; // target as GameObject;

        var globalPos = (Vector2)buttonExample.transform.position;
        var localPos = (Vector2)buttonExample.transform.localPosition;

        var pivot = buttonExample.GetComponent<RectTransform>().anchoredPosition;

        var Vector22 = trg.Vector22;

        var sum = globalPos + localPos;
        var position = new Vector2(-sum.x, sum.y);
        position = new Vector2(0, 0) + Vector22; // - localPos + new Vector3(0, Screen.height);

        Debug.Log("Position: " + position + " local=" + localPos + " global=" + globalPos); //  + " w=" + Screen.width + " h=" + Screen.height

        SimpleUI ui = EditorWindow.GetWindow<SimpleUI>();

        var currentUrl = ui.GetCurrentUrl();

        scrollPosition = GUILayout.BeginScrollView(scrollPosition, "");

        RenderRootLink(ui, currentUrl, position);

        GUILayout.Space(15);
        RenderSubRoutes(ui, currentUrl, position);

        GUILayout.EndScrollView();

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

                var pos = subUrlPosition + new Vector3(0, -i * (size + diff), 0);

                var textSize = pref.Name.Length;

                //Handles.Label(pos + new Vector3(-textSize * 2f, 0, 0), $"\u2198 {pref.Name}");

                if (BBBtn(pos, $"\u2198 {pref.Name}"))
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

            //Handles.Label(rootPosition, $"\u2B06 ({root})");

            if (BBBtn(rootPosition, $"\u2B06 ({root})"))
            {
                ui.OpenPrefab(root);
            }
        }
    }

    bool BBBtn(Vector3 pos, string text)
    {
        float size = 50f;
        float pickSize = size;

        return GUILayout.Button(text);
        //{
        //    Debug.Log("Butt");
        //}

        return Handles.Button(pos, Quaternion.identity, size, pickSize, Handles.RectangleHandleCap);
    }
}

public class DisplayConnectedUrls : MonoBehaviour
{
    public Vector2 Vector22;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
