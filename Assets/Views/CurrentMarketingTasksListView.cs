﻿using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CurrentMarketingTasksListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        var task = (TeamTask)(object)entity;
        t.GetComponent<TeamTaskView>().SetEntity(task, 0);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship;

        var tasks = new List<TeamTask>();

        for (var teamId = 0; teamId < company.team.Teams.Count; teamId++)
        {
            var team = company.team.Teams[teamId];

            for (var slotId = 0; slotId < team.Tasks.Count; slotId++)
            {
                if (team.Tasks[slotId].IsMarketingTask)
                    tasks.Add(team.Tasks[slotId]);
                    //tasks.Add(new SlotInfo { SlotId = slotId, TeamId = teamId });
            }
        }

        SetItems(tasks.OrderByDescending(t => Marketing.GetChannelClientGain(company, Markets.GetMarketingChannel(Q, (t as TeamTaskChannelActivity).ChannelId))).Take(18));
    }
}
