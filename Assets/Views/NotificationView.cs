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

    public void SetMessage(NotificationMessage message)
    {
        int notificationId = transform.GetSiblingIndex();


        
        switch (message.NotificationType)
        {
            case NotificationType.NicheTrends:
                gameObject.AddComponent<NotificationRendererTrendsChange>()
                    .Render(message as NotificationMessageTrendsChange, Title, Description, LinkToEvent, Panel);
                break;

            case NotificationType.NewCompanyOnNiche:
                gameObject.AddComponent<NotificationRendererNewCompany>()
                    .Render(message as NotificationMessageNewCompany, Title, Description, LinkToEvent, Panel);
                break;

            case NotificationType.InnovationOnNiche:
                gameObject.AddComponent<NotificationRendererMarketInnovation>()
                    .Render(message as NotificationMessageInnovation, Title, Description, LinkToEvent, Panel);
                break;

            case NotificationType.InvestmentRoundStarted:
                gameObject.AddComponent<NotificationRendererInvestmentRound>()
                    .Render(message as NotificationMessageInvestmentRoundStarted, Title, Description, LinkToEvent, Panel);
                break;

            case NotificationType.CompanyTypeChange:
                gameObject.AddComponent<NotificationRendererPromoteCompany>()
                    .Render(message as NotificationMessageCompanyTypeChange, Title, Description, LinkToEvent, Panel);
                break;

            case NotificationType.Bankruptcy:
                gameObject.AddComponent<NotificationRendererBankruptcy>()
                    .Render(message as NotificationMessageBankruptcy, Title, Description, LinkToEvent, Panel);
                break;

            case NotificationType.Buying:
                gameObject.AddComponent<NotificationRendererAcquisition>()
                    .Render(message as NotificationMessageBuyingCompany, Title, Description, LinkToEvent, Panel);
                break;

            default:
                gameObject.AddComponent<NotificationRendererDefault>()
                    .Render(message, Title, Description, LinkToEvent, Panel);
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
