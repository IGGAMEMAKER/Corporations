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
        var positionings = NicheUtils.GetNichePositionings(nicheType, GameContext);
        var niche = NicheUtils.GetNiche(GameContext, nicheType);

        var positioningData = positionings[segmentId];


        PositioningName.text = positioningData.name;

        var price = NicheUtils.GetSegmentProductPrice(GameContext, nicheType, segmentId);
        IncomePerUser.text = $"+{price.ToString("0.0")}";

        var estimatedUsers = NicheUtils.GetMarketSegmentAudiencePotential(GameContext, nicheType, segmentId);
        EstimatedUsers.text = Format.Minify(estimatedUsers);
    }
}
