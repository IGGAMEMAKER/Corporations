using Assets.Utils;
using Assets.Utils.Formatting;
using System.Collections.Generic;

public partial class AIProductSystems : OnDateChange
{
    public AIProductSystems(Contexts contexts) : base(contexts) {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in CompanyUtils.GetProductCompanies(gameContext))
            UpgradeSegment(e);

        //foreach (var e in CompanyUtils.GetAIProducts(gameContext))
        //{
        //    var val = RandomEnum<ProductImprovement>.GenerateValue();

        //    ProductUtils.UpgradeProductImprovement(val, e);
        //}
    }

    void UpgradeSegment(GameEntity product)
    {
        ProductUtils.UpdgradeProduct(product, gameContext);
        //ProductUtils.UpgradeExpertise(product, gameContext);
    }
}