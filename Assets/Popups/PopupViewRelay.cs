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

            default:
                RenderUniversalPopup(
                    popup.PopupType.ToString(),
                    popup.PopupType.ToString() + " description. This Popup was not filled! ",
                    typeof(ClosePopup)
                    );
                break;
        }
    }
}