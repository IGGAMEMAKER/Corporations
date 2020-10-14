using System.Collections.Generic;
using Assets.Core;

public partial class AIClosePoorProductsIfMarketIsDeadSystem : OnPeriodChange
{
    public AIClosePoorProductsIfMarketIsDeadSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var c in Companies.GetAIManagingCompanies(gameContext))
            CloseCompaniesIfNecessary(c);
    }

    void CloseCompaniesIfNecessary(GameEntity group)
    {
        foreach (var holding in Companies.GetDaughterProductCompanies(gameContext, group))
            CloseUnworthyProducts(holding);
    }

    void CloseUnworthyProducts(GameEntity product)
    {
        var niche = Markets.Get(gameContext, product.product.Niche);

        bool isProfitable = Economy.IsProfitable(gameContext, product);
        bool isNicheDead = Markets.GetMarketState(niche) == MarketState.Death;

        if (isNicheDead && !isProfitable)
            Companies.CloseCompany(gameContext, product);
    }
}
