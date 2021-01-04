using Assets.Core;

public class CreateAppPopupButton : PopupButtonController<PopupMessageDoYouWantToCreateApp>
{
    public override void Execute()
    {
        if (Popup == null)
            return;

        NicheType nicheType = Popup.NicheType;

        var company = Companies.CreateProductAndAttachItToGroup(Q, nicheType, MyCompany);
        var id = company.company.Id;

        NotificationUtils.ClosePopup(Q);
        NotificationUtils.AddPopup(Q, new PopupMessageCreateApp(id));

        // had no products before
        if (Companies.GetDaughtersAmount(MyCompany) == 1)
        {
            Companies.TurnProductToPlayerFlagship(company, Q, nicheType, MyCompany);

            Navigate(ScreenMode.HoldingScreen, C.MENU_SELECTED_NICHE, company.product.Niche);
        }

    }

    public override string GetButtonName() => "YES";
}

