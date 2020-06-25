using System.Collections.Generic;
using Assets.Core;
using Entitas;

public class WarnAboutBankruptcySystem : OnLastDayOfPeriod
{
    public WarnAboutBankruptcySystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var playerCompany = Companies.GetPlayerCompany(gameContext);

        if (playerCompany == null)
            return;

        //if (Economy.IsWillBecomeBankruptOnNextPeriod(gameContext, playerCompany))
        //{
        //    NotificationUtils.AddPopup(gameContext, new PopupMessageBankruptcyThreat(playerCompany.company.Id));

        //    TutorialUtils.Unlock(gameContext, TutorialFunctionality.CanRaiseInvestments);
        //    TutorialUtils.Unlock(gameContext, TutorialFunctionality.BankruptcyWarning);
        //}
    }
}
