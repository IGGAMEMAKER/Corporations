using Assets.Core;
using UnityEngine;

public class DeclareBankruptcyPopupButton : PopupButtonController<PopupMessageBankruptcyThreat>
{
    public override void Execute()
    {
        var playerCompany = Companies.GetPlayerCompany(Q);
        var daughters = Companies.GetDaughters(playerCompany, Q);

        Navigate(ScreenMode.MarketExplorationScreen);

        for (var i = 0; i < daughters.Length; i++)
        {
            Companies.CloseCompany(Q, daughters[i], true);
        }

        Companies.SetResources(playerCompany, 0, "Bankruptcy");
        //playerCompany.companyResource.Resources.Spend(playerCompany.companyResource.Resources);
        Teams.DismissTeam(playerCompany, Q);

        Debug.Log("Declared Bankruptcy");
        NotificationUtils.ClosePopup(Q);
    }

    public override string GetButtonName() => "Declare bankruptcy";
}