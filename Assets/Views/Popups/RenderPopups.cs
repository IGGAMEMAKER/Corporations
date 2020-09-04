using Assets;
using Assets.Core;
using System;
using System.Collections.Generic;

public partial class PopupView : View
{
    void RenderCloseCompanyPopup()
    {
        RenderUniversalPopup(
            "Do you want to close this company?",
            "",
            typeof(ClosePopupCancel),
            typeof(CloseCompanyPopupButton)
            );
    }


    void RenderInterestToCompany(PopupMessageInterestToCompany popup)
    {
        var target = Companies.Get(Q, popup.companyId);
        var buyer = Investments.GetInvestorById(Q, popup.buyerInvestorId);

        bool isOurCompany = Companies.IsRelatedToPlayer(Q, target);

        RenderUniversalPopup(
            $"{buyer.company.Name} wants to buy {target.company.Name}!",
            isOurCompany ? "They are waiting your response" : "If we want to prevent this, we need to send a counter offer!",
            typeof(ClosePopupOK)
            );
    }

    void RenderTargetAcquisition(PopupMessageAcquisitionOfCompanyInOurSphereOfInfluence popup)
    {
        var target = Companies.Get(Q, popup.companyId);
        var buyer = Investments.GetInvestorById(Q, popup.InterceptorCompanyId);

        RenderUniversalPopup(
            "ACQUISITION!",
            $"Company {buyer.company.Name} BOUGHT {target.company.Name} for {Format.Money(popup.Bid)}!\n\n" +
            $"This move will increase market share of {buyer.company.Name}",
            typeof(ClosePopupOK)
            );
    }

    void RenderMarketChangePopup(PopupMessageMarketPhaseChange popup)
    {
        var name = Enums.GetFormattedNicheName(popup.NicheType);
        var state = Markets.GetMarketState(Q, popup.NicheType);
        var possibilities = "";

        switch (state)
        {
            case MarketState.Innovation:
                possibilities = "It's time to be first! Your innovation chances in this niche increase by 25%";
                break;

            case MarketState.Trending:
                possibilities = "It seems, that this market has the potential! Companies on this market will get way more clients, but maintenance cost will also increase";
                break;

            case MarketState.MassGrowth:
                possibilities = "It's time to earn money now! Company maintenances increase even more";
                break;

            case MarketState.Decay:
                possibilities = "Market passed it's prime and will decay slowly. Companies will no longer receive new users";
                break;
        }

        RenderUniversalPopup(
            "Market state changed!",
            $"Market of {name} is {state} now!\n\n{possibilities}",
            typeof(ClosePopupOK)
            );
    }

    void RenderBankruptCompany(PopupMessageCompanyBankrupt popup)
    {
        var niche = Companies.Get(Q, popup.companyId).product.Niche;
        bool hasCompaniesOnMarket = Companies.HasCompanyOnMarket(MyCompany, niche, Q);

        var description = "Company " + GetCompanyName(popup.companyId) + " is bankrupt now!";
        if (hasCompaniesOnMarket)
            description += "\nSome of their clients will start using our products instead";

        RenderUniversalPopup(
            "Our competitor is bankrupt!",
            description,
            typeof(ClosePopupOK)
            );
    }

    void RenderNewCompany(PopupMessageCompanySpawn popup)
    {
        RenderUniversalPopup(
            "New Startup",
            "Company " + GetCompanyName(popup.companyId) + " started it's business. Their approach seems REVOLUTIONARY." +
            "\n\nKeep an eye on them. Perhaps, we can buy them later",
            typeof(ClosePopupOK)
            );
    }

    void RenderPreReleasePopup(PopupMessageDoYouWantToRelease popup)
    {
        RenderUniversalPopup(
            "Do you really want to release this product?",
            
            Visuals.Negative("Maintenance cost will increase") +
            Visuals.Positive("\nThis product will get a lot of new clients") +
            Visuals.Positive("\nYou will be able to make partnerships") +
            Visuals.Positive("\n\nYour income will increase!"),
            
            typeof(ReleaseAppPopupButton),
            typeof(ClosePopupCancel)
            );
    }

    void RenderReleasePopup(PopupMessageRelease popup)
    {
        RenderUniversalPopup(
            "You've released the product!",
            Visuals.Positive($"You got new users and {C.RELEASE_BRAND_POWER_GAIN} Brand Power!") +
            "\n\nIncrease Brand power if you want to get more clients!"
            //"\n\nBrand grows if you form partnerships, make innovations and capture markets"
            ,
            typeof(ClosePopupOK)
            );
    }

