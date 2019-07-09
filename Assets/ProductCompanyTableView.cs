using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class ProductCompanyTableView : View
{
    public SetAmountOfStars SetAmountOfStars;
    [SerializeField] Text AudienceGrowth;

    GameEntity entity;
    bool QuarterlyOrYearly;

    public void SetEntity(GameEntity company, bool quarterOrYearly)
    {
        entity = company;
        QuarterlyOrYearly = quarterOrYearly;

        Render();
    }

    public void Render()
    {
        if (entity == null)
            return;

        RenderAudienceGrowth();

        var niche = NicheUtils.GetNicheEntity(GameContext, entity.product.Niche);
        var rating = NicheUtils.GetMarketRating(niche);
        SetAmountOfStars.SetStars(rating);
    }

    void RenderAudienceGrowth()
    {
        var monthly = CompanyUtils.GetAudienceGrowth(entity, 3);
        var yearly = CompanyUtils.GetAudienceGrowth(entity, 12);

        var quarGrowth = monthly == 0 ? "???" : Format.Sign(monthly) + "%";
        var yrGrowth = yearly == 0 ? "???" : Format.Sign(yearly) + "%";

        AudienceGrowth.text = QuarterlyOrYearly ? quarGrowth : yrGrowth;


        //AudienceGrowth.text = $"{monGrowth} / {yrGrowth}";
    }
}
