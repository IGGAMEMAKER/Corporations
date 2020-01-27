﻿using Assets.Core;
using UnityEngine.UI;

public abstract class TimedButton : UpgradedButtonController
{
    public abstract string StandardTitle();
    //public abstract int QueuedTasks();

    public abstract CompanyTask GetCompanyTask();

    public bool HasActiveTimer()
    {
        return Cooldowns.IsHasTask(GameContext, GetCompanyTask());
    }
    int TimeRemaining()
    {
        var task = Cooldowns.GetTask(GameContext, GetCompanyTask());

        if (task == null)
            return 0;

        return task.EndTime - CurrentIntDate;
    }


    public override void ViewRender()
    {
        base.ViewRender();

        var title = StandardTitle();

        if (HasActiveTimer())
            title = $"will finish in {TimeRemaining()} days";

        GetComponentInChildren<Text>().text = title;
        GetComponent<Button>().interactable = IsInteractable();
    }

    // is interactable
    // check if can queue/do parallel tasks
}