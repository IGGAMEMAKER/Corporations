using Assets.Utils;
using System.Collections.Generic;

public partial class BaseProductSystems : OnDateChange
{
    public BaseProductSystems(Contexts contexts) : base(contexts) {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in CompanyUtils.GetProductCompanies(gameContext))
            ProductUtils.UpdgradeProduct(e, gameContext);

        foreach (var e in CompanyUtils.GetAIProducts(gameContext))
        {
            ManageProduct(e);
        }
    }

    void ManageProduct(GameEntity product)
    {
        if (!product.isRelease && ProductUtils.IsInMarket(product, gameContext))
            MarketingUtils.ReleaseApp(product, gameContext);

        ManageDumpingProduct(product);

        ManageMarketing(product);
    }

    void ManageMarketing(GameEntity product)
    {
        var financing = EconomyUtils.GetProductMarketingCost(product, gameContext);

        var nextFinancing = product.financing.Financing[Financing.Marketing];
    }

    void ManageDumpingProduct(GameEntity product)
    {
        return;
        var willBeBankruptIn6Months = !CompanyUtils.IsEnoughResources(product, EconomyUtils.GetProductCompanyMaintenance(product, gameContext) * 6);
        var hasMoneyToDumpSafely = !willBeBankruptIn6Months;

        var hasLowMarketShare = CompanyUtils.GetMarketShareOfCompanyMultipliedByHundred(product, gameContext) < 10;

        var competitorIsDumpingToo = MarketingUtils.HasDumpingCompetitors(gameContext, product);
        var isOutdated = ProductUtils.IsOutOfMarket(product, gameContext);

        var needsToDump = isOutdated || hasLowMarketShare || competitorIsDumpingToo;


        if (needsToDump && hasMoneyToDumpSafely)
        {
            var monthlyDumpingChance = 2f;
            var chance = UnityEngine.Random.Range(0f, 1f);

            var wantsToDump = chance < monthlyDumpingChance / 30f;

            if (wantsToDump)
                ProductUtils.StartDumping(gameContext, product);
        }
        else
            ProductUtils.StopDumping(gameContext, product);
    }
}