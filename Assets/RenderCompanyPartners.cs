using UnityEngine;

public class RenderCompanyPartners : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var companyId = (int)(object)entity;

        t.GetComponent<LinkToProjectView>().CompanyId = companyId;
        t.GetComponent<RenderPartnerName>().SetCompanyId(companyId);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var partners = SelectedCompany.partnerships.Companies;

        SetItems(partners.ToArray());
    }
}
