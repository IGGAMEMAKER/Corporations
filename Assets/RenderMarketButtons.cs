using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RenderMarketButtons : View
{
    public GameObject RaiseInvestments;
    public GameObject Partnerships;
    public GameObject Expand;

    public override void ViewRender()
    {
        base.ViewRender();

        bool IsMarketResearched = Markets.IsExploredMarket(Q, SelectedNiche);


        var amountOfCompanies = Companies.GetDaughterCompaniesAmount(MyCompany, Q);
        var daughtersOnMarket = Companies.GetDaughterCompaniesOnMarket(MyCompany, SelectedNiche, Q);

        bool hasReleasedApps    = daughtersOnMarket.Where(p => p.isRelease).Count() > 0;

        bool hasDaughtersOnMarket = daughtersOnMarket.Count() > 0;



        RaiseInvestments.SetActive(IsMarketResearched && amountOfCompanies == 1 && hasDaughtersOnMarket);
        Partnerships    .SetActive(IsMarketResearched && amountOfCompanies == 1 && hasDaughtersOnMarket && hasReleasedApps);

        bool isTimeToExpand = CurrentIntDate > 90;
        Expand          .SetActive(IsMarketResearched && !MyCompany.isWantsToExpand && isTimeToExpand);
    }
}
