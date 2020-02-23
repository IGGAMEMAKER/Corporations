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
            var ideaPerExpertise = Balance.IDEA_PER_EXPERTISE;

            var expertiseLevel = product.companyResource.Resources.ideaPoints / ideaPerExpertise;

            if (expertiseLevel > 0)
            {
                product.expertise.ExpertiseLevel += expertiseLevel;

                Companies.SpendResources(product, new TeamResource(0, 0, 0, expertiseLevel * ideaPerExpertise, 0));
            }
        }

        // release AI apps if can
        var releasableAIApps = products
            .Where(p => Companies.IsReleaseableApp(p, gameContext))
            
            .Where(p => !Companies.IsPlayerFlagship(gameContext, p))
            ;

        foreach (var concept in releasableAIApps)
            Marketing.ReleaseApp(gameContext, concept);
    }





    public static void UpdgradeProduct(GameEntity product, GameContext gameContext, bool IgnoreCooldowns = false)
    {
        if (Cooldowns.HasConceptUpgradeCooldown(gameContext, product) && !IgnoreCooldowns)
            return;

        UpgradeProductLevel(product, gameContext);
        UpdateMarketRequirements(product, gameContext);

        Cooldowns.AddConceptUpgradeCooldown(gameContext, product);
    }

    private static void UpgradeProductLevel(GameEntity product, GameContext gameContext)
    {
        var revolutionChance = Mathf.Sqrt(Products.GetInnovationChance(product, gameContext));

        var revolutionOccured = Random.Range(0, 100) < revolutionChance;
        var needsToUpgrade = !Products.IsWillInnovate(product, gameContext);

        if (revolutionOccured || needsToUpgrade)
            product.ReplaceProduct(product.product.Niche, Products.GetProductLevel(product) + 1);
    }

    private static long GiveInnovationBenefits(GameEntity product, GameContext gameContext)
    {
        Marketing.AddBrandPower(product, Balance.INNOVATION_BRAND_POWER_GAIN);

        // get your competitor's clients
        var products = Markets.GetProductsOnMarket(gameContext, product)
            .Where(p => p.isRelease)
            .Where(p => p.company.Id != product.company.Id);

        long sum = 0;
        foreach (var p in products)
        {
            var disloyal = Marketing.GetClients(p) / 6;

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
            // innovation
            var clientChange = GiveInnovationBenefits(product, gameContext);
            NotifyAboutInnovation(product, gameContext, clientChange);

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





    public static void NotifyAboutInnovation(GameEntity product, GameContext gameContext, long clients)
    {
        if (Companies.IsInPlayerSphereOfInterest(product, gameContext) && Markets.GetCompetitorsAmount(product, gameContext) > 1)
            NotificationUtils.AddPopup(gameContext, new PopupMessageInnovation(product.company.Id, clients));
    }

    private static void RemoveTechLeaders(GameEntity product, GameContext gameContext)
    {
        var players = Markets.GetProductsOnMarket(gameContext, product).ToArray();

        foreach (var p in players)
            p.isTechnologyLeader = false;
    }
}