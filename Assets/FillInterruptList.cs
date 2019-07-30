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

    public GameObject InvestorLoyaltyThreat;
    public GameObject TeamLoyaltyThreat;

    public override void ViewRender()
    {
        base.ViewRender();

        bool isCanUpgradeSegment = CheckSegments();
        bool isCanCompleteGoal = CheckGoal();
        bool isNeedsInterrupt = false;
        bool isCanSellCompany = CheckAcquisitionOffers();
        bool isCanBuyCompany = CheckAcquisitionCandidates();

        bool isCanSeeAnnualReport = CheckAnnualReport();

        CanUpgradeSegment.SetActive(isCanUpgradeSegment);
        CanCompleteGoal.SetActive(isCanCompleteGoal);
        NeedToCompleteGoal.SetActive(!isCanCompleteGoal);


        CanCheckAnnualReport.SetActive(isCanSeeAnnualReport);

        CanHireEmployee.SetActive(isNeedsInterrupt);

        InvestorLoyaltyWarning.SetActive(isNeedsInterrupt);
        TeamLoyaltyWarning.SetActive(isNeedsInterrupt);

        InvestorLoyaltyThreat.SetActive(isNeedsInterrupt);
        TeamLoyaltyThreat.SetActive(isNeedsInterrupt);

        CanSellCompany.SetActive(isCanSellCompany);
        CanBuyCompany.SetActive(isCanBuyCompany);
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

    private bool CheckSegments()
    {
        if (!HasProductCompany)
            return false;

        // TODO BETTER TO ITERATE THROUGH USERTYPE ENUM
        if (CheckCanUpgradeSegment(UserType.Core))
            return true;

        //if (CheckCanUpgradeSegment(UserType.Regular))
        //    return true;

        //if (CheckCanUpgradeSegment(UserType.Mass))
        //    return true;

        return false;
    }

    private bool CheckCanUpgradeSegment(UserType userType)
    {
        if (!HasProductCompany)
            return false;

        if (ProductUtils.HasSegmentCooldown(MyProductEntity, userType))
            return false;

        return ProductUtils.HasEnoughResourcesForSegmentUpgrade(MyProductEntity, GameContext, userType);
    }
}
