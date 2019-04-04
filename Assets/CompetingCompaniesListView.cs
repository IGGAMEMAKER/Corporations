using UnityEngine;

public class CompetingCompaniesListView : ListView
{
    public override void SetItem(Transform t, GameEntity entity)
    {
        t.GetComponentInChildren<LinkToCompanyPreview>().CompanyId = entity.company.Id;
        t.GetComponentInChildren<ProductCompanyCompetingPreview>().SetEntity(entity);
    }
}
