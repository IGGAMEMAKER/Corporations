using Assets.Utils;
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
        var buyerCompanyId = InvestmentUtils.GetCompanyIdByInvestorId(GameContext, investorId);

        GetComponent<LinkToProjectView>().CompanyId = buyerCompanyId;
        Offer.text = Format.Money(offer.acquisitionOffer.BuyerOffer.Price);
        CompanyName.text = Companies.GetCompany(GameContext, buyerCompanyId).company.Name;
    }
}
