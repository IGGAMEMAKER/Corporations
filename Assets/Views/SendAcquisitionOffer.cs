using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendAcquisitionOffer : ButtonController
{
    public override void Execute()
    {
        Companies.SendAcquisitionOffer(Q, SelectedCompany, MyCompany);
    }
}
