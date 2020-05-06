using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

            case NotificationType.InnovationOnNiche:
                gameObject.AddComponent<NotificationRendererMarketInnovation>()
                    .Render(notificationMessage as NotificationMessageInnovation, Title, Description, LinkToEvent);
                break;

            case NotificationType.InvestmentRoundStarted:
                gameObject.AddComponent<NotificationRendererInvestmentRound>()
                    .Render(notificationMessage as NotificationMessageInvestmentRoundStarted, Title, Description, LinkToEvent);
                break;

            case NotificationType.CompanyTypeChange:
                gameObject.AddComponent<NotificationRendererPromoteCompany>()
                    .Render(notificationMessage as NotificationMessageCompanyTypeChange, Title, Description, LinkToEvent);
                break;

            case NotificationType.Bankruptcy:
                gameObject.AddComponent<NotificationRendererBankruptcy>()
                    .Render(notificationMessage as NotificationMessageBankruptcy, Title, Description, LinkToEvent);
                break;

            case NotificationType.Buying:
                gameObject.AddComponent<NotificationRendererAcquisition>()
                    .Render(notificationMessage as NotificationMessageBuyingCompany, Title, Description, LinkToEvent);
                break;

            default:
                gameObject.AddComponent<NotificationRendererDefault>()
                    .Render(notificationMessage, Title, Description, LinkToEvent);
                break;
        }
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
}
