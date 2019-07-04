using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class NotificationRendererTrendsChange : NotificationRenderer<NotificationMessageTrendsChange>
{
    public override void Render(NotificationMessageTrendsChange message, Text Title, Text Description, GameObject LinkToEvent)
    {
        var nicheType = message.nicheType;
        var nicheName = GetNicheName(nicheType);

        var niche = NicheUtils.GetNicheEntity(GameContext, nicheType);

        Description.text = RenderTrendChageText(message);

        Title.text = GetShortTitle(niche.nicheState.Phase, nicheName);

        LinkToEvent.AddComponent<LinkToNiche>().SetNiche(nicheType);
    }

    string RenderTrendChageText(NotificationMessageTrendsChange notification)
    {
        var nicheType = notification.nicheType;

        var phase = NicheUtils.GetNicheEntity(GameContext, nicheType).nicheState.Phase;
        var nicheName = GetNicheName(nicheType);

        var description = "";

        switch (phase)
        {
            case NicheLifecyclePhase.Death:
                description = "People don't need them anymore and they will stop using the product. You'd better search new opportunities";
                break;

            case NicheLifecyclePhase.Decay:
                description = $"New users don't arrive anymore and we need to keep existing ones as long as possible";
                break;

            case NicheLifecyclePhase.Innovation:
                description = $"Maybe it is the next big thing?";
                break;

            case NicheLifecyclePhase.MassUse:
                description = $"They are well known even by those, who are not fancy to technologies";
                break;

            case NicheLifecyclePhase.Trending:
                description = $"We need to be quick if we want to make benefit from them";
                break;
        }

        return description;
    }

    static string GetShortTitle(NicheLifecyclePhase phase, string nicheName)
    {
        var description = "";

        switch (phase)
        {
            case NicheLifecyclePhase.Death:
                description = $"{nicheName} are DYING.";
                break;

            case NicheLifecyclePhase.Decay:
                description = $"{nicheName} are in DECAY.";
                break;

            case NicheLifecyclePhase.Innovation:
                description = $"{nicheName} - future or just a moment?";
                break;

            case NicheLifecyclePhase.MassUse:
                description = $"{nicheName} are EVERYWHERE.";
                break;

            case NicheLifecyclePhase.Trending:
                description = $"{nicheName} are TRENDING.";
                break;
        }

        return $"TRENDS change: {description}";
    }
}