using Entitas;
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

    public void SetEntity(NicheType niche)
    {
        NicheType = niche;

        PotentialMarketSize.text = "10M ... 100M";
        PotentialAudienceSize.text = "10M ... 100M";
        PotentialIncomeSize.text = "1$ ... 10$";

        IterationCost.text = "100";

        GameEntity[] entities = GameContext.GetEntities(GameMatcher.Company);

        CompetitorsContainer.SetItems(entities);
    }
}
