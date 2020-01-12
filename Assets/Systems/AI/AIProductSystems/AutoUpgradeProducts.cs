using Assets.Core;
using System.Collections.Generic;
using System.Linq;

public partial class AutoUpgradeProductsSystem : OnDateChange
{
    public AutoUpgradeProductsSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var products = Companies.GetProductCompanies(gameContext);

        foreach (var product in products)
        {
            var ideaPerExpertise = Constants.IDEA_PER_EXPERTISE;

            var expertiseLevel = product.companyResource.Resources.ideaPoints / ideaPerExpertise;

            if (expertiseLevel > 0)
            {
                product.expertise.ExpertiseLevel += expertiseLevel;

                Companies.SpendResources(product, new TeamResource(0, 0, 0, expertiseLevel * ideaPerExpertise, 0));
            }

            Products.UpdgradeProduct(product, gameContext);
        }

        // release AI apps if can
        var releasableAIApps = products
            // increase performance
            .Where(p => !p.isRelease)
            .Where(p => Companies.IsReleaseableApp(p, gameContext))
            
            .Where(p => !Companies.IsRelatedToPlayer(gameContext, p))
            ;

        foreach (var concept in releasableAIApps)
            MarketingUtils.ReleaseApp(concept, gameContext);
    }
}