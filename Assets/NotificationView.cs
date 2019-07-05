using Assets.Utils;
using Assets.Utils.Formatting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public interface INotificationRenderer<T> where T : NotificationMessage
{
    void Render(T message, Text Title, Text Description, GameObject LinkToEvent);
}

public abstract class NotificationRenderer<T> : View, INotificationRenderer<T> where T : NotificationMessage
{
    public abstract void Render(T message, Text Title, Text Description, GameObject LinkToEvent);

    public void RemoveLinks()
    {
        foreach (var l in GetComponents<ButtonController>())
            Destroy(l);
    }

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

public class NotificationView : View,
    IPointerEnterHandler,
    IPointerExitHandler
{
    public Text Title;
    public Text Description;
    public GameObject LinkToEvent;
    public Image Panel;

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

        //GetComponent<Text>().text = RenderNotificationText(notificationMessage);
    }

    void SetPanelColor(Color color)
    {
        Panel.color = color;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        SetPanelColor(GetPanelColor(true));
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        SetPanelColor(GetPanelColor(false));
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
