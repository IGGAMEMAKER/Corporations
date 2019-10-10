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
            case PopupType.BankruptCompany: RenderBankruptCompany(popup as PopupMessageCompanyBankrupt); break;
            case PopupType.NewCompany: RenderNewCompany(popup as PopupMessageCompanySpawn); break;


            default:
                Title.text = popup.PopupType.ToString();
                Description.text = popup.PopupType.ToString() + " description. This Popup was not filled! ";
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

        var name = EnumUtils.GetFormattedNicheName(popup.NicheType);
        var state = NicheUtils.GetMarketState(GameContext, popup.NicheType);
        var possibilities = "";

        switch (state)
        {
            case NicheLifecyclePhase.Innovation:
                possibilities = "It's time to be first! Your innovation chances in this niche increase by 25%";
                break;

            case NicheLifecyclePhase.Trending:
                possibilities = "It seems, that this market has the potential! Companies on this market will get way more clients, but maintenance cost will also increase";
                break;

            case NicheLifecyclePhase.MassUse:
                possibilities = "It's time to earn money now! Company maintenances increase even more";
                break;

            case NicheLifecyclePhase.Decay:
                possibilities = "Market passed it's prime and will decay slowly. Companies will no longer receive new users";
                break;
        }

        SetDescription($"Market of {name} is {state} now!\n\n{possibilities}");

        AddComponent(typeof(ClosePopup));
    }

    void RenderBankruptCompany(PopupMessageCompanyBankrupt popup)
    {
        SetTitle("Our competitor is bankrupt!");
        SetDescription("Company " + CompanyUtils.GetCompanyById(GameContext, popup.companyId).company.Name + " is bankrupt now!" +
            "\nSome of their clients will start using our product instead");

        AddComponent(typeof(ClosePopup));
    }

    void RenderNewCompany(PopupMessageCompanySpawn popup)
    {
        SetTitle("New Startup");
        SetDescription("Company " + CompanyUtils.GetCompanyById(GameContext, popup.companyId).company.Name + " started it's business. They will compete with our products now" +
            "\nKeep an eye on them. Perhaps, we can buy them later");

        AddComponent(typeof(ClosePopup));
    }
}
