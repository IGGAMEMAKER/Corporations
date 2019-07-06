using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class NotificationRendererInvestmentRound : NotificationRenderer<NotificationMessageInvestmentRoundStarted>
{
    public override void Render(NotificationMessageInvestmentRoundStarted message, Text Title, Text Description, GameObject LinkToEvent)
    {
        var product = CompanyUtils.GetCompanyById(GameContext, message.CompanyId);

        Description.text = $"{product.company.Name} needs money. Will they get enough investments to complete their goals?";

        Title.text = GetTitle(message, GameContext);

        RemoveLinks();
        LinkToEvent.AddComponent<LinkToProjectView>().CompanyId = message.CompanyId;
    }

    public static string GetTitle(NotificationMessageInvestmentRoundStarted message, GameContext gameContext)
    {
        var c = CompanyUtils.GetCompanyById(gameContext, message.CompanyId);

        return $"Investment Round {c.investmentRounds.InvestmentRound}: {c.company.Name}";
    }
}

public class NotificationRendererPromoteCompany : NotificationRenderer<NotificationMessageCompanyTypeChange>
{
    public override void Render(NotificationMessageCompanyTypeChange message, Text Title, Text Description, GameObject LinkToEvent)
    {
        var product = CompanyUtils.GetCompanyById(GameContext, message.CompanyId);

        Description.text = $"{product.company.Name} needs money. Will they get enough investments to complete their goals?";

        Title.text = GetTitle(message, GameContext);

        RemoveLinks();
        LinkToEvent.AddComponent<LinkToProjectView>().CompanyId = message.CompanyId;
    }

    public static string GetTitle(NotificationMessageCompanyTypeChange message, GameContext gameContext)
    {
        var c = CompanyUtils.GetCompanyById(gameContext, message.CompanyId);

        return $"PROMOTION: {message.PreviousName} => {c.company.Name}";
    }
}
