using Assets.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class AutoUpgradeProductsSystem : OnDateChange
{
    public AutoUpgradeProductsSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var products = Companies.GetProductCompanies(gameContext);

        foreach (var product in products)
        {
            GenerateIdeas(product, gameContext);
            UpdgradeProduct(product, gameContext);

            Products.ScaleTeam(product, gameContext);
        }

        ReleaseApps(products);
    }

    public static void GenerateIdeas(GameEntity product, GameContext gameContext)
    {
        var speed = Products.GetTotalDevelopmentEffeciency(gameContext, product);

        Companies.AddResources(product, new TeamResource(0, 0, 0, speed, 0));
    }

    public static void UpdgradeProduct(GameEntity product, GameContext gameContext)
    {
        var upgradeCost = new TeamResource(0, 0, 0, Products.GetIterationTimeCost(product, gameContext), 0);

        if (Companies.IsEnoughResources(product, upgradeCost))
        {
            Companies.SpendResources(product, upgradeCost);

            var previousAmountOfChannels = Markets.GetAmountOfAvailableChannels(gameContext, product);

            Products.UpgradeProductLevel(product, gameContext);
            Markets.UpdateMarketRequirements(product, gameContext);

            var currentAmountOfChannels = Markets.GetAmountOfAvailableChannels(gameContext, product);

            if (previousAmountOfChannels != currentAmountOfChannels)
            {
                // notify player about new channels
                if (Companies.IsPlayerFlagship(gameContext, product))
                    NotificationUtils.AddGameEvent(gameContext, new GameEventNewMarketingChannel());
            }

            Investments.CompleteGoal(product, gameContext);
        }
    }

    void ReleaseApps(GameEntity[] products)
    {
        var playerFlagshipId = Companies.GetPlayerFlagshipID(gameContext);

        // release all nonflagship apps if can
        var releasableNonPlayerFlagshipProducts = products
            .Where(p => Companies.IsReleaseableApp(p, gameContext))

            .Where(p => p.company.Id != playerFlagshipId)
            ;

        foreach (var concept in releasableNonPlayerFlagshipProducts)
            Marketing.ReleaseApp(gameContext, concept);
    }
}