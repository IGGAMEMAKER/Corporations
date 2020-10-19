using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SupportFeaturesListView : ListView
{
    public bool MarketingSupport = true;
    public bool Servers = false;

    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<SupportView>().SetEntity(entity as SupportFeature);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        if (MarketingSupport)
            SetItems(Products.GetMarketingSupportFeatures(Flagship));

        if (Servers)
        {
            var teams = Flagship.team.Teams;
            bool hasDevTeamsOrHasPotentialToBuildThem = teams.Where(t => t.TeamType == TeamType.DevelopmentTeam).Count() > 0 || teams.Count() > 2;

            SetItems(Products.GetHighloadFeatures(Flagship).Take(hasDevTeamsOrHasPotentialToBuildThem ? 4 : 2));
        }
    }
}
