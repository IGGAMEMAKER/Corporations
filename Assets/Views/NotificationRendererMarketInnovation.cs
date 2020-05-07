using Assets.Core;
using UnityEngine;

public class NotificationRendererMarketInnovation : NotificationRenderer<NotificationMessageInnovation>
{
    public override string GetTitle(NotificationMessageInnovation message)
    {
        return $"{Companies.Get(Q, message.CompanyId).company.Name} has reached {message.Level}LVL!";
    }

    public override string GetDescription(NotificationMessageInnovation message)
    {
        var product = Companies.Get(Q, message.CompanyId);

        var nicheName = Enums.GetFormattedNicheName(product.product.Niche);

        return $"Innovation on niche {nicheName}! Competitors need to adapt";
    }

    public override void SetLink(NotificationMessageInnovation message, GameObject LinkToEvent)
    {
        LinkToEvent.AddComponent<LinkToProjectView>().CompanyId = message.CompanyId;
    }
}
