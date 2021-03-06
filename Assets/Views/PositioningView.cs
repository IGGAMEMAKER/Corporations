﻿using Assets.Core;
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
        var positionings = Markets.GetNichePositionings(nicheType, Q);
        var niche = Markets.Get(Q, nicheType);

        var positioningData = positionings[segmentId];


        PositioningName.text = positioningData.name;

        var price = Markets.GetBaseProductPrice(nicheType, Q);
        IncomePerUser.text = $"+{price.ToString("0.0")}";

        var estimatedUsers = Markets.GetMarketSegmentAudiencePotential(Q, nicheType, segmentId);
        EstimatedUsers.text = Format.Minify(estimatedUsers);
    }
}
