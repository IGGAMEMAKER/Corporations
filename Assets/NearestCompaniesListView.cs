using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NearestCompaniesListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var e = (entity as GameEntity);

        t.GetComponent<CompanyPreviewView>()
            .SetEntity(e);
    }

    GameEntity[] GetCompetingCompanies()
    {
        var niches = MyCompany.companyFocus.Niches;
        
        return NicheUtils.GetCompaniesWithSameInterests(GameContext, MyCompany)
            .OrderByDescending(c => CompanyEconomyUtils.GetCompanyCost(GameContext, c))
            .ToArray();

        ////niches.Select()
        //return CompanyUtils.GetAIManagingCompanies(GameContext)
        //    .OrderByDescending(c => CompanyEconomyUtils.GetCompanyCost(GameContext, c))
        //    .ToArray();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var competingCompanies = GetCompetingCompanies();

        SetItems(competingCompanies);
    }
}
