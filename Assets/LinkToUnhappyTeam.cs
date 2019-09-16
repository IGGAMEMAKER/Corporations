using Assets.Utils;
using System;
using System.Linq;
using UnityEngine;

public class LinkToUnhappyTeam : ButtonController
{
    public override void Execute()
    {
        var myUnhappyTeams = CompanyUtils.GetDaughterUnhappyCompanies(GameContext, MyCompany.company.Id);

        var companyId = SelectedCompany.company.Id;

        if (myUnhappyTeams.Length == 0)
            return;

        var firstId = myUnhappyTeams.First().company.Id;

        var targetMenu = ScreenMode.ManageCompaniesScreen;

        if (CurrentScreen != targetMenu)
            companyId = firstId;
        else
        {
            var ind = Array.FindIndex(myUnhappyTeams, m => m.company.Id == companyId);

            Debug.Log("Unhappy team index: " + ind + " out of " + myUnhappyTeams.Length);

            if (ind == -1 || ind == myUnhappyTeams.Length - 1)
                companyId = firstId;
            else
                companyId = myUnhappyTeams[ind + 1].company.Id;
        }

        var hint = $"You have {myUnhappyTeams.Length} exhausted teams.\n" + String.Join("\n", myUnhappyTeams.Select(p => p.company.Name));

        GetComponent<Hint>().SetHint(hint);
        Navigate(targetMenu, Constants.MENU_SELECTED_COMPANY, companyId);
    }
}
