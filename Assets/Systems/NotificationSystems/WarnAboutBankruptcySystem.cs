using System.Collections.Generic;
using Assets.Utils;
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

        var profit = Economy.GetProfit(playerCompany, gameContext);

        var willBecomeBankruptNextDay = playerCompany.companyResource.Resources.money + profit < 0;

        if (profit < 0 && willBecomeBankruptNextDay)
            NotificationUtils.AddPopup(gameContext, new PopupMessageBankruptcyThreat(playerCompany.company.Id));
    }
}
