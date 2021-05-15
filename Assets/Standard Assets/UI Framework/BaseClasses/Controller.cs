using System;
using System.Runtime.Remoting.Messaging;
using Assets.Core;
using UnityEngine;

public abstract partial class Controller : BaseClass
{
    [Header("Always true except MenuController")]
    public bool TriggerOnEnable = true;
    [Space(30)]

    private View[] Views;
    private ButtonView[] buttonViews;

    public void Render()
    {
        foreach (var view in Views)
            view.ViewRender();

        foreach (var view in buttonViews)
            view.ViewRender();
    }

    void FillListeners()
    {
        Views = GetComponents<View>();
        buttonViews = GetComponents<ButtonView>();
    }

    void OnEnable()
    {
        AttachListeners();

        FillListeners();

        Render();
    }

    void OnDisable()
    {
        DetachListeners();
    }

    //void OnDestroy()
    //{
    //    DetachListeners();
    //}

    public abstract void AttachListeners();
    public abstract void DetachListeners();
}