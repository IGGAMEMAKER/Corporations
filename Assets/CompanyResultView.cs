using Assets.Core;
using UnityEngine.UI;

public class CompanyResultView : View
{
    public LinkToNiche LinkToNiche;
    public Text CompanyName;
    public Text ClientGrowth;
    public Text MarketShareChange;
    public Text ConceptStatusText;
    public Text Profit;

    public ToggleMarketingFinancing SetAggressiveMarketing;
    public ToggleMarketingFinancing SetNormalMarketing;
    public ToggleMarketingFinancing SetZeroMarketing;

    public ReleaseApp ReleaseApp;

    ProductCompanyResult result1;

    public void SetEntity(ProductCompanyResult result)
    {
        result1 = result;

        var product = Companies.GetCompany(GameContext, result.CompanyId);

        CompanyName.text = product.company.Name;

        DrawProductGrowth(product, result);
        DrawProductStatus();

        var profit = Economy.GetProfit(GameContext, result.CompanyId);
        Profit.text = "Profit\n" + Visuals.Colorize(Format.Money(profit), profit > 0);

        LinkToNiche.SetNiche(product.product.Niche);

        SetAggressiveMarketing.SetCompanyId(result.CompanyId);
        SetNormalMarketing.SetCompanyId(result.CompanyId);
        SetZeroMarketing.SetCompanyId(result.CompanyId);

        ReleaseApp.SetCompanyId(result.CompanyId);

        RenderMarketingButtons(result.CompanyId);
    }

    void RenderMarketingButtons(int companyId)
    {
        var company = Companies.GetCompany(GameContext, companyId);
        var financing = company.financing.Financing[Financing.Marketing];

        var isReleased = company.isRelease;

        SetNormalMarketing      .gameObject.SetActive(false);
        SetAggressiveMarketing  .gameObject.SetActive(false);
        SetZeroMarketing        .gameObject.SetActive(false && isReleased && financing > 0);

        ReleaseApp              .gameObject.SetActive(false);
    }

    void DrawProductGrowth(GameEntity product, ProductCompanyResult result)
    {
        //var growth = MarketingUtils.GetAudienceGrowthMultiplier(product, GameContext);
        //var growthMultiplier = MarketingUtils.GetGrowthMultiplier(product, GameContext);

        //ClientGrowth.text = "Client growth\n" + Visuals.PositiveOrNegativeMinified(growth) + "%";
        //ClientGrowth.gameObject.GetComponent<Hint>().SetHint(growthMultiplier.ToString());

        
        var bonus = MarketingUtils.GetBrandChange(product, GameContext);
        var change = bonus.Sum();
        ClientGrowth.text = $"Brand\n{(int)product.branding.BrandPower} ({Visuals.PositiveOrNegativeMinified(change)})";
        ClientGrowth.gameObject.GetComponent<Hint>().SetHint(bonus.ToString());

        var shareChange = (long)result.MarketShareChange;
        var share = Companies.GetMarketShareOfCompanyMultipliedByHundred(product, GameContext);
        MarketShareChange.text = "Market share\n" + Visuals.Colorize(share.ToString(), shareChange >= 0) + "%";
    }

    void DrawProductStatus()
    {
        var conceptStatus = result1.ConceptStatus;
        var color = GetStatusColor(conceptStatus);

        CooldownUtils.TryGetCooldown(GameContext, new CooldownImproveConcept(result1.CompanyId), out Cooldown c1);

        var days = 0;
        if (c1 != null)
            days = c1.EndDate - CurrentIntDate;

        var product = Companies.GetCompany(GameContext, result1.CompanyId);

        var outdatedDescription = conceptStatus == ConceptStatus.Outdated ? $" (-{Products.GetDifferenceBetweenMarketDemandAndAppConcept(product, GameContext)}LVL)" : "";
        ConceptStatusText.text = Visuals.Colorize(conceptStatus.ToString(), color) + outdatedDescription + $"\nUpgrades in {days}d";
    }

    public override void ViewRender()
    {
        base.ViewRender();

        DrawProductStatus();

        RenderMarketingButtons(result1.CompanyId);
    }

    string GetStatusColor (ConceptStatus conceptStatus)
    {
        if (conceptStatus == ConceptStatus.Leader)
            return VisualConstants.COLOR_POSITIVE;

        if (conceptStatus == ConceptStatus.Outdated)
            return VisualConstants.COLOR_NEGATIVE;

        return VisualConstants.COLOR_NEUTRAL;
    }
}
