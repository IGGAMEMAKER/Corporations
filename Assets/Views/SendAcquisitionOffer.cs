using Assets.Core;

public class SendAcquisitionOffer : ButtonController
{
    public override void Execute()
    {
        Companies.SendAcquisitionOffer(Q, SelectedCompany, MyCompany);

        NotificationUtils.AddSimplePopup(Q, Visuals.Positive("Offer was sent"), "They will respond in a month or so");

        NavigateToMainScreen();
    }
}
