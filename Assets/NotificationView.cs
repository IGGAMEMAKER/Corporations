using Assets.Utils;
using Assets.Utils.Formatting;
using UnityEngine;
using UnityEngine.UI;

//public class NotificationRenderer
public interface INotificationRenderer<T> where T : NotificationMessage
{
    void Render(T message, Text Title, Text Description, GameObject LinkToEvent);
    //string GetTitle(T message, GameContext gameContext);
    //string GetShortTitle(T message, GameContext gameContext);
}

public abstract class NotificationRenderer<T> : View, INotificationRenderer<T> where T : NotificationMessage
{
    public abstract void Render(T message, Text Title, Text Description, GameObject LinkToEvent);

    internal string GetCompanyName(int companyId)
    {
        return CompanyUtils.GetCompanyById(GameContext, companyId).company.Name;
    }

    internal string GetInvestorName(int investorId)
    {
        return CompanyUtils.GetInvestorName(GameContext, investorId);
    }

    internal string GetProductName(int companyId)
    {
        return CompanyUtils.GetCompanyById(GameContext, companyId).company.Name;
    }

    internal string GetNicheName(NicheType nicheType)
    {
        return EnumUtils.GetFormattedNicheName(nicheType);
    }

    internal string Prettify(long sum)
    {
        return $"${Format.Minify(sum)}";
    }
}

public class NotificationView : View {
    public CloseNotificationButton CloseNotificationButton;
    public Text Title;
    public Text Description;
    public GameObject LinkToEvent;

    public void SetMessage(NotificationMessage notificationMessage)
    {
        int notificationId = transform.GetSiblingIndex();

        switch (notificationMessage.NotificationType)
        {
            case NotificationType.NicheTrends:
                gameObject.AddComponent<NotificationRendererTrendsChange>()
                    .Render(notificationMessage as NotificationMessageTrendsChange, Title, Description, LinkToEvent);
                break;

            case NotificationType.NewCompanyOnNiche:
                gameObject.AddComponent<NotificationRendererNewCompany>()
                    .Render(notificationMessage as NotificationMessageNewCompany, Title, Description, LinkToEvent);
                break;
        }

        //CloseNotificationButton.NotificationId = notificationId;

        //GetComponent<Text>().text = RenderNotificationText(notificationMessage);
    }


    //private string RenderBuyingText(NotificationMessageBuyingCompany notification)
    //{
    //    return $"ACQUISITION: Company {GetCompanyName(notification.CompanyId)} was bought by {GetInvestorName(notification.BuyerInvestorId)} for {Prettify(notification.Bid)}";
    //}

    //string RenderBankruptcyText(NotificationMessageBankruptcy notification)
    //{
    //    return $"BANKRUPTCY: Company {GetCompanyName(notification.CompanyId)} is bankrupt!";
    //}
}

public class NotificationRendererNewCompany : NotificationRenderer<NotificationMessageNewCompany>
{
    public override void Render(NotificationMessageNewCompany message, Text Title, Text Description, GameObject LinkToEvent)
    {
        var product = CompanyUtils.GetCompanyById(GameContext, message.CompanyId);

        Title.text = GetTitle(message, GameContext);

        Description.text = $"STARTUP on niche {GetNicheName(product.product.Niche)}. Will they change the world?";

        LinkToEvent.AddComponent<LinkToProjectView>().CompanyId = message.CompanyId;
    }

    public static string GetTitle(NotificationMessageNewCompany message, GameContext gameContext)
    {
        return $"New Startup! {CompanyUtils.GetCompanyById(gameContext, message.CompanyId).company.Name}";
    }
}

public class NotificationRendererTrendsChange : NotificationRenderer<NotificationMessageTrendsChange>
{
    public override void Render(NotificationMessageTrendsChange message, Text Title, Text Description, GameObject LinkToEvent)
    {
        Title.text = GetShortTitle(message, GameContext);
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
                description = "People don't need them anymore and they will stop using the product." +
                    $" You'd better search new opportunities";
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

        return $"{GetShortTitle(phase, nicheName)}: {description}";
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