using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RenderMarketButtons : View
{
    public GameObject RaiseInvestments;
    public GameObject Release;
    public GameObject Partnerships;

    public override void ViewRender()
    {
        base.ViewRender();

        var amountOfCompanies = Companies.GetDaughterCompaniesAmount(MyCompany, GameContext);
        var daughtersOnMarket = Companies.GetDaughterCompaniesOnMarket(MyCompany, SelectedNiche, GameContext);

        bool hasReleasebleApps  = daughtersOnMarket.Where(p => !p.isRelease).Count() > 0;
        bool hasReleasedApps    = daughtersOnMarket.Where(p => p.isRelease).Count() > 0;

        bool hasDaughtersOnMarket = daughtersOnMarket.Count() > 0;

        bool IsMarketResearched = Markets.IsExploredMarket(GameContext, SelectedNiche);

        RaiseInvestments.SetActive(IsMarketResearched && amountOfCompanies == 1 && hasDaughtersOnMarket);
        Partnerships    .SetActive(IsMarketResearched && amountOfCompanies == 1 && hasReleasedApps);
        Release         .SetActive(IsMarketResearched && hasReleasebleApps);
    }
}
