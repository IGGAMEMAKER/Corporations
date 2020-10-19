using Assets.Core;
using UnityEngine;

public class RenderCompanyInterestList : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        var niche = Markets.Get(Q, (NicheType)(object)entity);

        t.GetComponent<NichePreview>().SetNiche(niche);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        SetItems(SelectedCompany.companyFocus.Niches.ToArray());
    }
}
