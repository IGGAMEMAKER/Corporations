using UnityEngine;

public class CompetingCompaniesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var e = entity as GameEntity;

        t.GetComponentInChildren<LinkToCompanyPreview>().CompanyId = e.company.Id;
        t.GetComponentInChildren<ProductCompanyCompetingPreview>().SetEntity(e);
    }
}
