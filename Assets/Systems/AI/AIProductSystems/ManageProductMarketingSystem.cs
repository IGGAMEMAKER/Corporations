using Assets.Core;
using System.Collections.Generic;

public partial class ManageMarketingFinancingSystem : OnPeriodChange
{
    public ManageMarketingFinancingSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var playerFlagshipId = Companies.GetPlayerFlagshipID(gameContext);

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

    long CheckCosts(GameEntity product, List<ProductUpgrade> upgradeSets, long balance)
    {
        var newBalance = balance;

        foreach (var u in upgradeSets)
        {
            var cost = Products.GetUpgradeCost(product, gameContext, u);
            var workerCost = Products.GetUpgradeWorkerCost(product, gameContext, u);

            var totalCost = cost + workerCost;

            if (totalCost < newBalance)
            {
                Products.SetUpgrade(product, u, gameContext, true);
                newBalance -= cost;
            }
            else
            {
                Products.SetUpgrade(product, u, gameContext, false);
            }
        }

        return newBalance;
    }

    void ManageReleasedProducts(GameEntity product)
    {
        var balance = Economy.BalanceOf(product);
        var income = Economy.GetCompanyIncome(gameContext, product);

        var managerMaintenance = Economy.GetManagersCost(product, gameContext);
        balance += income - managerMaintenance;

        var tier0 = new List<ProductUpgrade>() { ProductUpgrade.TestCampaign, ProductUpgrade.SimpleConcept };
        var tier1 = new List<ProductUpgrade>() { ProductUpgrade.TargetingCampaign, ProductUpgrade.BrandCampaign, ProductUpgrade.QA, ProductUpgrade.Support };
        var tier2 = new List<ProductUpgrade>() { ProductUpgrade.TargetingCampaign2, ProductUpgrade.BrandCampaign2, ProductUpgrade.QA2, ProductUpgrade.Support2 };
        var tier3 = new List<ProductUpgrade>() { ProductUpgrade.TargetingCampaign3, ProductUpgrade.BrandCampaign3, ProductUpgrade.QA3, ProductUpgrade.Support3 };

        balance = CheckCosts(product, tier0, balance);
        balance = CheckCosts(product, tier1, balance);
        balance = CheckCosts(product, tier2, balance);
        balance = CheckCosts(product, tier3, balance);
    }

    void ManagePrototypes(GameEntity product)
    {
        if (Companies.IsReleaseableApp(product, gameContext))
        {
            Marketing.ReleaseApp(gameContext, product);
        }
        else
        {
            Products.SetUpgrade(product, ProductUpgrade.TestCampaign, gameContext, true);
        }
    }
}
