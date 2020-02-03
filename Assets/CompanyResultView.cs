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

        var product = Companies.Get(Q, result.CompanyId);

        CompanyName.text = product.company.Name;

        DrawProductGrowth(product, result);
        DrawProductStatus();

        var profit = Economy.GetProfit(Q, result.CompanyId);
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
        var company = Companies.Get(Q, companyId);
        var financing = Economy.GetMarketingFinancing(company);

        var isReleased = company.isRelease;

        SetNormalMarketing      .gameObject.SetActive(false);
        SetAggressiveMarketing  .gameObject.SetActive(false);
        SetZeroMarketing        .gameObject.SetActive(false && isReleased && financing > 0);

        ReleaseApp              .gameObject.SetActive(false);
    }

    void DrawProductGrowth(GameEntity product, ProductCompanyResult result)
    {        
        var bonus = Marketing.GetBrandChange(product, Q);
        var change = bonus.Sum();
        ClientGrowth.text = $"Brand\n{(int)product.branding.BrandPower} ({Visuals.PositiveOrNegativeMinified(change)})";
        ClientGrowth.gameObject.GetComponent<Hint>().SetHint(bonus.ToString());

        var shareChange = (long)result.MarketShareChange;
        var share = Companies.GetMarketShareOfCompanyMultipliedByHundred(product, Q);
        MarketShareChange.text = "Market share\n" + Visuals.Colorize(share.ToString(), shareChange >= 0) + "%";
    }

    void DrawProductStatus()
    {
        //var conceptStatus = result1.ConceptStatus;
        //var color = GetStatusColor(conceptStatus);

        //Cooldowns.TryGetCooldown(Q, new CooldownImproveConcept(result1.CompanyId), out Cooldown c1);

        //var days = 0;
        //if (c1 != null)
        //    days = c1.EndDate - CurrentIntDate;

        //var product = Companies.Get(Q, result1.CompanyId);

        //var outdatedDescription = conceptStatus == ConceptStatus.Outdated ? $" (-{Products.GetDifferenceBetweenMarketDemandAndAppConcept(product, Q)}LVL)" : "";
        //ConceptStatusText.text = Visuals.Colorize(conceptStatus.ToString(), color) + outdatedDescription + $"\nUpgrades in {days}d";
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
            return Colors.COLOR_POSITIVE;

        if (conceptStatus == ConceptStatus.Outdated)
            return Colors.COLOR_NEGATIVE;

        return Colors.COLOR_NEUTRAL;
    }
}
