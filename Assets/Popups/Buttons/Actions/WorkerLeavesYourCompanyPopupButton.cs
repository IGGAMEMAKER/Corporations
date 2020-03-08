using Assets.Core;

public class WorkerLeavesYourCompanyPopupButton : PopupButtonController<PopupMessageWorkerLeavesYourCompany>
{
    public override void Execute()
    {
        var companyId = Popup.companyId;
        var humanId = Popup.humanId;

        NotificationUtils.ClosePopup(Q);

        var company = Companies.Get(Q, companyId);
        Teams.FireManager(company, Q, humanId);
    }

    public override string GetButtonName() => "Damn it!";
}
