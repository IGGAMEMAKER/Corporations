using Assets.Core;

public class WorkerJoinsYourCompanyPopupButton : PopupButtonController<PopupMessageWorkerWantsToWorkInYourCompany>
{
    public override void Execute()
    {
        var companyId = Popup.companyId;
        var humanId = Popup.humanId;


        var company = Companies.Get(Q, companyId);
        var human = Humans.Get(Q, humanId);

        NotificationUtils.ClosePopup(Q);


        Teams.HireManager(company, human, 0);

        NavigateToHuman(humanId);
    }

    public override string GetButtonName() => "Sure, we need people\nwith expertise!";
}
