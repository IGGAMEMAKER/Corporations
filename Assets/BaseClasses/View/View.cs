using Assets.Utils;
using Entitas;
using System;
using UnityEngine.UI;

public class View : BaseClass
{
    public void Animate(Text text)
    {
        if (text.gameObject.GetComponent<TextBlink>() == null)
            text.gameObject.AddComponent<TextBlink>();
    }

    public void AnimateIfValueChanged(Text text, string value)
    {
        if (text != null && !String.Equals(text.text, value))
        {
            text.text = value;

            Animate(text);
        }
    }

    public void ListenMenuChanges(IMenuListener menuListener)
    {
        ScreenUtils.GetMenu(GameContext).AddMenuListener(menuListener);
    }

    public void ListenDateChanges(IAnyDateListener dateListener)
    {
        ScheduleUtils.ListenDateChanges(GameContext, dateListener);
    }

    public void LazyUpdate(IAnyDateListener dateListener)
    {
        ScheduleUtils.ListenDateChanges(GameContext, dateListener);
    }
}

public class Controller : BaseClass
{
    public void ListenMenuChanges(IMenuListener menuListener)
    {
        ScreenUtils.GetMenu(GameContext).AddMenuListener(menuListener);
    }

    public void ListenDateChanges(IAnyDateListener dateListener)
    {
        ScheduleUtils.ListenDateChanges(GameContext, dateListener);
    }

    public void LazyUpdate(IAnyDateListener dateListener)
    {
        ScheduleUtils.ListenDateChanges(GameContext, dateListener);
    }
}
