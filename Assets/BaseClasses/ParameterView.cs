﻿using UnityEngine.UI;

public abstract class SimpleParameterView : View
{
    internal Text Text;
    internal Hint Hint;

    void Start()
    {
        Text = GetComponent<Text>();
        Hint = GetComponent<Hint>();
    }

    public void Render()
    {
        Text.text = RenderValue();

        string hint = RenderHint();

        if (hint.Length > 0)
            Hint.SetHint(hint);
    }

    void Update()
    {
        Render();
    }

    public abstract string RenderValue();
    public abstract string RenderHint();
}

public abstract class UpgradedParameterView : View
{
    internal Text Text;
    internal Hint Hint;

    void PickComponents()
    {
        Text = GetComponent<Text>();
        Hint = GetComponent<Hint>();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        PickComponents();

        Text.text = RenderValue();

        string hint = RenderHint();

        if (hint.Length > 0)
            Hint.SetHint(hint);
    }

    public abstract string RenderValue();
    public abstract string RenderHint();
}
