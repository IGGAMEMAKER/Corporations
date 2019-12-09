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

    public Text PositioningLabel;

    public Text Brand;

    public override void ViewRender()
    {
        base.ViewRender();

        if (!SelectedCompany.hasProduct)
            return;

        var canShowData = Companies.IsExploredCompany(GameContext, SelectedCompany);

        //var expertise = CompanyUtils.GetCompanyExpertise(SelectedCompany);
        var expertise = ProductUtils.GetInnovationChance(SelectedCompany, GameContext);
        Expertise.text = $"Innovation chance: {expertise}%";


        var speed = TeamUtils.GetPerformance(GameContext, SelectedCompany);
        DevelopmentSpeed.text = $"Development Speed: {speed}%";

        var strength = TeamUtils.GetTeamRating(GameContext, SelectedCompany);
        TeamSpeed.SetStars(strength);

        RenderCommonInfo();
    }

    void RenderCommonInfo()
    {
        var position = NicheUtils.GetPositionOnMarket(GameContext, SelectedCompany) + 1;
        Popularity.text = $"Position on market: #{position}";


        var quality = ProductUtils.GetProductLevel(SelectedCompany);
        var demand =  ProductUtils.GetMarketDemand(SelectedCompany, GameContext);
        var status =  ProductUtils.GetConceptStatus(SelectedCompany, GameContext);

        AppQuality.text = $"Concept: {quality} / {demand} ({status})";


        var brand = SelectedCompany.branding.BrandPower;
        Brand.text = $"Brand: {brand}";

        var posTextual = NicheUtils.GetCompanyPositioning(SelectedCompany, GameContext);
        PositioningLabel.text = "Positioning: " + posTextual;
    }
}
