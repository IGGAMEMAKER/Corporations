using Assets.Core;
using System.Collections.Generic;

class EmployeeSystem : OnMonthChange
{
    public EmployeeSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var playerCompany = Companies.GetPlayerCompany(gameContext);

        var products = Companies.GetPlayerRelatedProducts(gameContext);

        for (var i = 0; i < products.Length; i++)
        {
            Teams.ShuffleEmployees(products[i], gameContext);
        }

        Teams.ShuffleEmployees(playerCompany, gameContext);
    }
}