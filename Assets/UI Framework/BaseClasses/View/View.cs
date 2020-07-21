using Assets.Core;
using System;
using System.Collections.Generic;
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

    public string RenderName(GameEntity company)
    {
        if (Companies.IsRelatedToPlayer(Q, company))
            return Visuals.Colorize(company.company.Name, Colors.COLOR_GOLD);
        else
            return Visuals.Colorize(company.company.Name, Colors.COLOR_WHITE);
    }

    public void ShowOnly(GameObject obj, List<GameObject> objects)
    {
        foreach (var o in objects)
        {
            Draw(o, o.GetInstanceID() == obj.GetInstanceID());
        }
    }

    public void DrawCanvasGroup(GameObject go, bool condition)
    {
        var group = go.GetComponent<CanvasGroup>();

        if (group != null)
        {
            DrawCanvasGroup(group, condition);
        }
    }

    public void DrawCanvasGroup(CanvasGroup group, bool condition)
    {
        group.alpha = condition ? 1f : 0;
        //group.interactable = condition;
        group.blocksRaycasts = condition;
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

    AnimationSpawner AnimationSpawner;
    public void Animate(string text)
    {
        if (AnimationSpawner == null)
            AnimationSpawner = FindObjectOfType<AnimationSpawner>();

        AnimationSpawner.Spawn(text);
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