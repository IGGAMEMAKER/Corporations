using Entitas;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MarketPotentialView : View
{
    NicheType NicheType;

    public Text PotentialMarketSize;
    public Text PotentialAudienceSize;
    public Text PotentialIncomeSize;
    public Text IterationCost;
    public CompetingCompaniesListView CompetitorsContainer;

    public GameObject CompetingCompanyPrefab;

    GameEntity[] GetProductsOnNiche(NicheType niche)
    {
        return Array.FindAll(
            GameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Company, GameMatcher.Product)),
            c => c.product.Niche == niche
            );
    }

    public void SetEntity(NicheType niche)
    {
        NicheType = niche;

        PotentialMarketSize.text = "10M ... 100M";
        PotentialAudienceSize.text = "10M ... 100M";
        PotentialIncomeSize.text = "1$ ... 10$";

        IterationCost.text = "100";

        GameEntity[] entities = GetProductsOnNiche(niche);

        CompetitorsContainer.SetItems(entities);
    }
}
