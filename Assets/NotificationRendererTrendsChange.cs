using Assets.Core;
using Assets.Core.Formatting;
using UnityEngine;

public class NotificationRendererTrendsChange : NotificationRenderer<NotificationMessageTrendsChange>
{
    public override string GetTitle(NotificationMessageTrendsChange message)
    {
        var nicheType = message.nicheType;
        var nicheName = EnumUtils.GetFormattedNicheName(nicheType);

        var niche = Markets.GetNiche(GameContext, nicheType);

        return GetShortTitle(Markets.GetMarketState(niche), nicheName);
    }

    public override string GetDescription(NotificationMessageTrendsChange message)
    {
        return RenderTrendChageText(message);
    }

    public override void SetLink(NotificationMessageTrendsChange message, GameObject LinkToEvent)
    {
        var nicheType = message.nicheType;

        LinkToEvent.AddComponent<LinkToNiche>().SetNiche(nicheType);
    }

    string RenderTrendChageText(NotificationMessageTrendsChange notification)
    {
        var nicheType = notification.nicheType;

        var niche = Markets.GetNiche(GameContext, nicheType);
        var phase = Markets.GetMarketState(niche);

        var nicheName = EnumUtils.GetFormattedNicheName(nicheType);


        var description = "";

        switch (phase)
        {
            case MarketState.Death:
                description = "People don't need them anymore and they will stop using the product. You'd better search new opportunities";
                break;

            case MarketState.Decay:
                description = $"New users don't arrive anymore and we need to keep existing ones as long as possible";
                break;

            case MarketState.Innovation:
                description = $"Maybe it is the next big thing?";
                break;

            case MarketState.MassGrowth:
                description = $"They are well known even by those, who are not fancy to technologies";
                break;

            case MarketState.Trending:
                description = $"We need to be quick if we want to make benefit from them";
                break;
        }

        return description;
    }

    static string GetShortTitle(MarketState phase, string nicheName)
    {
        var description = "";

        switch (phase)
        {
            case MarketState.Death:
                description = $"{nicheName} are DYING.";
                break;

            case MarketState.Decay:
                description = $"{nicheName} are in DECAY.";
                break;

            case MarketState.Innovation:
                description = $"{nicheName} - future or just a moment?";
                break;

            case MarketState.MassGrowth:
            case MarketState.MassUsage:
                description = $"{nicheName} are EVERYWHERE.";
                break;

            case MarketState.Trending:
                description = $"{nicheName} are TRENDING.";
                break;
        }

        return $"TRENDS change: {description}";
    }

}