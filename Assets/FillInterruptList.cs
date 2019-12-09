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
        bool isNeedsInterrupt = false;

        //CanReleaseProduct.SetActive(HasReleaseableProducts());
        CanCompleteGoal.SetActive(isCanCompleteGoal && false);
        NeedToCompleteGoal.SetActive(!isCanCompleteGoal && false);

        NeedToManageCompanies.SetActive(CheckManagingCompanies());


        CanCheckAnnualReport.SetActive(CheckAnnualReport());

        CanUpgradeCorporateCulture.SetActive(IsCanUpgradeCorporateCulture());

        InvestorLoyaltyWarning.SetActive(isNeedsInterrupt);
        TeamLoyaltyWarning.SetActive(isNeedsInterrupt);
        InvestorLoyaltyThreat.SetActive(isNeedsInterrupt);
        TeamLoyaltyThreat.SetActive(HasUnhappyTeams());

        OutdatedProducts.SetActive(HasOutdatedProducts());

        CanSellCompany.SetActive(HasAcquisitionOffers());
        CanBuyCompany.SetActive(false && CheckAcquisitionCandidates());
    }

    bool IsCanUpgradeCorporateCulture() => !CooldownUtils.HasCorporateCultureUpgradeCooldown(GameContext, MyCompany);

    bool HasReleaseableProducts()
    {
        var upgradableCompanies = Companies.GetDaughterReleaseableCompanies(GameContext, MyCompany.company.Id);
        return upgradableCompanies.Count() > 0;
    }

    bool HasOutdatedProducts()
    {
        return Companies.GetDaughterOutdatedCompanies(GameContext, MyCompany.company.Id).Length > 0;
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
        return Companies.GetAcquisitionOffersToPlayer(GameContext).Length > 0;
    }

    bool CheckAcquisitionCandidates()
    {
        return NicheUtils.GetProductsAvailableForSaleInSphereOfInfluence(MyCompany, GameContext).Count > 0;
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
