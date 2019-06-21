using Assets.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillInterruptList : View
{
    public GameObject CanUpgradeSegment;
    public GameObject CanHireEmployee;

    public GameObject InvestorLoyaltyWarning;
    public GameObject TeamLoyaltyWarning;

    public GameObject InvestorLoyaltyThreat;
    public GameObject TeamLoyaltyThreat;

    public override void ViewRender()
    {
        base.ViewRender();

        bool isCanUpgradeSegment = CheckSegments();
        bool isNeedsInterrupt = false;

        CanUpgradeSegment.SetActive(isCanUpgradeSegment);

        CanHireEmployee.SetActive(isNeedsInterrupt);

        InvestorLoyaltyWarning.SetActive(isNeedsInterrupt);
        TeamLoyaltyWarning.SetActive(isNeedsInterrupt);

        InvestorLoyaltyThreat.SetActive(isNeedsInterrupt);
        TeamLoyaltyThreat.SetActive(isNeedsInterrupt);
    }

    private bool CheckSegments()
    {
        // TODO BETTER TO ITERATE THROUGH USERTYPE ENUM
        if (CheckCanUpgradeSegment(UserType.Core))
            return true;

        if (CheckCanUpgradeSegment(UserType.Regular))
            return true;

        if (CheckCanUpgradeSegment(UserType.Mass))
            return true;

        return false;
    }

    private bool CheckCanUpgradeSegment(UserType userType)
    {
        if (ProductUtils.HasSegmentCooldown(MyProductEntity, userType))
            return false;

        return ProductUtils.HasEnoughResourcesForSegmentUpgrade(MyProductEntity, GameContext, userType);
    }
}
