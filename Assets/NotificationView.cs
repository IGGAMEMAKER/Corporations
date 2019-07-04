using System;
using Assets.Utils;
using Assets.Utils.Formatting;
using UnityEngine.UI;

public class NotificationView : View {
    public CloseNotificationButton CloseNotificationButton;

    public void SetMessage(NotificationMessage notificationMessage)
    {
        int notificationId = transform.GetSiblingIndex();

        //CloseNotificationButton.NotificationId = notificationId;

        GetComponent<Text>().text = RenderNotificationText(notificationMessage);
    }

    string RenderNotificationText (NotificationMessage notificationMessage)
    {
        switch (notificationMessage.NotificationType)
        {
            case NotificationType.Bankruptcy:
                return RenderBankruptcyText(notificationMessage as NotificationMessageBankruptcy);

            case NotificationType.Buying:
                return RenderBuyingText(notificationMessage as NotificationMessageBuyingCompany);

            case NotificationType.LevelUp:
                return RenderLevelUpText(notificationMessage as NotificationMessageLevelUp);

            case NotificationType.NicheTrends:
                return RenderTrendChageText(notificationMessage as NotificationMessageTrendsChange);

            case NotificationType.NewCompanyOnNiche:
                return RenderNewCompanyText(notificationMessage as NotificationMessageNewCompany);

            default:
                return notificationMessage.NotificationType.ToString();
        }
    }

    private string RenderNewCompanyText(NotificationMessageNewCompany notificationMessageNewCompany)
    {
        var product = CompanyUtils.GetCompanyById(GameContext, notificationMessageNewCompany.CompanyId);

        return $"STARTUP on niche {GetNicheName(product.product.Niche)}: {GetProductName(notificationMessageNewCompany.CompanyId)}. Will they change the world?";
    }

    private string RenderLevelUpText(NotificationMessageLevelUp notificationLevelUp)
    {
        return $"Product {GetProductName(notificationLevelUp.CompanyId)} was upgraded to {notificationLevelUp.Level}LVL";
    }

    private string RenderBuyingText(NotificationMessageBuyingCompany notification)
    {
        return $"ACQUISITION: Company {GetCompanyName(notification.CompanyId)} was bought by {GetInvestorName(notification.BuyerInvestorId)} for {Prettify(notification.Bid)}";
    }

    string RenderBankruptcyText(NotificationMessageBankruptcy notification)
    {
        return $"BANKRUPTCY: Company {GetCompanyName(notification.CompanyId)} is bankrupt!";
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
                description = $"{nicheName} are DYING. People don't need them anymore and they will stop using the product." +
                    $" You'd better search new opportunities";
                break;

            case NicheLifecyclePhase.Decay:
                description = $"{nicheName} are in decay. New users don't arrive anymore and we need to keep existing ones as long as possible";
                break;

            case NicheLifecyclePhase.Innovation:
                description = $"{nicheName}, sounds interesting, isn't it? Maybe it is the next big thing?";
                break;

            case NicheLifecyclePhase.MassUse:
                description = $"{nicheName} are EVERYWHERE. They are well known even by those, who are not fancy to technologies";
                break;

            case NicheLifecyclePhase.Trending:
                description = $"{nicheName} are TRENDING. We need to be quick if we want to make benefit from them";
                break;
        }

        return $"TRENDS change: {description}";
    }

    string GetCompanyName(int companyId)
    {
        return CompanyUtils.GetCompanyById(GameContext, companyId).company.Name;
    }

    string GetInvestorName(int investorId)
    {
        return CompanyUtils.GetInvestorName(GameContext, investorId);
    }

    string GetProductName(int companyId)
    {
        return CompanyUtils.GetCompanyById(GameContext, companyId).company.Name;
    }

    string GetNicheName(NicheType nicheType)
    {
        return EnumUtils.GetFormattedNicheName(nicheType);
    }

    string Prettify(long sum)
    {
        return $"${Format.Minify(sum)}";
    }
}
