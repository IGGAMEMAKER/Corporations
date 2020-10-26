using Assets.Core;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class InvestmentGoal
{
    public InvestorGoalType InvestorGoalType;

    public InvestmentGoal()
    {
        InvestorGoalType = InvestorGoalType.None;
    }

    public InvestmentGoal(InvestorGoalType goalType)
    {
        InvestorGoalType = goalType;
    }

    public abstract string GetFormattedName();
    public abstract List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext);

    public string GetFormattedRequirements(GameEntity company, GameContext gameContext)
    {
        return string.Join("\n", GetGoalRequirements(company, gameContext)
            .Select(g => Visuals.Colorize(g.description, IsRequirementMet(g, company, gameContext))));
    }

    //public abstract bool Redoable();
    //public abstract bool IsPickable(GameEntity company, GameContext gameContext);
    public bool IsCompleted(GameEntity company, GameContext gameContext)
    {
        var r = GetGoalRequirements(company, gameContext);

        foreach (var req in r)
        {
            if (!IsRequirementMet(req, company, gameContext))
                return false;
        }

        return true;
    }

    public bool IsRequirementMet(GoalRequirements req, GameEntity company, GameContext gameContext)
    {
        return req.have >= req.need;
    }
}

public class InvestmentGoalUnknown : InvestmentGoal
{
    public InvestmentGoalUnknown(InvestorGoalType investorGoalType) : base(investorGoalType)
    {

    }

    public override string GetFormattedName() => "Unknown: " + InvestorGoalType.ToString();

    public override List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext)
    {
        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = 0,
                need = 5,

                description = "Unknown requirements?"
            }
        };
    }
}

public class InvestmentGoalMakePrototype : InvestmentGoal
{
    public InvestmentGoalMakePrototype() : base(InvestorGoalType.ProductPrototype)
    {

    }

    public override string GetFormattedName() => "Create a prototype";

    public override List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext)
    {
        GameEntity product;

        if (company.hasProduct)
            product = company;
        else
            product = Companies.GetDaughterProducts(gameContext, company).First();

        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = (long)Marketing.GetSegmentLoyalty(product, Marketing.GetCoreAudienceId(company)),
                need = 5,

                description = "Loyalty > 5"
            }
        };
    }
}

public class InvestmentGoalMakeProductMarketFit : InvestmentGoal
{
    public InvestmentGoalMakeProductMarketFit() : base(InvestorGoalType.ProductBecomeMarketFit)
    {

    }

    public override string GetFormattedName() => "Make product market fit";

    public override List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext)
    {
        GameEntity product;

        if (company.hasProduct)
            product = company;
        else
            product = Companies.GetDaughterProducts(gameContext, company).First();

        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = (long)Marketing.GetSegmentLoyalty(product, Marketing.GetCoreAudienceId(company)),
                need = 10,

                description = "Loyalty > 10"
            }
        };
    }
}

public class InvestmentGoalRelease : InvestmentGoal
{
    public InvestmentGoalRelease() : base(InvestorGoalType.ProductRelease)
    {

    }

    public override string GetFormattedName() => "Release product";

    public override List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext)
    {
        GameEntity product;

        if (company.hasProduct)
            product = company;
        else
            product = Companies.GetDaughterProducts(gameContext, company).First();

        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = Products.GetServerCapacity(product),
                need = 1_500_000,

                description = "Servers capacity > 1.5M"
            },
            new GoalRequirements
            {
                have = product.team.Teams.Count,
                need = 2,

                description = "Has at least 3 teams"
            },
            new GoalRequirements
            {
                have = product.isRelease ? 1 : 0,
                need = 1,

                description = "PRESS THE BUTTON!"
            },
        };
    }
}

public class InvestmentGoalFirstUsers : InvestmentGoal
{
    public long TargetUsersAmount;

    public InvestmentGoalFirstUsers(long users) : base(InvestorGoalType.ProductFirstUsers)
    {
        TargetUsersAmount = users;
    }

    public override string GetFormattedName() => "Get first users";

