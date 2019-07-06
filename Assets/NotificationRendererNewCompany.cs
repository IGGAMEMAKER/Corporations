using Assets.Utils;
using UnityEngine;

public class NotificationRendererNewCompany : NotificationRenderer<NotificationMessageNewCompany>
{
    public override string GetTitle(NotificationMessageNewCompany message)
    {
        return $"New Startup! {CompanyUtils.GetCompanyById(GameContext, message.CompanyId).company.Name}";
    }

    public override string GetDescription(NotificationMessageNewCompany message)
    {
        var product = CompanyUtils.GetCompanyById(GameContext, message.CompanyId);

        return $"STARTUP on niche {GetNicheName(product.product.Niche)}. Will they change the world?";
    }

    public override void SetLink(NotificationMessageNewCompany message, GameObject LinkToEvent)
    {
        LinkToEvent.AddComponent<LinkToProjectView>().CompanyId = message.CompanyId;
    }
}
