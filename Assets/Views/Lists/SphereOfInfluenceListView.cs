using Assets.Core;
using System.Linq;
using UnityEngine;

public class SphereOfInfluenceListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        NicheType nicheType = (NicheType)(object)entity;

        t.GetComponent<MarketShareView>().SetEntity(nicheType);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    void Render()
    {
        var niches = MyCompany.companyFocus.Niches
            .OrderByDescending(n => Companies.GetMarketImportanceForCompany(Q, MyCompany, n));

        SetItems(niches.ToArray());
    }
}
