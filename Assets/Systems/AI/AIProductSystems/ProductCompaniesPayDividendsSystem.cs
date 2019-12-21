using Assets.Utils;
using System.Collections.Generic;
using System.Linq;

public partial class ProductCompaniesPayDividendsSystem : OnPeriodChange
{
    public ProductCompaniesPayDividendsSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var dependantProducts = Companies.GetProductCompanies(gameContext).Where(p => !p.isIndependentCompany);

        foreach (var e in dependantProducts)
        {
            long dividends = e.companyResource.Resources.money;
            Companies.PayDividends(gameContext, e, dividends);
        }
    }
}