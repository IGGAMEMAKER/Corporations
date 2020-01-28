using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcquisitionBuyerCandidateView : View
{
    public Text CompanyName;
    public Text Offer;

    public void SetEntity(GameEntity offer)
    {
        var investorId = offer.acquisitionOffer.BuyerId;
        var buyerCompanyId = Investments.GetCompanyIdByInvestorId(GameContext, investorId);

        GetComponent<LinkToProjectView>().CompanyId = buyerCompanyId;
        Offer.text = Format.Money(offer.acquisitionOffer.BuyerOffer.Price);

        CompanyName.text = Companies.Get(GameContext, buyerCompanyId).company.Name;
    }
}
