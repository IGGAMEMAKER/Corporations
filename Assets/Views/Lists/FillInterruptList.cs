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

        bool isCanCompleteGoal = CheckGoal;

        CanRaiseInvestments         .SetActive(IsCanRaiseInvestments);
        CanUpgradeCorporateCulture  .SetActive(IsCanUpgradeCorporateCulture);
        CanSellCompany              .SetActive(HasAcquisitionOffers);
        CanCheckTrends              .SetActive(false);

        CanReleaseProduct           .SetActive(false);
        CanCheckAnnualReport        .SetActive(false);
        CanBuyCompany               .SetActive(false);

        TeamLoyaltyThreat           .SetActive(false);
        NeedToCompleteGoal          .SetActive(true);
        OutdatedProducts            .SetActive(false);


        NeedToManageCompanies       .SetActive(false);

        InvestorLoyaltyWarning      .SetActive(false);
        InvestorLoyaltyThreat       .SetActive(false);
        TeamLoyaltyWarning          .SetActive(false);
    }

    bool IsCanUpgradeCorporateCulture => Companies.IsHasReleasedProducts(Q, MyCompany) &&
            !Cooldowns.HasCorporateCultureUpgradeCooldown(Q, MyCompany);

    bool HasReleaseableProducts
    {
        get
        {
            var upgradableCompanies = Companies.GetDaughterReleaseableCompanies(Q, MyCompany.company.Id);
            var count = upgradableCompanies.Count();

            bool isAlreadyOnReleasableMarket = CurrentScreen == ScreenMode.DevelopmentScreen && count == 1 && SelectedCompany.company.Id == upgradableCompanies.First().company.Id;

            return count > 0 && !isAlreadyOnReleasableMarket;
        }
    }

    bool HasOutdatedProducts => Companies.GetDaughterOutdatedCompanies(Q, MyCompany.company.Id).Length > 0;

    bool IsCanRaiseInvestments => Companies.IsReadyToStartInvestmentRound(MyCompany) && Companies.IsHasDaughters(Q, MyCompany);

    bool HasUnhappyTeams => Companies.GetDaughterUnhappyCompanies(Q, MyCompany.company.Id).Length > 0;

    bool HasAcquisitionOffers => Companies.GetAcquisitionOffersToPlayer(Q).Count() > 0;

    bool CheckAcquisitionCandidates => Markets.GetProductsAvailableForSaleInSphereOfInfluence(MyCompany, Q).Count > 0;

    bool CheckAnnualReport => CurrentIntDate > 360;

    bool CheckGoal => Investments.IsGoalCompleted(MyCompany, Q);
}
