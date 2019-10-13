using Assets.Utils;
using System.Collections.Generic;

public partial class AIProductSystems : OnMonthChange
{
    public AIProductSystems(Contexts contexts) : base(contexts) {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in CompanyUtils.GetProductCompanies(gameContext))
            UpgradeSegment(e);

        foreach (var e in CompanyUtils.GetAIProducts(gameContext))
        {
            ProductUtils.UpgradeProductImprovement(ProductImprovement.Acquisition, e);
            ProductUtils.UpgradeProductImprovement(ProductImprovement.Monetisation, e);
        }
    }
}