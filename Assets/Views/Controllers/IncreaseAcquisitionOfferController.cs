﻿using Assets.Core;

public class IncreaseAcquisitionOfferController : ButtonController
{
    public override void Execute()
    {
        var offer = Companies.GetAcquisitionOffer(Q, SelectedCompany, MyCompany);

        var newConditions = offer.acquisitionOffer.BuyerOffer;

        newConditions.Price = (long)(newConditions.Price * 1.1f);

        Companies.TweakAcquisitionConditions(Q, SelectedCompany, MyCompany, newConditions);
    }
}
