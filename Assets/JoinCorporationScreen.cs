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

    public Text OfferNote;

    public override void ViewRender()
    {
        base.ViewRender();

        if (!HasCompany)
            return;

        var name = SelectedCompany.company.Name;
        Title.text = $"Integrate \"{name}\" to our corporation";

        // TODO DIVIDE BY ZERO
        var ourCost = EconomyUtils.GetCompanyCost(GameContext, MyCompany);
        if (ourCost == 0) ourCost = 1;
        var targetCost = EconomyUtils.GetCompanyCost(GameContext, SelectedCompany);

        var sizeComparison = targetCost * 100 / ourCost;

        var futureShares = targetCost * 100 / (targetCost + ourCost);

        OurValuation.text = Format.Money(ourCost);
        TargetValuation.text = $"{Format.Money(targetCost)} ({sizeComparison}%)";

        bool willStayIndependent = KeepAsIndependent.isOn;

        if (willStayIndependent)
        {
            OfferNote.text = $"Company {name} will be <b>Fully</b> integrated to our company.\n\n" +
                $"Their shareholders will own {futureShares} % of our corporation";
        }
        else
        {
            OfferNote.text = $"Company {name} will be <b>Partially</b> integrated to our company.\n\n" +
                $"They will be able to leave whenever they want.\nTheir shareholders won't receive our shares.\n\n" +
                $"Our company will get +1 Brand power for all products in Communications industry.\n\n" +
                $"Email Services 0 will get ";
        }
    }
}
