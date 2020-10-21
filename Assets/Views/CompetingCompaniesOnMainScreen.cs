using Assets.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompetingCompaniesOnMainScreen : ListView
{
    long maxValue = 1;
    long minValue = 0;

    public bool SortByIncome = false;

    public override void SetItem<T>(Transform t, T entity)
    {
        var e = entity as GameEntity;

        t.GetComponentInChildren<LinkToProjectView>().CompanyId = e.company.Id;
        t.GetComponentInChildren<CompanyViewOnMap>().SetEntity(e, false, SortByIncome);

        var value = SortByIncome ? Economy.GetIncome(Q, e) : Marketing.GetUsers(e);
        value = (long)Mathf.Clamp(value, minValue, maxValue);

        var marketShare = (value - minValue - 0f) / (maxValue - minValue);
        if (maxValue == 0 && minValue == 0)
        {
            marketShare = 0;
        }


        t.transform.localPosition = new Vector3(600 * marketShare, 0, 0);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var companies = Companies.GetCompetitorsOfCompany(Flagship, Q, true);

        var interval = 40;

        if (SortByIncome)
        {
            companies = companies.OrderBy(c => Economy.GetIncome(Q, c));
            //maxValue = companies.Max(c => Economy.GetCompanyIncome(Q, c));
            //minValue = companies.Min(c => Economy.GetCompanyIncome(Q, c));
            var income = Economy.GetIncome(Q, Flagship);
            maxValue = income * (100 + interval) / 100;
            minValue = income * (100 - interval) / 100;
        }
        else
        {
            companies = companies.OrderBy(Marketing.GetUsers);
            var clients = Marketing.GetUsers(Flagship);

            maxValue = clients * (100 + interval) / 100;
            minValue = clients * (100 - interval) / 100;
            //maxValue = companies.Max(Marketing.GetClients);
            //minValue = companies.Min(Marketing.GetClients);
        }


        SetItems(companies);
    }

    public void SetSortByIncome()
    {
        SortByIncome = true;

        ViewRender();
    }

    public void SetSortByUsers()
    {
        SortByIncome = false;
        ViewRender();
    }
}
