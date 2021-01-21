using Assets.Core;
using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class View : BaseClass
{
    public virtual void ViewRender() { }

    public string RenderName(GameEntity company)
    {
        if (Companies.IsDirectlyRelatedToPlayer(Q, company))
            return Visuals.Colorize(company.company.Name, Colors.COLOR_GOLD);
        else
            return Visuals.Colorize(company.company.Name, Colors.COLOR_WHITE);
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

    AnimationSpawner AnimationSpawner;
    public void Animate(string text, Transform t)
    {
        if (AnimationSpawner == null)
            AnimationSpawner = FindObjectOfType<AnimationSpawner>();

        if (AnimationSpawner != null)
            AnimationSpawner.Spawn(text, t);
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