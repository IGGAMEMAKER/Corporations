using Assets.Utils;
using System.Collections.Generic;

public partial class ManageProductMarketingSystem : OnDateChange
{
    public ManageProductMarketingSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in Companies.GetAIProducts(gameContext))
            ManageProduct(e);
    }

    void ManageProduct(GameEntity product)
    {
        if (!product.isRelease && Products.IsInMarket(product, gameContext))
            MarketingUtils.ReleaseApp(product, gameContext);

        //ManageDumpingProduct(product);

        ManageMarketing(product);
    }

    void ManageMarketing(GameEntity product)
    {
        var currentCost = Economy.GetMarketingCost(product, gameContext);
        var nextCost = Economy.GetNextMarketingLevelCost(product, gameContext);

        var diff = nextCost - currentCost;

        // go higher
        //if (EconomyUtils.IsCanMaintain(product, gameContext, diff))
        if (!Companies.IsEnoughResources(product, currentCost))
        {
            var financing = Economy.GetCheaperFinancing(product);

            Products.SetMarketingFinancing(product, financing);
        }

        if (Companies.IsEnoughResources(product, nextCost))
            Products.SetMarketingFinancing(product, Economy.GetNextMarketingFinancing(product));
    }

    void ManageDumpingProduct(GameEntity product)
    {
        var willBeBankruptIn6Months = !Companies.IsEnoughResources(product, Economy.GetProductCompanyMaintenance(product, gameContext) * 6);
        var hasMoneyToDumpSafely = !willBeBankruptIn6Months;

        var hasLowMarketShare = Companies.GetMarketShareOfCompanyMultipliedByHundred(product, gameContext) < 10;

        var competitorIsDumpingToo = MarketingUtils.HasDumpingCompetitors(gameContext, product);
        var isOutdated = Products.IsOutOfMarket(product, gameContext);

        var needsToDump = isOutdated || hasLowMarketShare || competitorIsDumpingToo;


        if (needsToDump && hasMoneyToDumpSafely)
        {
            var monthlyDumpingChance = 2f;
            var chance = UnityEngine.Random.Range(0f, 1f);

            var wantsToDump = chance < monthlyDumpingChance / 30f;

            if (wantsToDump)
                Products.StartDumping(gameContext, product);
        }
        else
            Products.StopDumping(gameContext, product);
    }
}
