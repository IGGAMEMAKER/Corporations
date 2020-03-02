using Assets.Core;
using System.Collections.Generic;

public partial class ManageMarketingFinancingSystem : OnPeriodChange
{
    public ManageMarketingFinancingSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        // ai
        foreach (var e in Companies.GetAIProducts(gameContext))
            ManageMarketing(e);


        // player
        var playerProducts = Companies.GetPlayerRelatedProducts(gameContext);
        var playerCompany = Companies.GetPlayerControlledGroupCompany(gameContext);

        foreach (var e in playerProducts)
        {
            if (!Companies.IsPlayerFlagship(gameContext, e))
                ManageMarketing(e);
            else
                ManageFlagship(e, playerCompany);
        }
    }

    void ManageFlagship(GameEntity product, GameEntity group)
    {
        var brandingCost = Marketing.GetBrandingCost(product, gameContext);
        var targetingCost = Marketing.GetTargetingCost(product, gameContext);

        var upgrs = product.productUpgrades.upgrades;

        var brand = Products.IsUpgradeEnabled(product, ProductUpgrade.BrandCampaign);
        var targeting = Products.IsUpgradeEnabled(product, ProductUpgrade.TargetingInSocialNetworks);

        if (product.isRelease)
        {
            if (Companies.IsEnoughResources(group, brandingCost) && brand)
            {
                Companies.SupportCompany(group, product, brandingCost);

                Marketing.StartBrandingCampaign(product, gameContext);
            }

            if (Companies.IsEnoughResources(group, targetingCost) && targeting)
            {
                Companies.SupportCompany(group, product, targetingCost);

                Marketing.StartTargetingCampaign(product, gameContext);
            }
        }
    }

    void StartCampaigns(GameEntity product)
    {
        var brandingCost = Marketing.GetBrandingCost(product, gameContext);
        var targetingCost = Marketing.GetTargetingCost(product, gameContext);



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

    void ManageMarketing(GameEntity product)
    {
        // set product upgrades here

        // start campaigns
        StartCampaigns(product);
    }
}
