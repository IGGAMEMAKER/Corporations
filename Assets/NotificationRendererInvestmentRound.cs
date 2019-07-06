using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class NotificationRendererInvestmentRound : NotificationRenderer<NotificationMessageInvestmentRoundStarted>
{
    public override string GetTitle(NotificationMessageInvestmentRoundStarted message)
    {
        var c = CompanyUtils.GetCompanyById(GameContext, message.CompanyId);

        return $"Investment Round {c.investmentRounds.InvestmentRound}: {c.company.Name}";
    }

    public override string GetDescription(NotificationMessageInvestmentRoundStarted message)
    {
        var product = CompanyUtils.GetCompanyById(GameContext, message.CompanyId);

        return $"{product.company.Name} needs money. Will they get enough investments to complete their goals?";
    }

    public override void SetLink(NotificationMessageInvestmentRoundStarted message, GameObject LinkToEvent)
    {
        LinkToEvent.AddComponent<LinkToProjectView>().CompanyId = message.CompanyId;
    }
}

public class NotificationRendererPromoteCompany : NotificationRenderer<NotificationMessageCompanyTypeChange>
{
    public override string GetTitle(NotificationMessageCompanyTypeChange message)
    {
        var c = CompanyUtils.GetCompanyById(GameContext, message.CompanyId);

        return $"PROMOTION: {message.PreviousName} => {c.company.Name}";
    }

    public override string GetDescription(NotificationMessageCompanyTypeChange message)
    {
        var product = CompanyUtils.GetCompanyById(GameContext, message.CompanyId);

        return $"{product.company.Name} needs money. Will they get enough investments to complete their goals?";
    }

    public override void SetLink(NotificationMessageCompanyTypeChange message, GameObject LinkToEvent)
    {
        LinkToEvent.AddComponent<LinkToProjectView>().CompanyId = message.CompanyId;
    }
}
