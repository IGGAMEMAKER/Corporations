using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RenderAllAudienceNeededFeatureListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        //t.GetComponent<RenderAudienceNeededFeatureListView>().SetAudience(entity as AudienceInfo);
        t.GetComponent<FeatureView>().SetFeature(entity as NewProductFeature);

    }

    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship;

        var taskMockup = new TeamTaskFeatureUpgrade(new NewProductFeature("blah", null, 0));
        var maxFeatureRating = company.team.Teams.Where(t => Teams.IsTaskSuitsTeam(t.TeamType, taskMockup)).Select(t => Products.GetFeatureRatingCap(company, t, Q)).Max();

        var counter = 1;

        // marketing tasks were added
        if (company.team.Teams.Any(t => t.Tasks.Any(task => task.IsMarketingTask)))
            counter = 4;

        var features = Products.GetAvailableFeaturesForProduct(company)
            .Where(f => !Products.IsUpgradingFeature(company, Q, f.Name))
            .Where(f => Products.GetFeatureRating(company, f.Name) + 0.1f < maxFeatureRating)
            .ToArray()
            .TakeWhile(f => counter-- > 0)
            ;

        SetItems(features);
    }
}
