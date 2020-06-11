public partial class PopupView : View
{
    void RenderPopup(PopupMessage popup)
    {
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

            case PopupType.CreatePrototype:
                RenderCreateCompanyPopup(popup as PopupMessageCreateApp);
                break;

            case PopupType.TooManyPartners:
                RenderHasTooManyPartners(popup as PopupMessageTooManyPartners);
                break;

            case PopupType.InspirationToOpenMarket:
                RenderInspirationPopup(popup as PopupMessageMarketInspiration);
                break;

            case PopupType.CreatePrototypeWarning:
                RenderDoYouReallyWantToCreateAPrototype(popup as PopupMessageDoYouWantToCreateApp);
                break;

            case PopupType.BankruptcyThreat:
                RenderBankruptcyThreat(popup as PopupMessageBankruptcyThreat);
                break;

            case PopupType.GameOver:
                RenderGameOverMessage(popup as PopupMessageGameOver);
                break;

            case PopupType.StrategicPartnership:
                RenderStrategicPartnershipMessage(popup as PopupMessageStrategicPartnership);
                break;

            case PopupType.NotEnoughWorkers:
                RenderNotEnoughWorkersMessage(popup as PopupMessageNeedMoreWorkers);
                break;

            case PopupType.NewCorporation:
                RenderNewCorporationSpawn(popup as PopupMessageCorporationSpawn);
                break;

            case PopupType.CorporationRequirementsWarning:
                RenderNewCorporationRequirements(popup as PopupMessageCorporationRequirements);
                break;

            case PopupType.CorporateCultureChange:
                RenderCorporateCultureChanges(popup as PopupMessageCultureChange);
                break;

            case PopupType.AcquisitionOfferResponse:
                RenderAcquisitionOfferResponse(popup as PopupMessageAcquisitionOfferResponse);
                break;

            case PopupType.Release:
                RenderReleasePopup(popup as PopupMessageRelease);
                break;

            case PopupType.Innovator:
                RenderInnovatorPopup(popup as PopupMessageInnovation);
                break;

            case PopupType.DisloyalManager:
                RenderDisloyalManager(popup as PopupMessageWorkerLeavesYourCompany);
                break;

            case PopupType.DefectedManager:
                RenderDefectedManager(popup as PopupMessageWorkerWantsToWorkInYourCompany);
                break;

            default:
                RenderUniversalPopup(
                    popup.PopupType.ToString(),
                    popup.PopupType.ToString() + " description. This Popup was not filled! ",
                    typeof(ClosePopupCancel)
                    );
                break;
        }
    }
}