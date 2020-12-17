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
            Teams.ShaffleEmployees(products[i], gameContext);
        }

        Teams.ShaffleEmployees(playerCompany, gameContext);
    }
}