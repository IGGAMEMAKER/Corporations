using Assets.Core;
using Entitas;
using System.Collections.Generic;
using UnityEngine;

class IncreaseOrganisationSystem : OnPeriodChange
{
    public IncreaseOrganisationSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var products = Companies.GetProductCompanies(gameContext);

        for (var i = 0; i < products.Length; i++)
        {
            var p = products[i];

            for (var teamId = 0; teamId < p.team.Teams.Count; teamId++)
            {
                var team = p.team.Teams[teamId];

                var change = Teams.GetOrganisationChanges(team, p, gameContext).Sum() / 10f;

                team.Organisation = Mathf.Clamp(team.Organisation + change, 0, 100);
            }
        }
    }
}
