using Assets.Core;
using System.Collections.Generic;
using UnityEngine;

class EmployeeSystem : OnMonthChange
{
    public EmployeeSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        Debug.Log("Employee system");

        var playerCompany = Companies.GetPlayerCompany(gameContext);

        var products = Companies.GetPlayerRelatedProducts(gameContext);

        for (var i = 0; i < products.Length; i++)
        {
            TeamUtils.ShaffleEmployees(products[i], gameContext);
        }

        TeamUtils.ShaffleEmployees(playerCompany, gameContext);
    }
}