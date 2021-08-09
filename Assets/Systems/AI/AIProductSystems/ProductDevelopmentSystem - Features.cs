using Assets.Core;
using System.Linq;
using UnityEngine;

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

    void ReduceChurn(GameEntity product, Bonus<float> churn)
    {
        var feature = Products.GetBestFeatureUpgradePossibility(product, gameContext);

        if (feature == null)
            return;

        //while (Products.IsCanUpgradeFeature(product, ))
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

            try
            {
                ReduceChurn(product, churn);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Caught exception while reducing churn in " + product.company.Name);
                Debug.LogError(ex);
            }
        }
        else
        {
            var core = product.team.Teams[0];

            var willMonetize = 0.25f;
            var willInnovate = 0.15f;

            if (!core.Managers.Any())
            {
                Debug.Log("HAS NO MANAGERS in " + product.company.Name);
            }
            else
            {
                var ceo = core.Managers[0];

                bool isGreedy = Humans.HasTrait(ceo, Trait.Greedy);
                bool isCreative = Humans.HasTrait(ceo, Trait.Visionaire) || Humans.HasTrait(ceo, Trait.WantsToCreate);

                if (isGreedy)
                {
                    willMonetize = 0.8f;
                }

                if (isCreative)
                {
                    willInnovate = 0.9f;
                }
            }

            // monetize
            if (!product.isRelease)
            {
                // innovate
            }
            else
            {
                if (Random.Range(0, willMonetize + willInnovate) < willMonetize)
                {
                    // monetize
                }
                else
                {
                    // innovate
                }
            }


            // monetize?

            // go for innovations
        }

        return;
        // random development
        foreach (var f in features)
        {
            Products.TryToUpgradeFeature(product, f, gameContext);
        }
    }
}
