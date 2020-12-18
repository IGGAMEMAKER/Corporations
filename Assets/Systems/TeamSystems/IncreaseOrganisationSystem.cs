using Assets.Core;
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

            var pointChange = (int)-Teams.GetManagerPointChange(p, gameContext).Sum();
            
            p.companyResource.Resources.Spend(new TeamResource(0, pointChange, 0,0,0));

            // for (var teamId = 0; teamId < p.team.Teams.Count; teamId++)
            // {
            //     var team = p.team.Teams[teamId];
            //
            //     var change = Teams.GetOrganisationChanges(team, p, gameContext).Sum() / 10f;
            //
            //     team.Organisation = Mathf.Clamp(team.Organisation + change, 0, 100);
            // }
        }
    }
}
