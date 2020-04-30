using Assets.Core;
using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class View : BaseClass
{
    public virtual void ViewRender() { }

    public void Draw(MonoBehaviour mb, bool condition) => Draw(mb.gameObject, condition);
    public void Draw(GameObject go, bool condition)
    {
        if (go.activeSelf != condition)
            go.SetActive(condition);
    }

    public void Show(MonoBehaviour mb) => Draw(mb.gameObject, true);
    public void Show(GameObject go) => Draw(go, true);

    public void Hide(MonoBehaviour mb) => Draw(mb.gameObject, false);
    public void Hide(GameObject go) => Draw(go, false);


    // TODO Remove/Drag used once
    public void Animate(Text text)
    {
        if (text.gameObject.GetComponent<TextBlink>() == null)
            text.gameObject.AddComponent<TextBlink>();
    }

    // TODO Remove/Drag used once
    public void AnimateIfValueChanged(Text text, string value)
    {
        if (text != null && !String.Equals(text.text, value))
        {
            text.text = value;

            Animate(text);
        }
    }



    public Color GetPanelColor(bool isSelected)
    {
        var col = isSelected
            ?
            Colors.COLOR_PANEL_SELECTED
            :
            Colors.COLOR_PANEL_BASE;

        ColorUtility.TryParseHtmlString(col, out Color panelColor);

        if (isSelected)
            panelColor.a = 1f;
        else
            panelColor.a = 66f / 255f;

        return panelColor;
    }

    public void Refresh()
    {
        ScreenUtils.UpdateScreen(Q);
    }
}
