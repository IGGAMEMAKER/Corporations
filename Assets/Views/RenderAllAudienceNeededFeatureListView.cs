using Assets.Core;
using System.Collections;
using System.Collections.Generic;
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

        //var audiences = Marketing.GetAudienceInfos();

        //SetItems(audiences);
        var company = Flagship;

        var features = Products.GetAvailableFeaturesForProduct(company);
        SetItems(features);
    }
}
