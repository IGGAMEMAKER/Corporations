using Assets.Core;
using System.Collections.Generic;
using UnityEngine;

public partial class ManageProductUpgradesSystem : OnPeriodChange
{
    public ManageProductUpgradesSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var playerFlagshipId = Companies.GetPlayerFlagshipID(gameContext);

        foreach (var product in Companies.GetProductCompanies(gameContext))
        {
            if (product.company.Id != playerFlagshipId)
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
        }
    }

    long CheckChannelCosts(GameEntity product, GameEntity channel, long balance, ref List<string> str)
    {
        var newBalance = balance;

        var cost = Marketing.GetMarketingActivityCost(product, gameContext, channel);
        var workerCost = 0; // Products.GetUpgradeWorkerCost(product, gameContext, u);

        var totalCost = cost + workerCost;

        if (totalCost < newBalance)
        {
            Marketing.EnableChannelActivity(product, gameContext, channel);

            newBalance -= totalCost;
        }

        else
        {
            Marketing.DisableChannelActivity(product, gameContext, channel);
        }

        return newBalance;
    }

    long CheckCosts(GameEntity product, List<ProductUpgrade> upgradeSets, long balance, ref List<string> str)
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

                if (totalCost > 0)
                    str.Add($"enabled {u} for Cash ({Format.Money(cost)}) and Workers ({Format.Money(workerCost)}) ... Total: {Format.Money(totalCost)}");

                newBalance -= totalCost;
            }
            else
            {
                str.Add("disabled " + u);
                Products.SetUpgrade(product, u, gameContext, false);
            }
        }

        return newBalance;
    }

    void ManageReleasedProducts(GameEntity product)
    {
        List<string> str = new List<string>(); ;

        var balance = Economy.BalanceOf(product);
        str.Add($"------------------ {product.company.Name} (#{product.creationIndex}) -------------------");
        str.Add($"Balance: " + Format.Money(balance));

        var income = Economy.GetCompanyIncome(gameContext, product);
        str.Add("Income: " + Format.Money(income));

        var managerMaintenance = Economy.GetManagersCost(product, gameContext);
        var totalFunds = balance + income - managerMaintenance;
        str.Add("Money available for upgrades: " + Format.Money(totalFunds));

        var tier0 = new List<ProductUpgrade>() { ProductUpgrade.TestCampaign, ProductUpgrade.SimpleConcept };
        var tier1 = new List<ProductUpgrade>() { ProductUpgrade.QA, ProductUpgrade.Support };
        var tier2 = new List<ProductUpgrade>() { ProductUpgrade.QA2, ProductUpgrade.Support2 };
        var tier3 = new List<ProductUpgrade>() { ProductUpgrade.QA3, ProductUpgrade.Support3 };

        totalFunds = CheckCosts(product, tier0, totalFunds, ref str);
        str.Add("Checking tier0: " + Format.Money(totalFunds));

        totalFunds = CheckCosts(product, tier1, totalFunds, ref str);
        str.Add("Checking tier1: " + Format.Money(totalFunds));

        totalFunds = CheckCosts(product, tier2, totalFunds, ref str);
        str.Add("Checking tier2: " + Format.Money(totalFunds));

        totalFunds = CheckCosts(product, tier3, totalFunds, ref str);
        str.Add("End balance: " + Format.Money(totalFunds));

        bool isTestCompany = false; // !Economy.IsProfitable(gameContext, product); // product.company.Id == 15;

        if (isTestCompany)
        {
            foreach (var s in str)
                Debug.Log(s);
        }

        var channels = Markets.GetAvailableMarketingChannels(gameContext, product);

        foreach (var c in channels)
        {
            totalFunds = CheckChannelCosts(product, c, totalFunds, ref str);
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
            Products.SetUpgrade(product, ProductUpgrade.TestCampaign, gameContext, true);
        }
    }
}
