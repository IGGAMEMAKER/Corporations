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
        var price = niche.nicheCosts.BasePrice;

        var positioningData = positionings[positioning];

        PositioningName.text = positioningData.name;
        IncomePerUser.text = price.ToString();

        var estimatedUsers = positioningData.marketShare * NicheUtils.GetMarketAudiencePotential(niche) / 100;
        EstimatedUsers.text = estimatedUsers.ToString();
    }
}
