using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hint : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    public string Text;

    HintSpawner HintSpawner;

    UIHint hint;

    bool isHovered;

    void Start () {
        //HintSpawner = GameObject.Find("HintSpawner").GetComponent<HintSpawner>();
        //hint = HintSpawner.Spawn(gameObject).GetComponent<UIHint>();

        SetHint(Text);
    }



    private void OnGUI()
    {
        if (isHovered)
        {
            //GUI.Button(new Rect(10, 10, 100, 20), new GUIContent("Click me", "This is the tooltip"));
            ////GUI.Label(new Rect(10, 40, 100, 40), GUI.tooltip);
            //GUI.Label(new Rect(10, 40, 100, 40), new GUIContent();

            ////var offset = new Vector3(40, -35);
            //Debug.Log("Tooltip: " + Text);
            ////transform.position = Input.mousePosition + offset;
            ////GUI.Label(new Rect(10, 40, 200, 40), GUI.tooltip);

            //Rect rect = new Rect(Input.mousePosition.x + 40, Input.mousePosition.y - 35f, 150f, 150f);
            Debug.Log(Input.mousePosition.y);

            float mouseX = Input.mousePosition.x;
            float mouseY = Input.mousePosition.y;

            Rect content = new Rect(mouseX, 150f, 150f, 150f);
            Rect wrapper = new Rect(mouseX - 15f, 150f, 150f, 150f);

            GUI.color = Color.white;

            GUI.Box(wrapper, "");
            GUI.Label(content, Text);
            //GUI.Label(rect, Text, GUI.tooltip);

        }
    }

    public void SetHintObject(string text)
    {
        SetHint(text);
    }

    public void SetHint(string text)
    {
        //hint.SetHintObject(Text);
        Text = text;
    }

    public void Rotate(float angle)
    {
        //hint.Rotate(angle);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //hint.OnHover();
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //hint.OnExit();
        isHovered = false;
    }
}
