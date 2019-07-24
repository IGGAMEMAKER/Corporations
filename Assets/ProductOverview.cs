using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductOverview : View
{
    public Text DevelopmentSpeed;
    public Text Expertise;
    public SetAmountOfStars TeamSpeed;

    public Text Popularity;
    public Text AppQuality;

    public override void ViewRender()
    {
        base.ViewRender();

        var speed = TeamUtils.GetPerformance(GameContext, SelectedCompany);
        DevelopmentSpeed.text = $"Development Speed: {speed}$";

        var expertise = 115;
        Expertise.text = $"Expertise: {expertise}";

        var strength = 5;
        TeamSpeed.SetStars(strength);

        var position = NicheUtils.GetPositionOnMarket(GameContext, SelectedCompany);
        Popularity.text = $"Position on market: #{position}";

        var quality = ProductUtils.GetProductLevel(SelectedCompany);
        var status = ProductUtils.GetSegmentMarketDemand(SelectedCompany, GameContext, UserType.Core);
        AppQuality.text = $"App quality: {quality} ({status})";
    }
}
