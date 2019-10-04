using Assets.Utils;
using UnityEngine;

public class DecreaseAcquisitionOfferController : ButtonController
{
    public override void Execute()
    {
        var offer = CompanyUtils.GetAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id);

        var newConditions = offer.acquisitionOffer.AcquisitionConditions;

        newConditions.BuyerOffer = (long)(newConditions.BuyerOffer / 1.1f);

        CompanyUtils.TweakAcquisitionConditions(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id, newConditions);
    }
}
