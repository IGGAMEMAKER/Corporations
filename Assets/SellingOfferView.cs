using Assets.Utils;
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
        var c = Companies.GetCompany(GameContext, companyId);
        var buyer = Companies.GetInvestorById(GameContext, buyerId);

        Name.text = c.company.Name;
        CompanyLink.CompanyId = companyId;
        BuyerLink.CompanyId = buyer.company.Id;

        Buyer.text = Companies.GetInvestorName(buyer);

        var hasControl = Companies.GetControlInCompany(MyCompany, buyer, GameContext) > 0;
        Buyer.color = Visuals.GetColorFromString(hasControl ? VisualConstants.COLOR_CONTROL : VisualConstants.COLOR_CONTROL_NO);

        var rating = c.hasProduct ? Markets.GetMarketRating(GameContext, c.product.Niche): 0;
        NicheStage.SetStars(rating);

        StageHint.SetHint(c.hasProduct ? Markets.GetMarketState(GameContext, c.product.Niche).ToString() : "");

        Valuation.text = Format.Money(Economy.GetCompanyCost(GameContext, companyId));

        var offer = Companies.GetAcquisitionOffer(GameContext, companyId, buyerId);
        //Offer.text = Format.Money(offer.acquisitionOffer.Offer);

        AcceptOffer.SetData(companyId, buyerId);
        RejectOffer.SetData(companyId, buyerId);
    }
}
