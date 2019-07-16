using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellingOfferView : View
{
    public Text Name;
    public Text Buyer;
    public Text Valuation;
    public Text Offer;

    public AcceptAcquisitionProposalController AcceptOffer;
    public RejectAcquisitionProposalController RejectOffer;

    int companyId, buyerId;

    public void SetEntity(int companyId, int buyerId)
    {
        this.companyId = companyId;
        this.buyerId = buyerId;

        Render();
    }

    void Render()
    {
        var c = CompanyUtils.GetCompanyById(GameContext, companyId);
        var buyer = CompanyUtils.GetInvestorById(GameContext, buyerId);

        Name.text = c.company.Name;
        Buyer.text = CompanyUtils.GetInvestorName(buyer);

        Valuation.text = Format.Money(CompanyEconomyUtils.GetCompanyCost(GameContext, companyId));

        var offer = CompanyUtils.GetAcquisitionOffer(GameContext, companyId, buyerId);
        Offer.text = Format.Money(offer.acquisitionOffer.Offer);

        AcceptOffer.SetData(companyId, buyerId);
        RejectOffer.SetData(companyId, buyerId);
    }
}
