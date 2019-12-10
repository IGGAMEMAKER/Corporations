using Assets.Utils;
using Assets.Utils.Formatting;

public partial class PopupView : View
{
    void RenderCloseCompanyPopup()
    {
        RenderUniversalPopup(
            "Do you want to close this company?",
            "",
            typeof(ClosePopup),
            typeof(CloseCompanyController)
            );
    }


    void RenderInterestToCompany(PopupMessageInterestToCompany popup)
    {
        var target = Companies.GetCompany(GameContext, popup.companyId);
        var buyer = Investments.GetInvestorById(GameContext, popup.buyerInvestorId);

        RenderUniversalPopup(
            $"{buyer.company.Name} wants to buy {target.company.Name}!",
            "If we want to prevent this, we need to send a counter offer!"
            );
    }

    void RenderTargetAcquisition(PopupMessageAcquisitionOfCompanyInOurSphereOfInfluence popup)
    {
        var target = Companies.GetCompany(GameContext, popup.companyId);
        var buyer = Investments.GetInvestorById(GameContext, popup.InterceptorCompanyId);

        RenderUniversalPopup(
            "ACQUISITION!",
            $"Company {buyer.company.Name} BOUGHT {target.company.Name} for {Format.Money(popup.Bid)}!\n\n" +
            $"This move will increase market share of {buyer.company.Name}"
            );
    }

    void RenderMarketChangePopup(PopupMessageMarketPhaseChange popup)
    {
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

            case NicheLifecyclePhase.MassGrowth:
                possibilities = "It's time to earn money now! Company maintenances increase even more";
                break;

            case NicheLifecyclePhase.Decay:
                possibilities = "Market passed it's prime and will decay slowly. Companies will no longer receive new users";
                break;
        }

        RenderUniversalPopup(
            "Market state changed!",
            $"Market of {name} is {state} now!\n\n{possibilities}",
            typeof(ClosePopup)
            );
    }

    void RenderBankruptCompany(PopupMessageCompanyBankrupt popup)
    {
        RenderUniversalPopup(
            "Our competitor is bankrupt!",
            "Company " + GetCompanyName(popup.companyId) + " is bankrupt now!" +
            "\nSome of their clients will start using our product instead"
            );
    }

    void RenderNewCompany(PopupMessageCompanySpawn popup)
    {
        RenderUniversalPopup(
            "New Startup",
            "Company " + GetCompanyName(popup.companyId) + " started it's business. Their approach seems REVOLUTIONARY. They will compete with our products now" +
            "\n\nKeep an eye on them. Perhaps, we can buy them later"
            );
    }

    void RenderPreReleasePopup(PopupMessageDoYouWantToRelease popup)
    {
        RenderUniversalPopup(
            "Do you really want to release this product?",
            "Maintenance cost will increase\nThis product will get 20 brand power",
            typeof(ReleaseAppPopup),
            typeof(ClosePopup)
            );
    }

    void RenderCreateCompanyPopup(PopupMessageCreateApp popup)
    {
        RenderUniversalPopup(
            $"You have created a new company called {GetCompanyName(popup.companyId)}!",
            "You will need some time to match market requirements and after that, you will be able to release your product and make money from it",
            typeof(CloseOKPopup)
            );
    }

    string GetCompanyName(int companyId) => Companies.GetCompanyName(GameContext, companyId);
}