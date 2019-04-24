using System;
using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class NotificationView : View {
    public void SetMessage(NotificationMessage notificationMessage)
    {
        GetComponent<Text>().text = RenderNotificationText(notificationMessage);
    }

    string RenderNotificationText (NotificationMessage notificationMessage)
    {
        switch (notificationMessage.NotificationType)
        {
            case NotificationType.Bankruptcy:
                return RenderBankruptcyText(notificationMessage as NotificationBankruptcy);
            case NotificationType.Buying:
                return RenderBuyingText(notificationMessage as NotificationBuyingCompany);
            case NotificationType.LevelUp:
                return RenderLevelUpText(notificationMessage as NotificationLevelUp);
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

    string Prettify(long sum)
    {
        return "$" + ValueFormatter.Shorten(sum);
    }

    string GetProductName(int companyId)
    {
        return CompanyUtils.GetCompanyById(GameContext, companyId).product.Name;
    }

    private string RenderLevelUpText(NotificationLevelUp notificationLevelUp)
    {
        return $"Product {GetProductName(notificationLevelUp.CompanyId)} was upgraded to {notificationLevelUp.Level}LVL";
    }

    private string RenderBuyingText(NotificationBuyingCompany notification)
    {
        return $"Company {GetCompanyName(notification.CompanyId)} was bought by {GetInvestorName(notification.BuyerInvestorId)} for {Prettify(notification.Bid)}";
    }

    string RenderBankruptcyText (NotificationBankruptcy notification)
    {
        return $"Company {GetCompanyName(notification.CompanyId)} is bankrupt!";
    }
}
