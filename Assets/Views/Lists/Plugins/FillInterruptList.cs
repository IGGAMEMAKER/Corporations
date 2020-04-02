using Assets.Core;
using System.Linq;
using UnityEngine;

public class FillInterruptList : View
{
    public GameObject CanReleaseProduct;
    public GameObject CanUpgradeCorporateCulture;
    public GameObject CanCompleteGoal;
    public GameObject CanSellCompany;
    public GameObject CanBuyCompany;
    public GameObject CanRaiseInvestments;
    public GameObject CanCheckAnnualReport;
    public GameObject CanCheckTrends;

    public GameObject InvestorLoyaltyWarning;
    public GameObject TeamLoyaltyWarning;
    public GameObject NeedToCompleteGoal;
    public GameObject NeedToManageCompanies;

    public GameObject InvestorLoyaltyThreat;
    public GameObject TeamLoyaltyThreat;
    public GameObject OutdatedProducts;

    public override void ViewRender()
    {
        base.ViewRender();

        if (!HasCompany)
            return;

        bool isCanCompleteGoal = CheckGoal();

        CanRaiseInvestments         .SetActive(IsCanRaiseInvestments());
        CanUpgradeCorporateCulture  .SetActive(IsCanUpgradeCorporateCulture());
        CanSellCompany              .SetActive(HasAcquisitionOffers());
        CanCheckTrends              .SetActive(true);

        CanReleaseProduct           .SetActive(false && HasReleaseableProducts());
        CanCheckAnnualReport        .SetActive(false && CheckAnnualReport());
        CanBuyCompany               .SetActive(false && CheckAcquisitionCandidates());

        TeamLoyaltyThreat           .SetActive(HasUnhappyTeams());
        NeedToCompleteGoal          .SetActive(true || false && !isCanCompleteGoal);
        OutdatedProducts            .SetActive(false && HasOutdatedProducts());


        NeedToManageCompanies       .SetActive(false);

        InvestorLoyaltyWarning      .SetActive(false);
        InvestorLoyaltyThreat       .SetActive(false);
        TeamLoyaltyWarning          .SetActive(false);


    }

    bool IsCanUpgradeCorporateCulture()
    {
        return
            MyCompany.isWantsToExpand &&
            Companies.IsHasReleasedProducts(Q, MyCompany) &&
            !Cooldowns.HasCorporateCultureUpgradeCooldown(Q, MyCompany);
    }

    bool HasReleaseableProducts()
    {
        var upgradableCompanies = Companies.GetDaughterReleaseableCompanies(Q, MyCompany.company.Id);
        var count = upgradableCompanies.Count();

        bool isAlreadyOnReleasableMarket = CurrentScreen == ScreenMode.DevelopmentScreen && count == 1 && SelectedCompany.company.Id == upgradableCompanies.First().company.Id;

        return count > 0 && !isAlreadyOnReleasableMarket;
    }

    bool HasOutdatedProducts()
    {
        return Companies.GetDaughterOutdatedCompanies(Q, MyCompany.company.Id).Length > 0;
    }

    bool IsCanRaiseInvestments()
    {
        return Companies.IsReadyToStartInvestmentRound(MyCompany) && Companies.IsHasDaughters(Q, MyCompany);
    }

    bool HasUnhappyTeams()
    {
        return Companies.GetDaughterUnhappyCompanies(Q, MyCompany.company.Id).Length > 0;
    }

    bool HasAcquisitionOffers()
    {
        return Companies.GetAcquisitionOffersToPlayer(Q).Count() > 0;
    }

    bool CheckAcquisitionCandidates()
    {
        return Markets.GetProductsAvailableForSaleInSphereOfInfluence(MyCompany, Q).Count > 0;
    }

    bool CheckAnnualReport()
    {
        return CurrentIntDate > 360;
    }

    bool CheckGoal()
    {
        return Investments.IsGoalCompleted(MyCompany, Q);
    }
}
