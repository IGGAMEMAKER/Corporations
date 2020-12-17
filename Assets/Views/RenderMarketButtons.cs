using Assets.Core;
using System.Linq;
using UnityEngine;

public class RenderMarketButtons : View
{
    public GameObject RaiseInvestments;
    public GameObject Partnerships;
    public GameObject Expand;
    public GameObject CreateCompany;
    public GameObject NextMarketButton;


    public override void ViewRender()
    {
        base.ViewRender();

        var niche = SelectedNiche;
        bool IsMarketResearched = Markets.IsExploredMarket(Q, niche);


        var daughtersOnMarket = Companies.GetDaughtersOnMarket(MyCompany, niche, Q);

        bool hasReleasedApps        = daughtersOnMarket.Where(p => p.isRelease).Count() > 0;
        bool hasDaughtersOnMarket   = daughtersOnMarket.Count() > 0;


        var amountOfCompanies = Companies.GetDaughtersAmount(MyCompany, Q);




        RaiseInvestments.SetActive(false && IsMarketResearched && amountOfCompanies == 1 && hasDaughtersOnMarket);
        Partnerships    .SetActive(false && IsMarketResearched && amountOfCompanies == 1 && hasDaughtersOnMarket && hasReleasedApps);

        bool isTimeToExpand = CurrentIntDate > 90;
        Expand          .SetActive(IsMarketResearched && !MyCompany.isWantsToExpand && isTimeToExpand);


        bool canStartNewCompany = !hasDaughtersOnMarket && IsMarketResearched;
        CreateCompany   .SetActive(canStartNewCompany);


        var focus = MyCompany.companyFocus.Niches;
        var hideNextMarketButton = focus.Count <= 1 || !focus.Contains(niche);
        NextMarketButton.SetActive(!hideNextMarketButton);
    }
}
