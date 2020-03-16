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
            UpdgradeProduct(product, gameContext);

        ReleaseApps(products);
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


    public static void UpdgradeProduct(GameEntity product, GameContext gameContext)
    {
        if (Cooldowns.HasConceptUpgradeCooldown(gameContext, product))
            return;

        UpgradeProductLevel(product, gameContext);
        UpdateMarketRequirements(product, gameContext);

        Cooldowns.AddConceptUpgradeCooldown(gameContext, product);
    }

    private static void UpgradeProductLevel(GameEntity product, GameContext gameContext)
    {
        var revolutionChance = Products.GetInnovationChance(product, gameContext);

        var revolutionOccured = Random.Range(0, 100) < revolutionChance;

        var upgrade = 1;

        if (revolutionOccured && Products.IsWillInnovate(product, gameContext))
            upgrade = 2;

        product.ReplaceProduct(product.product.Niche, Products.GetProductLevel(product) + upgrade);
    }

    private static long GiveInnovationBenefits(GameEntity product, GameContext gameContext, bool revolution)
    {
        Marketing.AddBrandPower(product, revolution ? Balance.REVOLUTION_BRAND_POWER_GAIN : Balance.INNOVATION_BRAND_POWER_GAIN);

        if (!revolution)
            return 0;

        // get your competitor's clients
        var innovatorCompetitors = Markets.GetProductsOnMarket(gameContext, product)
            .Where(p => p.isRelease)
            .Where(p => p.company.Id != product.company.Id);

        long sum = 0;

        foreach (var p in innovatorCompetitors)
        {
            var disloyal = Marketing.GetClients(p) / 15;

            Marketing.LooseClients(p, disloyal);
            Marketing.AddClients(product, disloyal);

            sum += disloyal;
        }

        return sum;
    }

    private static void UpdateMarketRequirements(GameEntity product, GameContext gameContext)
    {
        var niche = Markets.GetNiche(gameContext, product.product.Niche);

        var demand = Products.GetMarketDemand(niche);
        var newLevel = Products.GetProductLevel(product);

        if (newLevel > demand)
        {
            bool revolution = newLevel - demand > 1;

            // innovation
            var clientChange = GiveInnovationBenefits(product, gameContext, revolution);

            // notify about innovation
            if (Companies.IsInPlayerSphereOfInterest(product, gameContext) && Markets.GetCompetitorsAmount(product, gameContext) > 1)
                NotificationUtils.AddPopup(gameContext, new PopupMessageInnovation(product.company.Id, clientChange));

            niche.ReplaceSegment(newLevel);

            // order matters
            RemoveTechLeaders(product, gameContext);
            product.isTechnologyLeader = true;
        }
        else if (newLevel == demand)
        {
            // if you are techonology leader and you fail to innovate, you will not lose tech leadership
            if (product.isTechnologyLeader)
                return;

            RemoveTechLeaders(product, gameContext);
        }
    }





    private static void RemoveTechLeaders(GameEntity product, GameContext gameContext)
    {
        var players = Markets.GetProductsOnMarket(gameContext, product).ToArray();

        foreach (var p in players)
            p.isTechnologyLeader = false;
    }
}