using Assets.Core;
using System.Collections.Generic;

public partial class ManageMarketingFinancingSystem : OnPeriodChange
{
    public ManageMarketingFinancingSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        // ai
        foreach (var e in Companies.GetAIProducts(gameContext))
            AutoManageMarketing(e);


        // player
        var playerProducts = Companies.GetPlayerRelatedProducts(gameContext);
        var playerCompany  = Companies.GetPlayerControlledGroupCompany(gameContext);

        foreach (var e in playerProducts)
        {
            if (Companies.IsPlayerFlagship(gameContext, e))
                StartCampaigns(e);
            else
                AutoManageMarketing(e);
        }
    }

    void AutoManageMarketing(GameEntity product)
    {
        // set product upgrades here
        SetUpgrades(product);

        // start campaigns
        StartCampaigns(product);
    }

    void StartCampaigns(GameEntity product)
    {
        if (product.isRelease)
        {
            if (Products.IsUpgradeEnabled(product, ProductUpgrade.BrandCampaign))
                Marketing.StartBrandingCampaign(product, gameContext);

            if (Products.IsUpgradeEnabled(product, ProductUpgrade.TargetingInSocialNetworks))
                Marketing.StartTargetingCampaign(product, gameContext);
        }
    }

    void SetUpgrades(GameEntity product)
    {
        //
        var brandingCost  = Products.GetUpgradeCost(product, gameContext, ProductUpgrade.BrandCampaign);
        var targetingCost = Products.GetUpgradeCost(product, gameContext, ProductUpgrade.TargetingInSocialNetworks);

        var balance = Economy.BalanceOf(product);

        if (product.isRelease)
        {
            // targeting
            if (targetingCost < balance)
            {
                product.productUpgrades.upgrades[ProductUpgrade.TargetingInSocialNetworks] = true;
                balance -= targetingCost;
            }
            else
            {
                product.productUpgrades.upgrades[ProductUpgrade.TargetingInSocialNetworks] = false;
            }

            // copy pasted
            // TODO move to separate function
            // branding
            if (brandingCost < balance)
            {
                product.productUpgrades.upgrades[ProductUpgrade.BrandCampaign] = true;
                balance -= brandingCost;
            }
            else
            {
                product.productUpgrades.upgrades[ProductUpgrade.BrandCampaign] = false;
            }
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
