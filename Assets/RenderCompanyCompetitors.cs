using Assets.Core;
using UnityEngine;

public class RenderCompanyCompetitors : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var company = entity as GameEntity;
        var companyId = company.company.Id;

        t.GetComponent<LinkToProjectView>().CompanyId = companyId;
        t.GetComponent<RenderPartnerName>().SetCompanyId(companyId);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var competitors = Companies.GetCompetitorsOfCompany(SelectedCompany, Q);

        SetItems(competitors);
    }
}