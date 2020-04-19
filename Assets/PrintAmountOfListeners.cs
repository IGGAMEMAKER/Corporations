using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintAmountOfListeners : View
{
    GameEntity menu;
    public Rect Listeners;

    void Start()
    {
        menu = ScreenUtils.GetMenu(Q);
    }

    void OnGUI()
    {
        GUI.Label(Listeners,
            $"Menu listeners: {menu.menuListener.value.Count}" +
            $"\nNavigation listeners: {menu.navigationHistoryListener.value.Count}" +
            $"\nAny Date listeners: {menu.anyDateListener.value.Count}"
            //$"\nDate Listeners: {menu.dateListener.value.Count}"
            );
    }
}
