using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TasksForTaskTypeView : View
{
    public Text AmountOfFeatures;
    public Text AmountOfSlots;
    public GameObject PendingTaskIcon;

    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship;

        // ----------------------------------------------------
        var p = new TeamTaskSupportFeature(new SupportFeature { SupportBonus = new SupportBonusHighload(2_000_000) });
        var activeTasks = Teams.GetActiveSameTaskTypeSlots(company, p);

        var pending = Teams.GetPendingSameTypeTaskAmount(company, p);


        AmountOfFeatures.text = $"{activeTasks}";
        if (pending > 0)
            AmountOfFeatures.text += $"+{Visuals.Colorize(pending.ToString(), "orange")}";

        Draw(PendingTaskIcon, pending > 0);

        AmountOfSlots.text = Visuals.Colorize((long)Teams.GetOverallSlotsForTaskType(company, p));
        // ----------------------------------------------------
    }
}
