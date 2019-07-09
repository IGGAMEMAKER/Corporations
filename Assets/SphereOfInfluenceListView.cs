using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SphereOfInfluenceListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
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
            .OrderByDescending(n => CompanyUtils.GetMarketSize(GameContext, n) * CompanyUtils.GetGroupMarketControl(MyCompany, n, GameContext));

        SetItems(niches.ToArray());
    }
}
