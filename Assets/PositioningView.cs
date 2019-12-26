using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositioningView : View
{
    public Text PositioningName;
    public Text IncomePerUser;
    public Text EstimatedUsers;

    int segmentId;
    public void SetEntity(int positioning)
    {
        segmentId = positioning;

        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var nicheType = SelectedCompany.product.Niche;
        var positionings = Markets.GetNichePositionings(nicheType, GameContext);
        var niche = Markets.GetNiche(GameContext, nicheType);

        var positioningData = positionings[segmentId];


        PositioningName.text = positioningData.name;

        var price = Markets.GetSegmentProductPrice(GameContext, nicheType, segmentId);
        IncomePerUser.text = $"+{price.ToString("0.0")}";

        var estimatedUsers = Markets.GetMarketSegmentAudiencePotential(GameContext, nicheType, segmentId);
        EstimatedUsers.text = Format.Minify(estimatedUsers);
    }
}
