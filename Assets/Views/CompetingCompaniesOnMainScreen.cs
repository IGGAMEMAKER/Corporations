using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompetingCompaniesOnMainScreen : ListView
{
    long maxClients = 1;
    long minClients = 0;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var e = entity as GameEntity;

        t.GetComponentInChildren<LinkToProjectView>().CompanyId = e.company.Id;
        t.GetComponentInChildren<CompanyViewOnMap>().SetEntity(e, false);

        var clients = Marketing.GetClients(e);
        var marketShare = (clients - minClients - 0f) / (maxClients - minClients);
        var position = 600 * marketShare;

        t.transform.localPosition = new Vector3(position, 0, 0);
        t.transform.localScale = new Vector3(1, 1, 1 + marketShare);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var companies = Companies.GetCompetitorsOfCompany(Flagship, Q, true)
            .OrderBy(Marketing.GetClients);

        maxClients = companies.Max(Marketing.GetClients);
        minClients = companies.Min(Marketing.GetClients);

        SetItems(companies);
    }
}
