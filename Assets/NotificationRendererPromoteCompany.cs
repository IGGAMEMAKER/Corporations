using Assets.Utils;
using UnityEngine;

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
