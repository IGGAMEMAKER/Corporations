using Assets.Utils;
using Entitas;
using System.Collections.Generic;
using System.Linq;

public enum ProductCompanyGoals
{
    Survive,
    FixClientLoyalty,
    CompleteCompanyGoal,
    Develop,
    TakeTechLeadership
}

public class AIProductSystems : OnDateChange
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

    long GetBankruptcyScoring(GameEntity product)
    {
        var change = CompanyEconomyUtils.GetBalanceChange(gameContext, product.company.Id);
        var balance = product.companyResource.Resources.money;

        if (change >= 0)
            return 0;

        var timeToBankruptcy = balance / (-change);

        return Constants.COMPANY_SCORING_BANKRUPTCY / (1 + timeToBankruptcy * timeToBankruptcy);
    }

    long GetBadLoyaltyScoring(GameEntity product)
    {
        var coreLoyalty = MarketingUtils.GetClientLoyalty(gameContext, product.company.Id, UserType.Core);
        var regularsLoyalty = MarketingUtils.GetClientLoyalty(gameContext, product.company.Id, UserType.Regular);

        return (coreLoyalty <= 0 || regularsLoyalty <= 0) ? Constants.COMPANY_SCORING_LOYALTY : 0;
    }

    long GetCompanyGoalScoring(GameEntity product)
    {
        //var requirements = InvestmentUtils.GetGoalRequirements(product, gameContext);

        var goalCompleted = InvestmentUtils.IsGoalCompleted(product, gameContext);

        return goalCompleted ? 0 : Constants.COMPANY_SCORING_COMPANY_GOAL;
    }

    long GetDevelopmentScoring(GameEntity product)
    {
        return 100;
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

        ExecuteGoal(goal, product);
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


    void CompleteCompanyGoal(GameEntity product)
    {

    }

    void Survive(GameEntity product)
    {
        // we cannot earn money fast, we need to reduce company maintenance!

        // shrink team
        // increase prices
        // crunch
    }

    void FixLoyalty(GameEntity product)
    {
        // decrease prices
        // improve segments
        // match market requirements
        //  // focus on ideas?
    }

    void Develop(GameEntity product)
    {
        // ---- Team ----
        // stop crunches                            cooldown
        // hire someone                             money, mp
        // upgrade team                             mp
        
        
        // ---- Product ----
        // improve segments                         pp, ip
        // monetisation                             ip
        // increase prices if possible              -cooldown
        // steal ideas                              cooldown
        
        // ---- Marketing ----
        // grab clients                             sp, money
        // increase marketing budget if possible

        
        // ---- Business ----
        // start round                              ????
        // accept investments
        // flip goal                                cooldown
    }

    void TakeLeadership(GameEntity product)
    {
        // focus on ideas
        // crunch
    }
}
