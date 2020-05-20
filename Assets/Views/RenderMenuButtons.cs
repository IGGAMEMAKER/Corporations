using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderMenuButtons : View
{
    public GameObject Main;
    public GameObject Date;
    public GameObject Cash;
    public GameObject Stats;
    public GameObject Messages;

    public GameObject Culture;
    public Image CultureIcon;

    public GameObject Investments;
    public Image InvestmentsIcon;

    public GameObject ExploreMarkets;
    public GameObject Partnerships;

    public GameObject Separator1;

    public GameObject Quit;

    public override void ViewRender()
    {
        base.ViewRender();

        bool hasProduct = Companies.IsHasDaughters(Q, MyCompany);

        bool isFirstYear = CurrentIntDate < 360;

        bool showStats = !isFirstYear;
        bool showMessages = hasProduct;


        var hasCultureCooldown = Cooldowns.HasCorporateCultureUpgradeCooldown(Q, MyCompany);

        bool hasReleasedProducts = Companies.IsHasReleasedProducts(Q, MyCompany);
        var playerCanExploreAdvancedTabs = hasReleasedProducts;
        bool bankruptcyLooming = TutorialUtils.IsOpenedFunctionality(Q, TutorialFunctionality.BankruptcyWarning);

        //var canRaiseInvestments = !isRoundActive ;
        var canRaiseInvestments = playerCanExploreAdvancedTabs || bankruptcyLooming;
        var isOnMainScreen = CurrentScreen == ScreenMode.HoldingScreen;
        //
        Draw(Main, hasProduct);
        Draw(Stats, showStats && isOnMainScreen);

        // messages
        Draw(Messages, false && showMessages);

        // culture
        CultureIcon.color = Visuals.GetColorFromString(hasCultureCooldown ? Colors.COLOR_NEUTRAL : Colors.COLOR_POSITIVE);
        Draw(Culture, false && hasProduct && hasReleasedProducts && !hasCultureCooldown);


        // investments
        //InvestmentsIcon.color = Visuals.GetColorFromString(canRaiseInvestments ? Colors.COLOR_NEUTRAL : Colors.COLOR_POSITIVE);
        Draw(Investments, hasProduct && canRaiseInvestments && isOnMainScreen);
        Draw(ExploreMarkets, playerCanExploreAdvancedTabs && isOnMainScreen);
        Draw(Partnerships, playerCanExploreAdvancedTabs && isOnMainScreen);
    }
}
