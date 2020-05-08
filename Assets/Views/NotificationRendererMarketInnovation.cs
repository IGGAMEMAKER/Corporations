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

    public override Color GetNewsColor(NotificationMessageInnovation message)
    {
        var c = Companies.Get(Q, message.CompanyId);

        bool isCompetitor = Companies.IsCompetingCompany(MyCompany, c, Q);
        var colName = isCompetitor ? Colors.COLOR_POSITIVE : Colors.COLOR_PANEL_BASE;

        return Visuals.GetColorFromString(colName);
    }
}
