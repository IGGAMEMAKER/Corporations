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

    //float GetContentWidth(string text)
    //{
    //    text.Split('\n',);
    //}

    private void OnGUI()
    {
        if (isHovered)
        {
            if (Text.Length == 0)
                return;

            float mouseX = Input.mousePosition.x;
            float mouseY = Screen.height - Input.mousePosition.y;

            float offsetX = 15f;
            float offsetY = 15f;

            float width = 225f;
            float height = 225f;

            Rect content = new Rect(mouseX + offsetX, mouseY, width, height);
            Rect wrapper = new Rect(mouseX - offsetX, mouseY - offsetY, width + offsetX, height + offsetY);

            GUI.color = Color.white;

            GUI.Label(content, Text);
            GUI.Box(wrapper, "");
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
