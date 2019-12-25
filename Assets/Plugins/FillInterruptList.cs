using Assets.Utils;
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

        CanReleaseProduct           .SetActive(HasReleaseableProducts());
        NeedToCompleteGoal          .SetActive(!isCanCompleteGoal && false);

        NeedToManageCompanies       .SetActive(false);

        CanRaiseInvestments         .SetActive(IsCanRaiseInvestments());
        CanCheckAnnualReport        .SetActive(false && CheckAnnualReport());

        CanUpgradeCorporateCulture  .SetActive(IsCanUpgradeCorporateCulture());

        InvestorLoyaltyWarning      .SetActive(false);
        InvestorLoyaltyThreat       .SetActive(false);
        TeamLoyaltyWarning          .SetActive(false);
        TeamLoyaltyThreat           .SetActive(HasUnhappyTeams());

        OutdatedProducts            .SetActive(false && HasOutdatedProducts());

        CanSellCompany              .SetActive(HasAcquisitionOffers());
        CanBuyCompany               .SetActive(false && CheckAcquisitionCandidates());
    }

    bool IsCanUpgradeCorporateCulture()
    {
        return
            MyCompany.isWantsToExpand &&
            Companies.IsHasReleasedProducts(GameContext, MyCompany) &&
            !CooldownUtils.HasCorporateCultureUpgradeCooldown(GameContext, MyCompany);
    }

    bool HasReleaseableProducts()
    {
        var upgradableCompanies = Companies.GetDaughterReleaseableCompanies(GameContext, MyCompany.company.Id);
        var count = upgradableCompanies.Count();

        bool isAlreadyOnReleasableMarket = CurrentScreen == ScreenMode.NicheScreen && count == 1 && SelectedNiche == upgradableCompanies.First().product.Niche;

        return count > 0 && !isAlreadyOnReleasableMarket;
    }

    bool HasOutdatedProducts()
    {
        return Companies.GetDaughterOutdatedCompanies(GameContext, MyCompany.company.Id).Length > 0;
    }

    bool IsCanRaiseInvestments()
    {
        return Companies.IsReadyToStartInvestmentRound(MyCompany) && Companies.IsHasDaughters(GameContext, MyCompany);
    }

    bool HasUnhappyTeams()
    {
        return Companies.GetDaughterUnhappyCompanies(GameContext, MyCompany.company.Id).Length > 0;
    }

    bool CheckManagingCompanies()
    {
        return Companies.GetDaughterCompanies(GameContext, MyCompany.company.Id).Length > 0;
    }

    bool HasAcquisitionOffers()
    {
        return Companies.GetAcquisitionOffersToPlayer(GameContext).Count() > 0;
    }

    bool CheckAcquisitionCandidates()
    {
        return Markets.GetProductsAvailableForSaleInSphereOfInfluence(MyCompany, GameContext).Count > 0;
    }

    bool CheckAnnualReport()
    {
        return CurrentIntDate > 360;
    }

    bool CheckGoal()
    {
        return Investments.IsGoalCompleted(MyCompany, GameContext);
    }
}
