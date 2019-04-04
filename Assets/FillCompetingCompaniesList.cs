using Assets.Utils;
using Entitas;
using System;

public class FillCompetingCompaniesList : View
{
    public CompetingCompaniesListView CompetingCompaniesListView;

    GameEntity[] GetProductsOnNiche(NicheType niche)
    {
        return Array.FindAll(
            GameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Company, GameMatcher.Product)),
            c => c.product.Niche == niche
            );
    }

    private void OnEnable()
    {
        NicheType niche = MenuUtils.GetNiche(GameContext);

        GameEntity[] entities = GetProductsOnNiche(niche);

        CompetingCompaniesListView.SetItems(entities);
    }
}
