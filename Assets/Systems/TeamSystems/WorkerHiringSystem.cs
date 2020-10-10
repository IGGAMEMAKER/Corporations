using Assets.Core;
using Entitas;
using System.Collections.Generic;

class WorkerHiringSystem : OnDateChange
{
    public WorkerHiringSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities) {
        // hiring workers

        var companies = contexts.game.GetEntities(GameMatcher.AllOf(GameMatcher.Alive, GameMatcher.Company));

        foreach (var c in companies)
        {
            foreach (var t in c.team.Teams)
            {
                if (t.Workers < 8)
                {
                    bool isUniversalTeam = Teams.IsUniversalTeam(t.TeamType);

                    var hiringSpeed = isUniversalTeam ? 25 : 15;
                    t.HiringProgress += hiringSpeed;

                    if (t.HiringProgress >= 100)
                    {
                        t.HiringProgress = 0;
                        t.Workers++;
                    }
                }
            }
        }
    }
}
