using Assets.Core;
using System.Collections.Generic;

public partial class ManageMarketingFinancingSystem : OnPeriodChange
{
    public ManageMarketingFinancingSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var playerFlagshipId = Companies.GetPlayerFlagshipID(gameContext);
        // ai
        foreach (var e in Companies.GetProductCompanies(gameContext))
        {
            if (e.company.Id != playerFlagshipId)
                SetUpgrades(e);
        }
    }

    void SetUpgrades(GameEntity product)
    {
        if (product.isRelease)
        {
            ManageReleasedProducts(product);
        }
        else
        {
            ManagePrototypes(product);
        }
    }

    void ManageReleasedProducts(GameEntity product)
    {
        var balance = Economy.BalanceOf(product);
        var income = Economy.GetCompanyIncome(gameContext, product);

        var managerMaintenance = Economy.GetManagersCost(product, gameContext);

        var brandingCost = Products.GetUpgradeCost(product, gameContext, ProductUpgrade.BrandCampaign);
        var targetingCost = Products.GetUpgradeCost(product, gameContext, ProductUpgrade.TargetingCampaign);

        var tier0 = new List<ProductUpgrade>() { ProductUpgrade.TestCampaign, ProductUpgrade.SimpleConcept, ProductUpgrade.AutorecruitWorkers };
        var tier1 = new List<ProductUpgrade>() { ProductUpgrade.TargetingCampaign, ProductUpgrade.BrandCampaign, ProductUpgrade.QA, ProductUpgrade.Support };
        var tier2 = new List<ProductUpgrade>() { ProductUpgrade.TargetingCampaign2, ProductUpgrade.BrandCampaign2, ProductUpgrade.QA2, ProductUpgrade.Support2 };
        var tier3 = new List<ProductUpgrade>() { ProductUpgrade.TargetingCampaign3, ProductUpgrade.BrandCampaign3, ProductUpgrade.QA3, ProductUpgrade.Support3 };


        balance += income - managerMaintenance;


        // targeting
        if (targetingCost < balance)
        {
            Products.SetUpgrade(product, ProductUpgrade.TargetingCampaign, true);
            balance -= targetingCost;
        }
        else
        {
            Products.SetUpgrade(product, ProductUpgrade.TargetingCampaign, false);
        }

        // copy pasted
        // TODO move to separate function
        // branding
        if (brandingCost < balance)
        {
            Products.SetUpgrade(product, ProductUpgrade.BrandCampaign, true);
            balance -= brandingCost;
        }
        else
        {
            Products.SetUpgrade(product, ProductUpgrade.BrandCampaign, false);
        }
    }

    void ManagePrototypes(GameEntity product)
    {
        if (Companies.IsReleaseableApp(product, gameContext))
        {
            Marketing.ReleaseApp(gameContext, product);
        }
        else
        {
            Products.SetUpgrade(product, ProductUpgrade.TestCampaign, true);
        }
    }
}
