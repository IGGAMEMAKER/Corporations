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

    public Text Brand;

    public override void ViewRender()
    {
        base.ViewRender();

        if (!SelectedCompany.hasProduct)
            return;

        var speed = TeamUtils.GetPerformance(GameContext, SelectedCompany);
        DevelopmentSpeed.text = $"Development Speed: {speed}%";

        var expertise = CompanyUtils.GetCompanyExpertise(SelectedCompany);
        Expertise.text = $"Expertise: {expertise}%";

        var strength = TeamUtils.GetAverageTeamRating(GameContext, SelectedCompany);
        TeamSpeed.SetStars(strength);

        var position = NicheUtils.GetPositionOnMarket(GameContext, SelectedCompany) + 1;
        Popularity.text = $"Position on market: #{position}";

        var quality = ProductUtils.GetProductLevel(SelectedCompany);
        var status = ProductUtils.GetMarketDemand(SelectedCompany, GameContext, UserType.Core);
        AppQuality.text = $"App quality: {quality} ({status})";

        var brand = SelectedCompany.branding.BrandPower;
        Brand.text = $"Brand strength: {brand}";
    }
}
