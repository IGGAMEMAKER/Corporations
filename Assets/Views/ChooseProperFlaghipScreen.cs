using Assets.Core;
using System.Collections.Generic;
using UnityEngine;

public class ChooseProperFlaghipScreen : View
{
    // upgrade to level 1
    public GameObject PrototypeScreen;
    
    // get first test users. Upgrades: marketing only
    public GameObject FirstUsersScreen;

    // match market requirements and create your team
    // + hire project manager, team lead
    public GameObject MVPScreen;

    // hire marketing lead?
    public GameObject PrepareToReleaseScreen;

    public GameObject ReleasedScreen;
    // * hire all top managers
    // * become top1 by users
    // * buy all competitors

    public override void ViewRender()
    {
        base.ViewRender();

        var companyGoal = Flagship.companyGoal.InvestorGoal;

        UpdateIfNecessary(PrototypeScreen, companyGoal == InvestorGoal.Prototype);
        UpdateIfNecessary(FirstUsersScreen, companyGoal == InvestorGoal.FirstUsers);
        UpdateIfNecessary(MVPScreen, companyGoal == InvestorGoal.BecomeMarketFit);
        UpdateIfNecessary(PrepareToReleaseScreen, companyGoal == InvestorGoal.Release);

        // after release
        UpdateIfNecessary(ReleasedScreen, companyGoal == InvestorGoal.BecomeProfitable);
    }
}
