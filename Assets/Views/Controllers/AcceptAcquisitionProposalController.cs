using Assets.Core;
using UnityEngine;

public class AcceptAcquisitionProposalController : ButtonController
{
    int companyId, buyerId;

    public void SetData(int companyId, int buyerId)
    {
        this.companyId = companyId;
        this.buyerId = buyerId;
    }

    public override void Execute()
    {
        var company = Companies.Get(Q, companyId);
        var buyer = Companies.GetInvestorById(Q, buyerId);

        var offer = Companies.GetAcquisitionOffer(Q, company, buyer);
        offer.acquisitionOffer.SellerOffer = offer.acquisitionOffer.BuyerOffer;

        Debug.Log("AcceptAcquisitionProposalController");
        //CompanyUtils.BuyShares(GameContext, companyId, buyerId, MyCompany.shareholder.Id, -1, offer.acquisitionOffer.Offer);

        Companies.ConfirmAcquisitionOffer(Q, company, buyer);
    }
}
