using Entitas;
using System.Collections.Generic;

class InvestmentRoundExecutionSystem : OnDateChange
{
    //private readonly Contexts contexts;

    public InvestmentRoundExecutionSystem(Contexts contexts) : base(contexts)
    {
        //this.contexts = contexts;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity[] CompaniesWhichAcceptInvestments = contexts.game.GetEntities(GameMatcher.AcceptsInvestments);

        foreach (var e in CompaniesWhichAcceptInvestments)
        {
            var left = e.acceptsInvestments.DaysLeft - 1;

            if (left > 0)
                e.ReplaceAcceptsInvestments(left);
            else
                e.RemoveAcceptsInvestments();
        }
    }
}