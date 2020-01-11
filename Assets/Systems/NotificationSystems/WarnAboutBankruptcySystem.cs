using System.Collections.Generic;
using Assets.Core;
using Entitas;

public class WarnAboutBankruptcySystem : OnLastDayOfPeriod
{
    public WarnAboutBankruptcySystem(Contexts contexts) : base(contexts)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        var playerCompany = Companies.GetPlayerCompany(gameContext);

        if (playerCompany == null)
            return;

        var profit = Economy.GetProfit(gameContext, playerCompany);
        var balance = Economy.BalanceOf(playerCompany);

        var willBecomeBankruptNextDay = balance + profit < 0;

        if (profit < 0 && willBecomeBankruptNextDay)
            NotificationUtils.AddPopup(gameContext, new PopupMessageBankruptcyThreat(playerCompany.company.Id));
    }
}
