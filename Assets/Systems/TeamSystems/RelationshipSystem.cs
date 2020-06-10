using Assets.Core;
using Entitas;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class RelationshipSystem : OnPeriodChange
{
    public RelationshipSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        var relationshipContainers = contexts.game.GetEntities(GameMatcher.PersonalRelationships)
            .Where(h => h.worker.companyId != -1 && h.worker.WorkerRole == WorkerRole.CEO);

        foreach (var h in relationshipContainers)
        {
            var company = Companies.Get(gameContext, h.worker.companyId);
            var team = company.team.Managers.Keys;

            foreach (var humanId in team)
            {
                if (h.personalRelationships.Relations.ContainsKey(humanId))
                {
                    var relation = h.personalRelationships.Relations[humanId];

                    h.personalRelationships.Relations[humanId] = Mathf.Clamp(relation + 2, 0, 100);
                }
                else
                {
                    h.personalRelationships.Relations[humanId] = 5;
                }
            }
        }
    }
}
