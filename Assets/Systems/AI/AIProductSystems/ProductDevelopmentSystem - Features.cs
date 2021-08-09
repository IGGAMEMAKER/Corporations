using Assets.Core;
using System.Linq;

public partial class ProductDevelopmentSystem
{
    void Monetize(GameEntity product)
    {
        var remainingFeatures = Products.GetUpgradeableMonetizationFeatures(product);
        var churn = Marketing.GetChurnRate(product, gameContext);

        if (remainingFeatures.Any() && churn < 4)
        {
            var feature = remainingFeatures.First();

            Products.TryToUpgradeFeature(product, feature, gameContext);

            Companies.LogSuccess(product, $"Added {feature.Name} for profit");
        }
    }

    void DeMonetize(GameEntity product)
    {
        var remainingFeatures = Products.GetMonetisationFeatures(product);

        foreach (var f in remainingFeatures)
        {
            Products.RemoveFeature(product, f.Name, gameContext);
        }
    }

    static NewProductFeature GetBestFeatureUpgradePossibility(GameEntity product, GameContext gameContext)
    {
        var marketRequirements = Markets.GetMarketRequirementsForCompany(gameContext, product);
        var features = Products.GetAllFeaturesForProduct();

        var sortedByImportance = features
            .OrderBy(f =>
            {
                if (f.IsMonetizationFeature)
                    return 10;

                var index = features.ToList().FindIndex(ff => f.Name == ff.Name);

                return marketRequirements.Features[index] - Products.GetFeatureRating(product, f.Name);
            });

        return sortedByImportance.FirstOrDefault();
    }

    void ReduceChurn(GameEntity product, Bonus<float> churn)
    {
        var feature = GetBestFeatureUpgradePossibility(product, gameContext);

        if (feature == null)
            return;

        while (Products.IsCanUpgradeFeatures(product))
            Products.TryToUpgradeFeature(product, feature, gameContext);
    }

    void Innovate(GameEntity product)
    {

    }

    void ManageFeatures(GameEntity product)
    {
        var features = Products.GetUpgradeableRetentionFeatures(product);
        var churn = Marketing.GetChurnRate(product, gameContext, true);

        if (churn.Sum() > 3)
        {
            // need to lower churn ASAP

            ReduceChurn(product, churn);
        }
        else
        {
            var ceo = Humans.Get(gameContext, product.team.Teams[0].Managers[0]);

            bool isGreedy = Humans.HasTrait(ceo, Trait.Greedy);
            bool isCreative = Humans.HasTrait(ceo, Trait.Visionaire) || Humans.HasTrait(ceo, Trait.WantsToCreate);

            var willMonetize = isGreedy ? 0.8f : 0.25f;
            var willInnovate = isCreative ? 0.9f : 0.15f;

            // monetize
            if (!product.isRelease)
                willMonetize = 0;


            // monetize?

            // go for innovations
            return;
        }

        // random development
        foreach (var f in features)
        {
            Products.TryToUpgradeFeature(product, f, gameContext);
        }
    }
}
