using Assets.Utils;
using System.Collections.Generic;
using System.Linq;

public partial class ProductCompaniesPayDividendsSystem : OnPeriodChange
{
    public ProductCompaniesPayDividendsSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var dependantProducts = Companies.GetDependentProducts(gameContext);

        var aiProducts      = dependantProducts.Where(p => !Companies.IsCompanyRelatedToPlayer(gameContext, p));
        var playerProducts  = dependantProducts.Where(p => Companies.IsCompanyRelatedToPlayer(gameContext, p));

        foreach (var e in playerProducts)
        {
            long dividends = Companies.BalanceOf(e);
            Companies.PayDividends(gameContext, e, dividends);
        }

        foreach (var e in aiProducts)
        {
            var maintenance = Economy.GetCompanyMaintenance(gameContext, e.company.Id);

            long dividends = Companies.BalanceOf(e) - maintenance;

            if (dividends > 0)
                Companies.PayDividends(gameContext, e, dividends);
        }
    }
}