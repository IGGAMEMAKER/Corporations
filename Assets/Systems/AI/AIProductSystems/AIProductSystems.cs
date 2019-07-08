using Assets.Utils;
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
    public AIProductSystems(Contexts contexts) : base(contexts) {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in CompanyUtils.GetAIProducts(gameContext))
        {
            var goal = ChooseGoal(e);

            ExecuteGoal(goal, e);
        }
    }

    void ExecuteGoal(ProductCompanyGoals goal, GameEntity product)
    {
        switch (goal)
        {
            case ProductCompanyGoals.Survive:
                Survive(product);
                break;

            case ProductCompanyGoals.FixClientLoyalty:
                FixLoyalty(product);
                break;

            default:
                CompleteCompanyGoal(product);
                break;
        }
    }

    ProductCompanyGoals ChooseGoal(GameEntity product)
    {
        var goals = new Dictionary<ProductCompanyGoals, long>
        {
            // threats
            [ProductCompanyGoals.Survive] = GetBankruptcyUrgency(product),

            // company goal
            [ProductCompanyGoals.CompleteCompanyGoal] = GetCompanyGoalUrgency(product),
        };
        
        return PickMostImportantValue(goals);
    }
}