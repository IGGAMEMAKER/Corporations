using Assets.Utils;
using UnityEngine;

public class NotificationRendererInvestmentRound : NotificationRenderer<NotificationMessageInvestmentRoundStarted>
{
    public override string GetTitle(NotificationMessageInvestmentRoundStarted message)
    {
        var c = CompanyUtils.GetCompany(GameContext, message.CompanyId);

        return $"Investment Round {c.investmentRounds.InvestmentRound}: {c.company.Name}";
    }

    public override string GetDescription(NotificationMessageInvestmentRoundStarted message)
    {
        var product = CompanyUtils.GetCompany(GameContext, message.CompanyId);

        return $"{product.company.Name} needs money. Will they get enough investments to complete their goals?";
    }

    public override void SetLink(NotificationMessageInvestmentRoundStarted message, GameObject LinkToEvent)
    {
        LinkToEvent.AddComponent<LinkToProjectView>().CompanyId = message.CompanyId;
    }
}
