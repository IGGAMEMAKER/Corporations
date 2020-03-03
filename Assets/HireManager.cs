using Assets.Core;

public class HireManager : ButtonController
{
    public override void Execute()
    {
        Teams.HireManager(SelectedCompany, SelectedHuman);

        NavigateToProjectScreen(SelectedCompany.company.Id);
        //Navigate(ScreenMode.TeamScreen, Balance.MENU_SELECTED_COMPANY, SelectedCompany.company.Id);
    }
}
