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

    SoundManager soundManager;

    bool isHovered;

    void Start () {
        soundManager = new SoundManager();

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

            //GUIStyle guiStyle = new GUIStyle();
            //Font font = new Font();
            //font
            //guiStyle.font = font;
            //guiStyle.fontSize = 20;

            float mouseX = Input.mousePosition.x;
            float mouseY = Screen.height - Input.mousePosition.y;

            float offsetX = 15f;
            float offsetY = 15f;


            float contentWidth = 225f;
            float contentHeight = 225f;

            float rightSideOfScreenOffset = 0;

            if (mouseX > Screen.width  - contentWidth)
                rightSideOfScreenOffset = contentWidth;

            Rect content = new Rect(mouseX + offsetX - rightSideOfScreenOffset, mouseY, contentWidth, contentHeight);
            Rect wrapper = new Rect(mouseX - offsetX - rightSideOfScreenOffset, mouseY - offsetY, contentWidth + offsetX, contentHeight + offsetY);

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
        Text = text;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        soundManager.PlayOnHintHoverSound();
        isHovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovered = false;
    }
}
