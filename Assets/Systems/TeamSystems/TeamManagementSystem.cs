using Assets.Core;
using Entitas;
using System.Collections.Generic;

class TeamManagementSystem : OnMonthChange
{
    public TeamManagementSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var products = Companies.GetProductCompanies(gameContext);

        for (var i = 0; i < products.Length; i++)
        {
            TeamUtils.ReduceOrganisationPoints(products[i], -2);
        }
    }
}
