using Assets.Utils;
using UnityEngine;

public class RenderCompanyInterestList : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var niche = NicheUtils.GetNicheEntity(GameContext, (NicheType)(object)entity);

        t.GetComponent<NichePreview>().SetNiche(niche);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        SetItems(SelectedCompany.companyFocus.Niches.ToArray());
    }
}
