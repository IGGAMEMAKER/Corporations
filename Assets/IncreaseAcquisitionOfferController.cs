using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAcquisitionOfferController : ButtonController
{
    public override void Execute()
    {
        var offer = CompanyUtils.GetAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id);

        var newConditions = offer.acquisitionOffer.BuyerOffer;

        newConditions.Price = (long)(newConditions.Price * 1.1f);

        CompanyUtils.TweakAcquisitionConditions(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id, newConditions);
    }
}
