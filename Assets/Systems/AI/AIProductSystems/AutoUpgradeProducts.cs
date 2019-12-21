using Assets.Utils;
using System.Collections.Generic;

public partial class AutoUpgradeProducts : OnDateChange
{
    public AutoUpgradeProducts(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in Companies.GetProductCompanies(gameContext))
            Products.UpdgradeProduct(e, gameContext);


    }
}