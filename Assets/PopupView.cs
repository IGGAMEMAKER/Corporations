using Assets.Utils;
using Assets.Utils.Formatting;
using System.Collections.Generic;
using UnityEngine.UI;

public class PopupView : View
{
    public Text Title;
    public Text Description;
    public PopupButtonsContainer popupButtonsContainer;

    PopupMessage PopupMessage;

    List<System.Type> ButtonComponents = new List<System.Type>();


    //public override void ViewRender()
    private void OnEnable()
    {
        var popup = NotificationUtils.GetPopupMessage(GameContext);

        PopupMessage = popup;

        ButtonComponents.Clear();

        switch (popup.PopupType)
        {
            case PopupType.CloseCompany: RenderCloseCompanyPopup(); break;
            case PopupType.MarketChanges: RenderMarketChangePopup(popup as PopupMessageMarketPhaseChange); break;
            default:
                Title.text = popup.PopupType.ToString();
                Description.text = popup.PopupType.ToString() + " descr ";
                break;
        }

        popupButtonsContainer.SetComponents(ButtonComponents);
    }

    void SetTitle(string text)
    {
        Title.text = text;
    }

    void SetDescription(string text)
    {
        Description.text = text;
    }

    void AddComponent(System.Type type)
    {
        ButtonComponents.Add(type);
    }


    void RenderCloseCompanyPopup()
    {
        SetTitle("Do you want to close this company?");
        SetDescription("");

        AddComponent(typeof(ClosePopup));
        AddComponent(typeof(CloseCompanyController));
    }

    void RenderMarketChangePopup(PopupMessageMarketPhaseChange popup)
    {
        SetTitle("Market state changed!");
        SetDescription("Market of " + EnumUtils.GetFormattedNicheName(popup.NicheType) + " is " + NicheUtils.GetMarketState(GameContext, popup.NicheType) + " now!");

        AddComponent(typeof(ClosePopup));
    }
}
