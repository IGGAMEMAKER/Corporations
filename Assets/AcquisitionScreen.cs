using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcquisitionScreen : View
{
    public Text Title;
    public Text ProposalStatus;

    public Text Offer;

    public AcquisitionButtonView AcquisitionButtonView;

    long offer;

    public override void ViewRender()
    {
        base.ViewRender();

        Title.text = $"Acquisition of company {SelectedCompany.company.Name}";

        ProposalStatus.text = "???";

        RenderOffer();
    }

    private void OnEnable()
    {
        offer = CompanyEconomyUtils.GetCompanyCost(GameContext, SelectedCompany.company.Id);
    }

    void RenderOffer()
    {
        string overpriceText = "";
        long overprice = 1;

        var cost = CompanyEconomyUtils.GetCompanyCost(GameContext, SelectedCompany.company.Id);

        if (offer > cost)
        {
            overprice = (offer * 10 / cost);
            overpriceText = $"  ({(float)(overprice / 10)}x)";
        }

        Offer.text = Format.Money(offer) + overpriceText;

        AcquisitionButtonView.SetAcquisitionBid(offer, offer > cost);
    }

    public void IncreaseOffer()
    {
        offer = offer * 3 / 2;
        if (offer > Balance)
            offer = Balance;

        RenderOffer();
    }

    public void DecreaseOffer()
    {
        offer = offer * 2 / 3;

        RenderOffer();
    }
}
