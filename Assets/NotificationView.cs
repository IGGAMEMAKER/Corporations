using Assets.Utils;
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
            default:
                return notificationMessage.NotificationType.ToString();
        }
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

    string Prettify(long sum)
    {
        return $"${Format.Minify(sum)}";
    }

    private string RenderLevelUpText(NotificationMessageLevelUp notificationLevelUp)
    {
        return $"Product {GetProductName(notificationLevelUp.CompanyId)} was upgraded to {notificationLevelUp.Level}LVL";
    }

    private string RenderBuyingText(NotificationMessageBuyingCompany notification)
    {
        return $"Company {GetCompanyName(notification.CompanyId)} was bought by {GetInvestorName(notification.BuyerInvestorId)} for {Prettify(notification.Bid)}";
    }

    string RenderBankruptcyText (NotificationMessageBankruptcy notification)
    {
        return $"Company {GetCompanyName(notification.CompanyId)} is bankrupt!";
    }
}
