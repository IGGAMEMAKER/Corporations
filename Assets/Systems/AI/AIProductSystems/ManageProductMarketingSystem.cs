using Assets.Core;
using System.Collections.Generic;

public partial class ManageMarketingFinancingSystem : OnPeriodChange
{
    public ManageMarketingFinancingSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in Companies.GetAIProducts(gameContext))
            ManageMarketing(e);

        var playerProducts = Companies.GetPlayerRelatedProducts(gameContext);
        foreach (var e in playerProducts)
        {
            if (!Companies.IsPlayerFlagship(gameContext, e))
                ManageMarketing(e);
        }
    }

    void ManageMarketing(GameEntity product)
    {
        var brandingCost = Marketing.GetBrandingCampaignCost(product, gameContext);
        var targetingCost = Marketing.GetTargetingCampaignCost(product, gameContext);



        if (product.isRelease)
        {
            if (Companies.IsEnoughResources(product, brandingCost))
                Marketing.StartBrandingCampaign(product, gameContext);

            if (Companies.IsEnoughResources(product, targetingCost))
                Marketing.StartTargetingCampaign(product, gameContext);
        }
        else
        {
            if (Companies.IsReleaseableApp(product, gameContext))
                Marketing.ReleaseApp(gameContext, product);
            else
                Marketing.StartTestCampaign(product, gameContext);
        }
    }
}
