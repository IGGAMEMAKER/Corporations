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
            var value = Companies.GetPolicyValue(c, CorporatePolicy.PeopleOrProcesses);
            var gain = 1;

            var center = 5;

            if (value > center)
                gain = 3;

            if (value == center)
                gain = 2;

            foreach (var t in c.team.Teams)
            {
                var maxSize = Teams.GetMaxTeamSize(t);
                if (t.Workers < maxSize)
                {
                    bool isUniversalTeam = Teams.IsUniversalTeam(t.TeamType);

                    var hiringSpeed = isUniversalTeam ? 25 : 15;
                    t.HiringProgress += hiringSpeed;

                    if (t.HiringProgress >= 100)
                    {
                        t.HiringProgress = 0;
                        t.Workers += gain;

                        if (t.Workers > maxSize)
                            t.Workers = maxSize;
                    }
                }
            }
        }
    }
}
