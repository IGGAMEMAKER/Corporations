using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
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
        var offer = CompanyUtils.GetAcquisitionOffer(GameContext, companyId, buyerId);

        CompanyUtils.BuyShares(GameContext, companyId, buyerId, MyCompany.shareholder.Id, -1, offer.acquisitionOffer.Offer);

        CompanyUtils.RemoveAcquisitionOffer(GameContext, companyId, buyerId);
    }
}
