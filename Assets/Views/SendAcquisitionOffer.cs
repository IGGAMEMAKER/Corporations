using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendAcquisitionOffer : ButtonController
{
    public override void Execute()
    {
        Companies.SendAcquisitionOffer(Q, SelectedCompany, MyCompany);

        NotificationUtils.AddSimplePopup(Q, Visuals.Positive("Offer was sent"), "They will respond in a month or so");

        NavigateToMainScreen();
    }
}
