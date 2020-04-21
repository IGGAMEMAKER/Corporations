using Assets.Core;
using UnityEngine;

public class DeclareBankruptcyPopupButton : PopupButtonController<PopupMessageBankruptcyThreat>
{
    public override void Execute()
    {
        var playerCompany = Companies.GetPlayerCompany(Q);
        var daughters = Companies.GetDaughterCompanies(Q, playerCompany);

        for (var i = 0; i < daughters.Length; i++)
        {
            Companies.CloseCompany(Q, daughters[i]);
        }

        playerCompany.companyResource.Resources.Spend(playerCompany.companyResource.Resources);
        Teams.DismissTeam(playerCompany, Q);

        Debug.Log("Declared Bankruptcy");
    }

    public override string GetButtonName() => "Declare bankruptcy :(";
}