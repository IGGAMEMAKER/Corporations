﻿using Assets.Core;
using UnityEngine;

public class UpgradingFeaturesListView : ListView
{
    //public override void SetItem<T>(Transform t, T entity)
    //{
    //    var slot = (SlotInfo)(object)(entity);
    //    t.GetComponent<TeamTaskView>().SetEntity(slot.TeamId, slot.SlotId);
    //}

    //public override void ViewRender()
    //{
    //    base.ViewRender();

    //    var company = Flagship;

    //    var tasks = new List<SlotInfo>();

    //    for (var teamId = 0; teamId < company.team.Teams.Count; teamId++)
    //    {
    //        var team = company.team.Teams[teamId];

    //        for (var slotId = 0; slotId < team.Tasks.Count; slotId++)
    //        {
    //            if (team.Tasks[slotId].IsFeatureUpgrade)
    //                tasks.Add(new SlotInfo { SlotId = slotId, TeamId = teamId });
    //        }
    //    }

    //    SetItems(tasks);
    //}
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<FeatureView>().SetFeature(entity as NewProductFeature, null);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var features = Products.GetUpgradingFeatures(Flagship);

        SetItems(features);
    }

    private void OnEnable()
    {
        ViewRender();
    }
}
