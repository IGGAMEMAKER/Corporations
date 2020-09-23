using Assets.Core;
using UnityEngine;

public class NotificationRendererBankruptcy : NotificationRenderer<NotificationMessageBankruptcy>
{
    public override string GetDescription(NotificationMessageBankruptcy message)
    {
        var name = Companies.Get(Q, message.CompanyId).company.Name;

        return $"Company {name} is BANKRUPT! Everything has it's lifespan...";
    }

    public override Color GetNewsColor(NotificationMessageBankruptcy message)
    {
        var c = Companies.Get(Q, message.CompanyId);

        bool isCompetitor = Companies.IsCompetingCompany(MyCompany, c, Q);
        var colName = isCompetitor ? Colors.COLOR_POSITIVE : Colors.COLOR_PANEL_BASE;

        return Visuals.GetColorFromString(colName);
    }

    public override string GetTitle(NotificationMessageBankruptcy message)
    {
        var name = Companies.Get(Q, message.CompanyId).company.Name;

        return $"Company {name} is BANKRUPT!";
    }

    public override void SetLink(NotificationMessageBankruptcy message, GameObject LinkToEvent)
    {
        
    }
}

public class NotificationRendererAcquisition : NotificationRenderer<NotificationMessageBuyingCompany>
{
    public override string GetDescription(NotificationMessageBuyingCompany message)
    {
        return GetTitle(message);
    }

    public override Color GetNewsColor(NotificationMessageBuyingCompany message)
    {
        var c = Companies.Get(Q, message.CompanyId);

        bool isCompetitor = Companies.IsCompetingCompany(MyCompany, c, Q);
        var colName = isCompetitor ? Colors.COLOR_NEGATIVE : Colors.COLOR_PANEL_BASE;

        return Visuals.GetColorFromString(colName);
    }

    public override string GetTitle(NotificationMessageBuyingCompany message)
    {
        var name = Companies.Get(Q, message.CompanyId).company.Name;
        var buyer = Companies.GetInvestorName(Q, message.BuyerInvestorId);

        return $"ACQUISITION: {buyer} bought {name} for {Format.Money(message.Bid)}!";
    }

    public override void SetLink(NotificationMessageBuyingCompany message, GameObject LinkToEvent)
    {
        LinkToEvent.AddComponent<LinkToProjectView>().CompanyId = message.CompanyId;
    }
}

public class NotificationRendererPromoteCompany : NotificationRenderer<NotificationMessageCompanyTypeChange>
{
    public override string GetTitle(NotificationMessageCompanyTypeChange message)
    {
        var c = Companies.Get(Q, message.CompanyId);

        return $"PROMOTION: {message.PreviousName} => {c.company.Name}";
    }

    public override string GetDescription(NotificationMessageCompanyTypeChange message)
    {
        var product = Companies.Get(Q, message.CompanyId);

        return $"{product.company.Name} promoted to GROUP of companies. They want to buy companies and make more innovative products!";
    }

    public override void SetLink(NotificationMessageCompanyTypeChange message, GameObject LinkToEvent)
    {
        LinkToEvent.AddComponent<LinkToProjectView>().CompanyId = message.CompanyId;
    }

    public override Color GetNewsColor(NotificationMessageCompanyTypeChange message)
    {
        var c = Companies.Get(Q, message.CompanyId);

        bool isCompetitor = Companies.IsCompetingCompany(MyCompany, c, Q);
        var colName = isCompetitor ? Colors.COLOR_NEGATIVE : Colors.COLOR_PANEL_BASE;

        return Visuals.GetColorFromString(colName);
    }
}
