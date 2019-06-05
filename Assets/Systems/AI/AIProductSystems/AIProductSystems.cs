using Entitas;
using System.Collections.Generic;

public enum ProductCompanyGoals
{
    Survive,
    FixClientLoyalty,
    CompleteCompanyGoal,
    Develop,
    TakeTechLeadership
}

public partial class AIProductSystems : OnDateChange
{
    public AIProductSystems(Contexts contexts) : base(contexts) {}

    GameEntity[] GetAIProducts()
    {
        return gameContext.GetEntities(
            GameMatcher
            .AllOf(GameMatcher.Product)
            .NoneOf(GameMatcher.ControlledByPlayer)
        );
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in GetAIProducts())
            ChooseGoal(e);
    }

    void ChooseGoal(GameEntity product)
    {
        var goals = new Dictionary<ProductCompanyGoals, long>
        {
            // threats
            [ProductCompanyGoals.Survive] = GetBankruptcyScoring(product),
            [ProductCompanyGoals.FixClientLoyalty] = GetBadLoyaltyScoring(product),

            // company goal
            [ProductCompanyGoals.CompleteCompanyGoal] = GetCompanyGoalScoring(product),

            // development
            [ProductCompanyGoals.Develop] = GetDevelopmentScoring(product),
            [ProductCompanyGoals.TakeTechLeadership] = GetDevelopmentScoring(product),
        };

        var goal = PickUrgentGoal(goals);

        ExecuteGoal(goal, product);
    }

    ProductCompanyGoals PickUrgentGoal(Dictionary<ProductCompanyGoals, long> goals)
    {
        long value = 0;
        ProductCompanyGoals goal = ProductCompanyGoals.Survive;

        foreach (var pair in goals)
        {
            if (pair.Value > value)
            {
                value = pair.Value;
                goal = pair.Key;
            }
        }

        return goal;
    }

    void ExecuteGoal(ProductCompanyGoals goal, GameEntity product)
    {
        switch (goal)
        {
            case ProductCompanyGoals.Survive: Survive(product); break;
            case ProductCompanyGoals.FixClientLoyalty: FixLoyalty(product); break;
            case ProductCompanyGoals.CompleteCompanyGoal: CompleteCompanyGoal(product); break;
            case ProductCompanyGoals.TakeTechLeadership: TakeLeadership(product); break;

            default: Develop(product); break;
        }
    }
}