    void RenderInnovatorPopup(PopupMessageInnovation popup)
    {
        bool ourInnovation = Companies.IsRelatedToPlayer(Q, popup.companyId);

        bool isRevolution = popup.clientGain > 0;

        var brandGain = isRevolution ? C.REVOLUTION_BRAND_POWER_GAIN : C.INNOVATION_BRAND_POWER_GAIN;
        var innovationBenefit = $"They will get {brandGain} Brand Power";

        if (isRevolution)
            innovationBenefit += $" and {Format.Minify(popup.clientGain)} clients from their competitors";

        var innovationDescription = isRevolution ? "a REVOLUTION" : "an innovaton";

        RenderUniversalPopup(
            $"Company {GetCompanyName(popup.companyId)} made {innovationDescription}!",
            Visuals.Colorize(innovationBenefit, ourInnovation),
            typeof(ClosePopupOK)
            );
    }

    void RenderDefectedManager(PopupMessageWorkerWantsToWorkInYourCompany popup)
    {
        var human = Humans.GetHuman(Q, popup.humanId);

        var role = Humans.GetRole(human);
        var formattedRole = Humans.GetFormattedRole(role);

        var rating = Humans.GetRating(Q, human);

        RenderUniversalPopup(
        "You can hire new manager from your competitors!",
        
        $"<b>{formattedRole}</b> {Humans.GetFullName(human)} <b>({rating}LVL)</b> will join your company.\n\n" + 
        Visuals.Positive("\n\nDo you want to hire?"),
        
        typeof(WorkerJoinsYourCompanyPopupButton),
        typeof(ClosePopupNO)
        );
    }

    void RenderDisloyalManager(PopupMessageWorkerLeavesYourCompany popup)
    {
        var human = Humans.GetHuman(Q, popup.humanId);

        var role = Humans.GetRole(human);
        var formattedRole = Humans.GetFormattedRole(role);

        var rating = Humans.GetRating(Q, human);

        RenderUniversalPopup(
            "Manager doesn't want to work in your company anymore!",
            $"{formattedRole} {Humans.GetFullName(human)} ({rating}LVL) will leave your company.\n\n" + Visuals.Negative("Managers leave if they don't like corporate culture"),
            typeof(WorkerLeavesYourCompanyPopupButton)
            );
    }

    void RenderCreateCompanyPopup(PopupMessageCreateApp popup)
    {
        RenderUniversalPopup(
            $"You have created a new company called {GetCompanyName(popup.companyId)}!",
            "You will need some time to match market requirements and after that, you will be able to release your product and make money from it",
            typeof(ClosePopupOK)
            );
    }

    void RenderHasTooManyPartners(PopupMessageTooManyPartners popup)
    {
        bool we = popup.companyId == MyCompany.company.Id;
        RenderUniversalPopup(
            (we ? "We" : "They") + " have too many partnerships already!",
            (we ? "We" : "They") + " need to break one partnership to become partners",
            typeof(ClosePopupOK)
            );
    }

    void RenderInspirationPopup(PopupMessageMarketInspiration popup)
    {
        RenderUniversalPopup(
            "INSPIRATION",
            "On your spare time you got new revolutionary ideas! You can create first " + Enums.GetFormattedNicheName(popup.NicheType).ToUpper(),
            typeof(InspirationPopupButton)
            );
    }

    void RenderDoYouReallyWantToCreateAPrototype(PopupMessageDoYouWantToCreateApp popup)
    {
        // check if has enough resources
        var maintenance = Markets.GetBiggestMaintenanceOnMarket(Q, popup.NicheType);

        bool hasResources = Economy.IsCanMaintain(MyCompany, Q, maintenance);

        var title = "Do you really want to create a new " + Enums.GetFormattedNicheName(popup.NicheType) + "?";
        //var description = $"We need at least {Format.Money(startCapital)} to create a product, which meets market requirements";

        var description = $"On release, this product will cost you about {Format.Money(maintenance)} each month";
        if (maintenance == 0)
            description = "We don't know, how much it will cost on release. Create app to find out! Be the innovator!";


        var resourceText = "";
        if (!hasResources && Companies.GetDaughterCompaniesAmount(MyCompany, Q) > 0)
            resourceText = Visuals.Negative("\nIt's too expensive!");


        RenderUniversalPopup(
            title,
            "", /*description + resourceText,*/
            typeof(CreateAppPopupButton),
            typeof(ClosePopupCancel)
            );
    }

