using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendAcquisitionOffer : ButtonController
{
    public override void Execute()
    {
        CompanyUtils.SendAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id);
    }
}
