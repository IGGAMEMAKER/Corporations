using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositioningView : View
{
    public Text PositioningName;
    public Text IncomePerUser;
    public Text EstimatedUsers;

    int positioning;
    public void SetEntity(int positioning)
    {
        this.positioning = positioning;

        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var nicheType = SelectedCompany.product.Niche;
        var positionings = NicheUtils.GetNichePositionings(nicheType, GameContext);
        var niche = NicheUtils.GetNicheEntity(GameContext, nicheType);

        var positioningData = positionings[positioning];


        PositioningName.text = positioningData.name;

        var price = niche.nicheCosts.BasePrice * 1000 * 12;
        IncomePerUser.text = $"+{price}";

        var estimatedUsers = positioningData.marketShare * NicheUtils.GetMarketAudiencePotential(niche) / 100;
        EstimatedUsers.text = Format.Minify(estimatedUsers);
    }
}
