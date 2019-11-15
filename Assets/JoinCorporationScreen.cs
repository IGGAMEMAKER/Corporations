using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinCorporationScreen : View
{
    public Text Title;
    public Text ProposalStatus;

    public Text OurValuation;
    public Text TargetValuation;

    public Toggle KeepAsIndependent;

    public Text SharePercentage;
    public InputField SharesOfferInput;

    public override void ViewRender()
    {
        base.ViewRender();

        if (!HasCompany)
            return;

        Title.text = $"Integrate {SelectedCompany.company.Name} company to our corporation";

        // TODO DIVIDE BY ZERO
        var ourCost = EconomyUtils.GetCompanyCost(GameContext, MyCompany);
        if (ourCost == 0) ourCost = 1;
        var targetCost = EconomyUtils.GetCompanyCost(GameContext, SelectedCompany);

        var sizeComparison = targetCost * 100 / ourCost;

        OurValuation.text = Format.Money(ourCost);
        TargetValuation.text = $"{Format.Money(targetCost)} ({sizeComparison}%)";
    }
}
