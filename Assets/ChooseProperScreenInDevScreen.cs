using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChooseProperScreenInDevScreen : View
{
    public GameObject IndustrialScreen;
    public GameObject TOP1Screen;
    public GameObject MissionScreen;

    public override void ViewRender()
    {
        base.ViewRender();


        // TODO also check if products are in same industry
        var dominantIndustry = MyCompany.companyFocus.Industries[0];
        var dominantIndustryCompetitors = Companies.GetNonFundCompaniesInterestedInIndustry(Q, dominantIndustry)
            .OrderByDescending(c => Economy.GetCompanyCost(Q, c));

        bool isDomineeringOnMarket = dominantIndustryCompetitors.First().company.Id == MyCompany.company.Id;

        IndustrialScreen.SetActive(!isDomineeringOnMarket);

        // check company goal here
        // top1 screen or mission screen

        bool isSuperRich = Economy.BalanceOf(MyCompany) > 3000000000;
        TOP1Screen.SetActive(isDomineeringOnMarket && !isSuperRich);

        MissionScreen.SetActive(isSuperRich);
    }
}
