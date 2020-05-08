using Assets.Core;
using UnityEngine;

public class NotificationRendererMarketInnovation : NotificationRenderer<NotificationMessageInnovation>
{
    public override string GetTitle(NotificationMessageInnovation message)
    {
        var product = Companies.Get(Q, message.CompanyId);

        var status = message.Revolution ? "REVOLUTIONAIRE" : "innovator";

        return $"{product.company.Name} is {status} ({message.Level}LVL). This comapny will get {message.BrandGain} Brand Power"; // {}LVL!
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
        bool isPlayerRelated = Companies.IsRelatedToPlayer(Q, c);

        var colName = Colors.COLOR_PANEL_BASE;

        if (isCompetitor)
            colName = Colors.COLOR_NEGATIVE;

        if (isPlayerRelated)
            colName = Colors.COLOR_POSITIVE;

        return Visuals.GetColorFromString(colName);
    }
}
