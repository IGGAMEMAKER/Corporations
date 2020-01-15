using Assets.Core;

public class HireManager : ButtonController
{
    public override void Execute()
    {
        Teams.HireManager(SelectedCompany, SelectedHuman);

        Navigate(ScreenMode.TeamScreen, Constants.MENU_SELECTED_COMPANY, SelectedCompany.company.Id);
    }
}
