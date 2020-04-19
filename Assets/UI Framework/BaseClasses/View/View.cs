using Assets.Core;
using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class View : BaseClass
{
    public virtual void ViewRender() { }

    public void UpdateIfNecessary(MonoBehaviour mb, bool condition) => UpdateIfNecessary(mb.gameObject, condition);
    public void UpdateIfNecessary(GameObject go, bool condition)
    {
        if (go.activeSelf != condition)
            go.SetActive(condition);
    }


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


    public void ListenMenuChanges(IMenuListener menuListener)
    {
        ScreenUtils.GetMenu(Q).AddMenuListener(menuListener);
    }

    public void RefreshPage()
    {
        ScreenUtils.UpdateScreenWithoutAnyChanges(Q);
    }
}
