﻿using UnityEngine;



public class RenderCompanyPartners : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        var companyId = (int)(object)entity;

        t.GetComponent<LinkToProjectView>().CompanyId = companyId;
        t.GetComponent<RenderPartnerName>().SetCompanyId(companyId);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var partners = SelectedCompany.partnerships.companies;

        SetItems(partners);
    }
}
