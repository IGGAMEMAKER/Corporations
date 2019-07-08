using Assets.Utils;
using UnityEngine;

public class NotificationRendererBankruptcy : NotificationRenderer<NotificationMessageBankruptcy>
{
    public override string GetDescription(NotificationMessageBankruptcy message)
    {
        var name = CompanyUtils.GetCompanyById(GameContext, message.CompanyId).company.Name;

        return $"Company {name} is DEAD! Everything has it's lifespan...";
    }

    public override string GetTitle(NotificationMessageBankruptcy message)
    {
        var name = CompanyUtils.GetCompanyById(GameContext, message.CompanyId).company.Name;

        return $"Company {name} is DEAD!";
    }

    public override void SetLink(NotificationMessageBankruptcy message, GameObject LinkToEvent)
    {
        
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
