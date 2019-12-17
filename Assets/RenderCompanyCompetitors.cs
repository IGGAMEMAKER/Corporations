using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCompanyCompetitors : ListView
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

        var competitors = SelectedCompany.partnerships.companies;

        SetItems(competitors.ToArray());
    }
}