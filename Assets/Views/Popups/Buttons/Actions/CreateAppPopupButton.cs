using Assets.Core;
using System.Linq;
using UnityEngine;

public class CreateAppPopupButton : PopupButtonController<PopupMessageDoYouWantToCreateApp>
{
    public override void Execute()
    {
        if (Popup == null)
            return;

        NicheType nicheType = Popup.NicheType;

        var id = Companies.CreateProductAndAttachItToGroup(Q, nicheType, MyCompany);

        NotificationUtils.ClosePopup(Q);
        NotificationUtils.AddPopup(Q, new PopupMessageCreateApp(id));

        // had no products before
        if (Companies.GetDaughterCompaniesAmount(MyCompany, Q) == 1)
        {
            var company = Companies.Get(Q, id);
            company.isFlagship = true;
            company.AddChannelExploration(new System.Collections.Generic.Dictionary<int, int>(), new System.Collections.Generic.List<int>(), 1);

            var infos = Marketing.GetAudienceInfos();

            var positionings = Markets.GetNichePositionings(nicheType, Q);
            var positioningWorths = positionings.OrderBy(Markets.GetPositioningValue);

            var rand = Random.Range(0, infos.Count / 2);
            company.productPositioning.Positioning = positioningWorths.ToArray()[rand].ID;
            
            //NavigateToNiche(company.product.Niche);
            Navigate(ScreenMode.HoldingScreen, C.MENU_SELECTED_NICHE, company.product.Niche);

            var CEO = Humans.GetHuman(Q, Companies.GetCEOId(company));

            var salary = Teams.GetSalaryPerRating(CEO);
            Teams.SetJobOffer(CEO, company, new JobOffer(salary), 0);
        }

    }

    public override string GetButtonName() => "YES";
}

