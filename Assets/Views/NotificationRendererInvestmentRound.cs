using Assets.Core;
using UnityEngine;

public class NotificationRendererInvestmentRound : NotificationRenderer<NotificationMessageInvestmentRoundStarted>
{
    public override string GetTitle(NotificationMessageInvestmentRoundStarted message)
    {
        var c = Companies.Get(Q, message.CompanyId);

        return $"{c.company.Name} started investment Round {c.investmentRounds.InvestmentRound}";
    }

    public override string GetDescription(NotificationMessageInvestmentRoundStarted message)
    {
        var product = Companies.Get(Q, message.CompanyId);

        return $"{product.company.Name} needs money. Will they get enough investments to complete their goals?";
    }

    public override void SetLink(NotificationMessageInvestmentRoundStarted message, GameObject LinkToEvent)
    {
        LinkToEvent.AddComponent<LinkToProjectView>().CompanyId = message.CompanyId;
    }

    public override Color GetNewsColor(NotificationMessageInvestmentRoundStarted message)
    {
        var c = Companies.Get(Q, message.CompanyId);

        bool isCompetitor = Companies.IsCompetingCompany(MyCompany, c, Q);
        var colName = isCompetitor ? Colors.COLOR_NEGATIVE : Colors.COLOR_PANEL_BASE;

        return Visuals.GetColorFromString(colName);
    }
}
