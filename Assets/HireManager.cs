using Assets.Core;

public class HireManager : ButtonController
{
    public override void Execute()
    {
        var human = SelectedHuman;
        bool hasWorkAlready = human.hasWorker && human.worker.companyId != -1;

        if (hasWorkAlready)
            Teams.HuntManager(human, SelectedCompany, Q);
        else
            Teams.HireManager(SelectedCompany, human);

        NavigateToProjectScreen(SelectedCompany.company.Id);
        //Navigate(ScreenMode.TeamScreen, Balance.MENU_SELECTED_COMPANY, SelectedCompany.company.Id);
    }
}
