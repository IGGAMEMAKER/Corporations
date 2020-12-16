using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSendAcquisitionOfferButtonIfSentAlready : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var offer = Companies.GetAcquisitionOffer(Q, SelectedCompany, MyCompany);

        return offer.acquisitionOffer.Turn == AcquisitionTurn.Seller || Companies.IsDirectlyRelatedToPlayer(Q, SelectedCompany);
    }
}