    void RenderBankruptcyThreat(PopupMessageBankruptcyThreat popup)
    {
        RenderUniversalPopup(
            "BANKRUPTCY IS COMING!",
            "YOU HAVE ONE DAY TO SAVE YOUR COMPANY!\n\n" + Visuals.Negative("Raise investments, fire workers, close not profitable companies!"),
            typeof(ClosePopupOK)
            //typeof(AutomaticInvestmentPickButton)
            //typeof(DeclareBankruptcyPopupButton)
            );
    }

    void RenderGameOverMessage(PopupMessageGameOver popup)
    {
        RenderUniversalPopup(
            "YOU ARE BANKRUPT!",
            Visuals.Negative("<b><i>GAME OVER</i></b>") + "\nYou will be luckier next time. The game will be closed. Restart it!",
            typeof(ClosePopupExitGame)
            );
    }

    void RenderStrategicPartnershipMessage(PopupMessageStrategicPartnership popup)
    {
        bool signed = Companies.IsHaveStrategicPartnershipAlready(popup.companyId, popup.companyId2, Q);

        var target = GetCompanyName(popup.companyId2);

        SoundManager.Play(Sound.SignContract);

        RenderUniversalPopup(
            signed ? $"We are strategic partners with {target}!" : $"We have revoked our strategic partnership with {target}",
            signed ? "Our products will get more brand power every month" : "They are worthless and we don't need their help anymore!",
            typeof(ClosePopupOK)
            );
    }

    void RenderSimplePopupInfo(PopupMessageInfo popup)
    {
        RenderUniversalPopup(popup.Title, popup.Description, typeof(ClosePopupOK));
    }

    void RenderNotEnoughWorkersMessage(PopupMessageNeedMoreWorkers popup)
    {
        RenderUniversalPopup(
            "You need more workers",
            "Hire more teams to do this",
            typeof(ClosePopupOK)
            );
    }

    void RenderNewCorporationSpawn(PopupMessageCorporationSpawn popup)
    {
        RenderUniversalPopup(
            "YOU HAVE CREATED A CORPORATION!!!",
            Visuals.Positive("You can buy and manage way more companies than before!") +
            "\n\nBECOME the RICHEST COMPANY in the world!",
            typeof(ClosePopupOK)
            );
    }

    void RenderNewCorporationRequirements(PopupMessageCorporationRequirements popup)
    {
        var cost = Economy.GetCompanyCost(Q, popup.companyId);
        var goal = C.CORPORATION_REQUIREMENTS_COMPANY_COST;

        RenderUniversalPopup(
            "You cannot create a corporation :(",
            $"Your company costs {Format.Money(cost)}, but needs to cost at least {Format.Money(goal)} to become a corporation",
            typeof(ClosePopupCancel)
            );
    }

    void RenderCorporateCultureChanges(PopupMessageCultureChange popup)
    {
        RenderUniversalPopup(
            "You changed the corporate culture!",
            "{improvement_description}\n\nYou will be able to do this again in " + C.CORPORATE_CULTURE_CHANGES_DURATION + " days",
            typeof(ClosePopupOK)
            );
    }

    void RenderAcquisitionOfferResponse(PopupMessageAcquisitionOfferResponse popup)
    {
        var positiveResponse = "They " + Visuals.Positive("ACCEPTED") + " our offer!\n\nPress OK to buy a company";
        var negativeResponse = "They " + Visuals.Negative("DECLINED") + " our offer!\n\nSend another offer to buy this company";

        bool willAccept = Companies.IsCompanyWillAcceptAcquisitionOffer(Q, popup.companyId, popup.buyerInvestorId);

        List<Type> buttons = new List<Type>();
        
        if (willAccept)
        {
            buttons.Add(typeof(AcquireCompanyPopupButton));
        }
        else
        {
            buttons.Add(typeof(SendAnotherAcquisitionOfferPopupButton));
        }

        buttons.Add(typeof(ClosePopupCancel));

        RenderUniversalPopup(
            "Response from company " + GetCompanyName(popup.companyId),
            willAccept ? positiveResponse : negativeResponse,
            buttons.ToArray()
            );
    }

    string GetCompanyName(int companyId) => Companies.GetCompanyName(Q, companyId);
}