using Assets.Utils;
using Assets.Utils.Formatting;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class PopupView : View
{
    public Text Title;
    public Text Description;
    public PopupButtonsContainer popupButtonsContainer;
    public Text AmountOfMessages;

    PopupMessage PopupMessage;

    List<Type> ButtonComponents = new List<Type>();

    private void OnEnable()
    {
        Render();
    }


    public override void ViewRender()
    {
        Render();
    }

    void Render()
    {
        var messagesCount = NotificationUtils.GetPopups(GameContext).Count;

        if (messagesCount == 0)
            return;

        var popup = NotificationUtils.GetPopupMessage(GameContext) ?? null;

        PopupMessage = popup;

        ButtonComponents.Clear();

        AmountOfMessages.text = "Messages: " + messagesCount;

        switch (popup.PopupType)
        {
            case PopupType.CloseCompany:
                RenderCloseCompanyPopup();
                break;

            case PopupType.MarketChanges:
                RenderMarketChangePopup(popup as PopupMessageMarketPhaseChange);
                break;

            case PopupType.BankruptCompany:
                RenderBankruptCompany(popup as PopupMessageCompanyBankrupt);
                break;

            case PopupType.NewCompany:
                RenderNewCompany(popup as PopupMessageCompanySpawn);
                break;

            case PopupType.InterestToCompanyInOurSphereOfInfluence:
                RenderInterestToCompany(popup as PopupMessageInterestToCompany);
                break;

            case PopupType.TargetWasBought:
                RenderTargetAcquisition(popup as PopupMessageAcquisitionOfCompanyInOurSphereOfInfluence);
                break;

            case PopupType.PreRelease:
                RenderPreReleasePopup(popup as PopupMessageDoYouWantToRelease);
                break;

            default:
                RenderUniversalPopup(
                    popup.PopupType.ToString(),
                    popup.PopupType.ToString() + " description. This Popup was not filled! ",
                    typeof(ClosePopup)
                    );
                //Title.text = popup.PopupType.ToString();
                //Description.text = popup.PopupType.ToString() + " description. This Popup was not filled! ";
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

    void AddComponent(Type type)
    {
        ButtonComponents.Add(type);
    }


    void RenderUniversalPopup(string title, string description, params Type[] buttons)
    {
        SetTitle(title);
        SetDescription(description);

        foreach (var b in buttons)
            AddComponent(b);

        if (buttons.Length == 0)
            AddComponent(typeof(ClosePopup));
    }
    void RenderCloseCompanyPopup()
    {
        RenderUniversalPopup(
            "Do you want to close this company?",
            "",
            typeof(ClosePopup),
            typeof(CloseCompanyController)
            );
    }


    private void RenderInterestToCompany(PopupMessageInterestToCompany popup)
    {
        var target = CompanyUtils.GetCompanyById(GameContext, popup.companyId);
        var buyer = InvestmentUtils.GetInvestorById(GameContext, popup.buyerInvestorId);

        RenderUniversalPopup(
            $"{buyer.company.Name} wants to buy {target.company.Name}!",
            "If we want to prevent this, we need to send a counter offer!"
            );
    }

    private void RenderTargetAcquisition(PopupMessageAcquisitionOfCompanyInOurSphereOfInfluence popup)
    {
        var target = CompanyUtils.GetCompanyById(GameContext, popup.companyId);
        var buyer = InvestmentUtils.GetInvestorById(GameContext, popup.InterceptorCompanyId);

        RenderUniversalPopup(
            "ACQUISITION!",
            $"Company {buyer.company.Name} BOUGHT {target.company.Name} for {Format.Money(popup.Bid)}!\n\n" +
            $"This move will increase market share of {buyer.company.Name}"
            );
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
        RenderUniversalPopup(
            "Our competitor is bankrupt!",
            "Company " + CompanyUtils.GetCompanyById(GameContext, popup.companyId).company.Name + " is bankrupt now!" +
            "\nSome of their clients will start using our product instead"
            );
    }

    void RenderNewCompany(PopupMessageCompanySpawn popup)
    {
        RenderUniversalPopup(
            "New Startup",
            "Company " + CompanyUtils.GetCompanyById(GameContext, popup.companyId).company.Name + " started it's business. Their approach seems REVOLUTIONARY. They will compete with our products now" +
            "\n\nKeep an eye on them. Perhaps, we can buy them later"
            );
    }

    void RenderPreReleasePopup(PopupMessageDoYouWantToRelease popup)
    {
        RenderUniversalPopup(
            "Do you really want to release this product?",
            "Maintenance cost will increase\nThis product will get 20 brand power",

            );
    }
}
