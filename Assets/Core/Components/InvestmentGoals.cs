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
    public string GetFormattedRequirements(GameEntity company, GameContext gameContext)
    {
        return string.Join("\n", GetGoalRequirements(company, gameContext).Select(g => g.description));
    }

    //public abstract bool Redoable();
    //public abstract bool IsPickable(GameEntity company, GameContext gameContext);
    public bool IsCompleted(GameEntity company, GameContext gameContext)
    {
        var r = GetGoalRequirements(company, gameContext);

        foreach (var req in r)
        {
            if (req.have < req.need)
                return false;
        }

        return true;
    }

    public bool Did(GameEntity company, InvestorGoalType investorGoalType)
    {
        return company.completedGoals.Goals.Contains(investorGoalType);
    }

    public abstract List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext);

    //public static List<GoalRequirements> Wrap(List<GoalRequirements> requirements) => requirements;
    public static List<GoalRequirements> Wrap(long need, long have) => Wrap(new GoalRequirements { have = have, need = need });
    public static List<GoalRequirements> Wrap(GoalRequirements requirements)
    {
        return new List<GoalRequirements> { requirements };
    }

    // --------------- auxillary functions ------------------


}

public class InvestmentGoalUnknown : InvestmentGoal
{
    public InvestmentGoalUnknown(InvestorGoalType investorGoalType) : base(investorGoalType)
    {

    }

    public override string GetFormattedName() => "Unknown: " + InvestorGoalType.ToString();

    public override List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext)
    {
        var Loyalty = (long)Marketing.GetSegmentLoyalty(company, Marketing.GetCoreAudienceId(company));
        return Wrap(new GoalRequirements
        {
            have = 0,
            need = 5,

            description = "Unknown requirements?"
        });
    }
}

public class InvestmentGoalMakePrototype : InvestmentGoal
{
    public InvestmentGoalMakePrototype() : base(InvestorGoalType.ProductPrototype) { }

    public override string GetFormattedName() => "Create a prototype";

    public override List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext)
    {
        return Wrap(new GoalRequirements
        {
            have = (long)Marketing.GetSegmentLoyalty(company, Marketing.GetCoreAudienceId(company)),
            need = 5,

            description = "Loyalty > 5"
        });
    }
}

public class InvestmentGoalMakeProductMarketFit : InvestmentGoal
{
    public InvestmentGoalMakeProductMarketFit() : base(InvestorGoalType.ProductBecomeMarketFit) { }

    public override string GetFormattedName()
    {
        return "Make product market fit";
    }

    public override List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext)
    {
        return Wrap(new GoalRequirements
        {
            have = (long)Marketing.GetSegmentLoyalty(company, Marketing.GetCoreAudienceId(company)),
            need = 10,

            description = "Loyalty > 10"
        });
    }
}

public class InvestmentGoalRelease : InvestmentGoal
{
    public InvestmentGoalRelease() : base(InvestorGoalType.ProductRelease) { }

    public override string GetFormattedName() => "Release product";

    public override List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext)
    {
        return Wrap(new GoalRequirements
        {
            have = company.isRelease ? 1 : 0,
            need = 1,

            description = "PRESS THE BUTTON!"
        });
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
        return Wrap(new GoalRequirements
        {
            have = Marketing.GetUsers(company),
            need = TargetUsersAmount,

            description = "Users > " + TargetUsersAmount
        });
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
        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = Marketing.GetUsers(company),
                need = TargetUsersAmount,

                description = "Users > " + TargetUsersAmount
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

    public override string GetFormattedName()
    {
        return "Grow profit";
    }

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

            //new GoalRequirements
            //{
            //    have = Economy.GetIncome(gameContext, company),
            //    need = 
            //}
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

    public override string GetFormattedName()
    {
        return "Grow company cost";
    }

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

    public override string GetFormattedName()
    {
        return "Outcompete " + CompetitorName + " by income";
    }

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

    public override string GetFormattedName()
    {
        return "Outcompete " + CompetitorName + " by users";
    }

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

