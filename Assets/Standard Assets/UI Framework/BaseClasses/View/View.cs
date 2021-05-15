using Assets.Core;
using System;
using UnityEngine;
using UnityEngine.UI;

public abstract partial class View : BaseClass
{
    public virtual void ViewRender() { }

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

    AnimationSpawner AnimationSpawner;
    public void Animate(string text, Transform t)
    {
        if (AnimationSpawner == null)
            AnimationSpawner = FindObjectOfType<AnimationSpawner>();

        if (AnimationSpawner != null)
            AnimationSpawner.Spawn(text, t);
    }

    public void Animate(string text, GameObject obj)
    {
        if (AnimationSpawner == null)
            AnimationSpawner = FindObjectOfType<AnimationSpawner>();

        if (AnimationSpawner != null)
            AnimationSpawner.Spawn(text, obj.transform);
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
}