using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMarketResearchTask : HideOnSomeCondition
{
    bool show = false;
    public TaskView TaskView;

    public override bool HideIf()
    {
        var task = CooldownUtils.GetTask(GameContext, new CompanyTaskExploreMarket(SelectedNiche));
        var hasTask = task != null;

        if (hasTask && !show)
        {
            show = true;
            TaskView.SetEntity(task);
        }

        show = hasTask && !task.isCompleted;

        return !show;
    }
}
