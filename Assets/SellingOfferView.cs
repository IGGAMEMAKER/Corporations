using Assets.Utils;
using Assets.Utils.Formatting;
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

    public SetAmountOfStars NicheStage;
    public Hint StageHint;

    public LinkToProjectView CompanyLink;
    public LinkToProjectView BuyerLink;

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
        CompanyLink.CompanyId = companyId;
        BuyerLink.CompanyId = buyer.company.Id;

        Buyer.text = CompanyUtils.GetInvestorName(buyer);

        var hasControl = CompanyUtils.GetControlInCompany(MyCompany, buyer, GameContext) > 0;
        Buyer.color = Visuals.GetColorFromString(hasControl ? VisualConstants.COLOR_CONTROL : VisualConstants.COLOR_CONTROL_NO);

        var rating = c.hasProduct ? NicheUtils.GetMarketRating(GameContext, c.product.Niche): 0;
        NicheStage.SetStars(rating);

        StageHint.SetHint(c.hasProduct ? NicheUtils.GetMarketState(GameContext, c.product.Niche).ToString() : "");

        Valuation.text = Format.Money(EconomyUtils.GetCompanyCost(GameContext, companyId));

        var offer = CompanyUtils.GetAcquisitionOffer(GameContext, companyId, buyerId);
        Offer.text = Format.Money(offer.acquisitionOffer.Offer);

        AcceptOffer.SetData(companyId, buyerId);
        RejectOffer.SetData(companyId, buyerId);
    }
}
