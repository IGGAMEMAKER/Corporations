using Assets.Utils;
using Assets.Utils.Formatting;
using UnityEngine;

public class NotificationRendererNewCompany : NotificationRenderer<NotificationMessageNewCompany>
{
    public override string GetTitle(NotificationMessageNewCompany message)
    {
        return $"New Startup! {Companies.GetCompany(GameContext, message.CompanyId).company.Name}";
    }

    public override string GetDescription(NotificationMessageNewCompany message)
    {
        var product = Companies.GetCompany(GameContext, message.CompanyId);

        var nicheName = EnumUtils.GetFormattedNicheName(product.product.Niche);

        return $"STARTUP on niche {nicheName}. Will they change the world?";
    }

    public override void SetLink(NotificationMessageNewCompany message, GameObject LinkToEvent)
    {
        LinkToEvent.AddComponent<LinkToProjectView>().CompanyId = message.CompanyId;
    }
}
