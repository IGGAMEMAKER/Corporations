using Assets.Core;
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
        var c = Companies.Get(Q, companyId);
        var buyer = Companies.GetInvestorById(Q, buyerId);

        Name.text = c.company.Name;
        CompanyLink.CompanyId = companyId;
        BuyerLink.CompanyId = buyer.company.Id;

        Buyer.text = Companies.GetInvestorName(buyer);

        var hasControl = Companies.GetControlInCompany(MyCompany, buyer, Q) > 0;
        Buyer.color = Visuals.GetColorFromString(hasControl ? Colors.COLOR_CONTROL : Colors.COLOR_CONTROL_NO);

        var rating = c.hasProduct ? Markets.GetMarketRating(Q, c.product.Niche): 0;
        NicheStage.SetStars(rating);

        StageHint.SetHint(c.hasProduct ? Markets.GetMarketState(Q, c.product.Niche).ToString() : "");

        Valuation.text = Format.Money(Economy.CostOf(c, Q));

        var offer = Companies.GetAcquisitionOffer(Q, c, buyer);
        //Offer.text = Format.Money(offer.acquisitionOffer.Offer);

        AcceptOffer.SetData(companyId, buyerId);
        RejectOffer.SetData(companyId, buyerId);
    }
}
