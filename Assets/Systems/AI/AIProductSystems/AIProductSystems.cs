using Assets.Utils;
using Assets.Utils.Formatting;
using System.Collections.Generic;

public partial class AIProductSystems : OnDateChange
{
    public AIProductSystems(Contexts contexts) : base(contexts) {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in CompanyUtils.GetProductCompanies(gameContext))
            ManageProduct(e);

        //foreach (var e in CompanyUtils.GetAIProducts(gameContext))
        //{
        //    var val = RandomEnum<ProductImprovement>.GenerateValue();

        //    ProductUtils.UpgradeProductImprovement(val, e);
        //}
    }

    void ManageProduct(GameEntity product)
    {
        ProductUtils.UpdgradeProduct(product, gameContext);

        if (!product.isRelease && ProductUtils.IsInMarket(product, gameContext))
            MarketingUtils.ReleaseApp(product, gameContext);

        ManageDumpingProduct(product);
    }

    void ManageDumpingProduct(GameEntity product)
    {
        var isOutdated = ProductUtils.IsOutOfMarket(product, gameContext);

        var willBeBankruptIn6Months = !CompanyUtils.IsEnoughResources(product, EconomyUtils.GetProductCompanyMaintenance(product, gameContext) * 6);
        var hasMoneyToDumpSafely = !willBeBankruptIn6Months;
        var hasLowMarketShare = CompanyUtils.GetMarketShareOfCompanyMultipliedByHundred(product, gameContext) < 25;

        var monthlyDumpingChance = 5f;
        var wantsToDump = UnityEngine.Random.Range(0f, 1f) < monthlyDumpingChance / 30f;

        var competitorIsDumpingToo = MarketingUtils.HasDumpingCompetitors(gameContext, product);

        var needsToDump = (isOutdated || hasLowMarketShare || competitorIsDumpingToo);

        if (needsToDump && hasMoneyToDumpSafely)
        {
            if (wantsToDump)
                ProductUtils.StartDumping(gameContext, product);
        }
        else
            ProductUtils.StopDumping(gameContext, product);
    }
}