    public override List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext)
    {
        GameEntity product;

        if (company.hasProduct)
            product = company;
        else
            product = Companies.GetDaughterProducts(gameContext, company).First();

        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = Marketing.GetUsers(product),
                need = TargetUsersAmount,

                description = "Users > " + TargetUsersAmount
            }
        };
    }
}

public class InvestmentGoalGrowAudience : InvestmentGoal
{
    public long TargetUsersAmount;

    public InvestmentGoalGrowAudience(long users) : base(InvestorGoalType.GrowUserBase)
    {
        TargetUsersAmount = users;
    }

    public override string GetFormattedName() => "Grow user base";

    public override List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext)
    {
        GameEntity product;

        if (company.hasProduct)
            product = company;
        else
            product = Companies.GetDaughterProducts(gameContext, company).First();

        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = Marketing.GetUsers(product),
                need = TargetUsersAmount,

                description = "Users > " + TargetUsersAmount
            }
        };
    }
}

public class InvestmentGoalBecomeProfitable : InvestmentGoal
{
    public long Income;
    public InvestmentGoalBecomeProfitable(long income) : base(InvestorGoalType.BecomeProfitable)
    {
        Income = income;
    }
    public override string GetFormattedName()
    {
        return "Become profitable";
    }

    public override List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext)
    {
        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = Economy.GetIncome(gameContext, company),
                need = Income,

                description = "Income > " + Format.MinifyMoney(Income)
            },

            new GoalRequirements
            {
                have = Economy.IsProfitable(gameContext, company) ? 1 : 0,
                need = 1,

                description = "Profitable"
            }
        };
    }
}
public class InvestmentGoalGrowProfit : InvestmentGoal
{
    public long Profit;

    public InvestmentGoalGrowProfit(long profit) : base(InvestorGoalType.GrowIncome)
    {
        Profit = profit;
    }

    public override string GetFormattedName() => "Grow profit";

    public override List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext)
    {
        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = Economy.GetIncome(gameContext, company),
                need = Profit,

                description = "Income > " + Format.MinifyMoney(Profit)
            },
        };
    }
}

public class InvestmentGoalGrowCost : InvestmentGoal
{
    public long Cost;

    public InvestmentGoalGrowCost(long cost) : base(InvestorGoalType.GrowCompanyCost)
    {
        Cost = cost;
    }

    public override string GetFormattedName() => "Grow company cost";

    public override List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext)
    {
        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = Economy.CostOf(company, gameContext),
                need = Cost,

                description = "Cost > " + Format.MinifyMoney(Cost)
            }
        };
    }
}

public class InvestmentGoalOutcompeteByIncome : InvestmentGoal
{
    public int CompanyId;
    public string CompetitorName;

    public InvestmentGoalOutcompeteByIncome(int id, string name) : base(InvestorGoalType.OutcompeteCompanyByIncome)
    {
        CompanyId = id;
        CompetitorName = name;
    }

    public override string GetFormattedName() => "Outcompete " + CompetitorName + " by income";

    public override List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext)
    {
        var income = Economy.GetIncome(gameContext, Companies.Get(gameContext, CompanyId));
        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = Economy.GetIncome(gameContext, company),
                need = income,

                description = "Your income > income of " + CompetitorName + $"({Format.MinifyMoney(income)})"
            }
        };
    }
}

public class InvestmentGoalOutcompeteByUsers : InvestmentGoal
{
    public int CompanyId;
    public string CompetitorName;

    public InvestmentGoalOutcompeteByUsers(int id, string name) : base(InvestorGoalType.OutcompeteCompanyByUsers)
    {
        CompanyId = id;
        CompetitorName = name;
    }

    public override string GetFormattedName() => "Outcompete " + CompetitorName + " by users";

    public override List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext)
    {
        var users = Marketing.GetUsers(Companies.Get(gameContext, CompanyId));

        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = Marketing.GetUsers(company),
                need = users,

                description = "Your audience > audience of " + CompetitorName + $"({Format.Minify(users)})"
            }
        };
    }
}

