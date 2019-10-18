using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAcceptOfferIfNecessary : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return !CompanyUtils.IsCompanyWillAcceptAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id);
    }
}
