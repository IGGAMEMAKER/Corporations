using Assets.Utils;
using UnityEngine;

public class FillInterruptList : View
{
    public GameObject CanUpgradeSegment;
    public GameObject CanHireEmployee;
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

        bool isCanUpgradeSegment = false;
        bool isCanCompleteGoal = CheckGoal();
        bool isNeedsInterrupt = false;
        bool isCanSellCompany = CheckAcquisitionOffers();
        bool isCanBuyCompany = false && CheckAcquisitionCandidates();

        bool isHaveUnhappyCompanies = CheckUnhappyTeams();

        bool isCanSeeAnnualReport = CheckAnnualReport();
        bool isHasDaughterCompanies = CheckManagingCompanies();
        bool isHasOutdatedProducts = CheckOutdatedProducts();

        CanUpgradeSegment.SetActive(isCanUpgradeSegment);
        CanCompleteGoal.SetActive(isCanCompleteGoal && false);
        NeedToCompleteGoal.SetActive(!isCanCompleteGoal && false);

        NeedToManageCompanies.SetActive(isHasDaughterCompanies);


        CanCheckAnnualReport.SetActive(isCanSeeAnnualReport);

        CanHireEmployee.SetActive(isNeedsInterrupt);

        InvestorLoyaltyWarning.SetActive(isNeedsInterrupt);
        TeamLoyaltyWarning.SetActive(isNeedsInterrupt);

        InvestorLoyaltyThreat.SetActive(isNeedsInterrupt);
        TeamLoyaltyThreat.SetActive(isHaveUnhappyCompanies);
        OutdatedProducts.SetActive(isHasOutdatedProducts);

        CanSellCompany.SetActive(isCanSellCompany);
        CanBuyCompany.SetActive(isCanBuyCompany);
    }

    bool CheckOutdatedProducts()
    {
        return CompanyUtils.GetDaughterOutdatedCompanies(GameContext, MyCompany.company.Id).Length > 0;
    }

    bool CheckUnhappyTeams()
    {
        return CompanyUtils.GetDaughterUnhappyCompanies(GameContext, MyCompany.company.Id).Length > 0;
    }

    bool CheckManagingCompanies()
    {
        return CompanyUtils.GetDaughterCompanies(GameContext, MyCompany.company.Id).Length > 0;
    }

    bool CheckAcquisitionOffers()
    {
        return CompanyUtils.GetAcquisitionOffersToPlayer(GameContext).Length > 0;
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
        return InvestmentUtils.IsGoalCompleted(MyCompany, GameContext);
    }
}
