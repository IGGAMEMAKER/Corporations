using System.Collections.Generic;
using UnityEngine;

public class ListenMenuChanges : Controller
    , IMenuListener
{
    public override void AttachListeners()
    {
        ListenMenuChanges(this);
    }

    public override void DetachListeners()
    {
        Debug.Log("Detach menu listeners");
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, Dictionary<string, object> data)
    {
        Render();
    }
}
