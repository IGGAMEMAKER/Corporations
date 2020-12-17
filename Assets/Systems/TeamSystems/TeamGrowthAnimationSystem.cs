using Entitas;
using System.Collections.Generic;

class TeamGrowthAnimationSystem : OnDateChange
{
    public TeamGrowthAnimationSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        var upgradedWorkers = contexts.game.GetEntities(GameMatcher.HumanUpgradedSkills);

        for (var i = 0; i < upgradedWorkers.Length; i++)
        {
            var w = upgradedWorkers[i];

            if (w.humanUpgradedSkills.DaysSinceUpgrade <= 0)
                w.RemoveHumanUpgradedSkills();
            else
                w.ReplaceHumanUpgradedSkills(w.humanUpgradedSkills.DaysSinceUpgrade - 1);
        }
    }
}
