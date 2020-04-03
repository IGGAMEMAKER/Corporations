using Assets.Core;
using UnityEngine;

public class NotificationRendererNewCompany : NotificationRenderer<NotificationMessageNewCompany>
{
    public override string GetTitle(NotificationMessageNewCompany message)
    {
        return $"New Startup! {Companies.Get(Q, message.CompanyId).company.Name}";
    }

    public override string GetDescription(NotificationMessageNewCompany message)
    {
        var product = Companies.Get(Q, message.CompanyId);

        var nicheName = Enums.GetFormattedNicheName(product.product.Niche);

        return $"STARTUP on niche {nicheName}. Will they change the world?";
    }

    public override void SetLink(NotificationMessageNewCompany message, GameObject LinkToEvent)
    {
        LinkToEvent.AddComponent<LinkToProjectView>().CompanyId = message.CompanyId;
    }
}
