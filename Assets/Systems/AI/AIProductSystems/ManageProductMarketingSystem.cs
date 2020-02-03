using Assets.Core;
using System.Collections.Generic;

public partial class ManageMarketingFinancingSystem : OnPeriodChange
{
    public ManageMarketingFinancingSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in Companies.GetAIProducts(gameContext))
            ManageProduct(e);
    }

    void ManageProduct(GameEntity product)
    {
        //ManageDumpingProduct(product);

        ManageMarketing(product);
    }

    void ManageMarketing(GameEntity product)
    {
        var currentCost = Economy.GetRegularCampaignCost(product, gameContext);
        var nextCost = Economy.GetNextMarketingLevelCost(product, gameContext);

        // reduce maintenance
        if (!Companies.IsEnoughResources(product, currentCost))
            Products.SetMarketingFinancing(product, Economy.GetCheaperFinancing(product));

        // go higher
        if (Companies.IsEnoughResources(product, nextCost))
            Products.SetMarketingFinancing(product, Economy.GetNextMarketingFinancing(product));
    }

    void ManageDumpingProduct(GameEntity product)
    {
        var willBeBankruptIn6Months = !Companies.IsEnoughResources(product, Economy.GetProductCompanyMaintenance(product, gameContext) * 6);
        var hasMoneyToDumpSafely = !willBeBankruptIn6Months;

        var hasLowMarketShare = Companies.GetMarketShareOfCompanyMultipliedByHundred(product, gameContext) < 10;

        var competitorIsDumpingToo = Marketing.HasDumpingCompetitors(gameContext, product);
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
        {
            Products.StopDumping(gameContext, product);
        }
    }
}
