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

        var unlockAll = TutorialUtils.IsGodMode(Q) || TutorialUtils.IsDebugMode();

        bool showPrototype  = !unlockAll && companyGoal == InvestorGoal.Prototype;
        bool showFirstUsers = !unlockAll && companyGoal == InvestorGoal.FirstUsers;
        bool showMVP        = !unlockAll && companyGoal == InvestorGoal.BecomeMarketFit;
        bool showPrerelease = !unlockAll && companyGoal == InvestorGoal.Release;

        bool showReleased   = companyGoal >= InvestorGoal.BecomeProfitable || unlockAll;

        Draw(PrototypeScreen, showPrototype);
        Draw(FirstUsersScreen, showFirstUsers);
        Draw(MVPScreen, showMVP);
        Draw(PrepareToReleaseScreen, showPrerelease);

        // after release
        Draw(ReleasedScreen, showReleased);
    }
}
