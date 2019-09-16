using Assets.Utils;
using System;
using System.Linq;

public class LinkToUnhappyTeam : ButtonController
{
    public override void Execute()
    {
        var myUnhappyTeams = CompanyUtils.GetDaughterUnhappyCompanies(GameContext, MyCompany.company.Id);

        var companyId = 0;

        if (myUnhappyTeams.Length == 0)
            return;

        var firstId = myUnhappyTeams.First().company.Id;

        var targetMenu = ScreenMode.ManageCompaniesScreen;

        if (CurrentScreen != targetMenu)
            companyId = firstId;
        else
        {
            var ind = Array.FindIndex(myUnhappyTeams, m => m.company.Id == companyId);

            if (ind == -1 || ind == myUnhappyTeams.Length - 1)
                companyId = firstId;
            else
                companyId = myUnhappyTeams[ind + 1].company.Id;
        }

        Navigate(targetMenu, Constants.MENU_SELECTED_COMPANY, companyId);
    }
}
