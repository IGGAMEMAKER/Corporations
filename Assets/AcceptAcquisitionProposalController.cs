using Assets.Utils;
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
        var offer = Companies.GetAcquisitionOffer(GameContext, companyId, buyerId);

        Debug.Log("AcceptAcquisitionProposalController");
        //CompanyUtils.BuyShares(GameContext, companyId, buyerId, MyCompany.shareholder.Id, -1, offer.acquisitionOffer.Offer);

        Companies.RemoveAcquisitionOffer(GameContext, companyId, buyerId);
    }
}
