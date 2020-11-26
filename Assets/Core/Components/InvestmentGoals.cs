using Assets.Core;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct GoalRequirements
{
    public long have;
    public long need;

    public string description;
}

public abstract class InvestmentGoal
{
    public InvestorGoalType InvestorGoalType;

    public int ExecutorId;
    public int ControllerId;

    public InvestmentGoal SetExecutorAndController(int executor, int controller)
    {
        ExecutorId = executor;
        ControllerId = controller;

        return this;
    }

    public InvestmentGoal SetExecutorAndController(GameEntity executor, GameEntity controller)
    {
        return SetExecutorAndController(executor.company.Id, controller.company.Id);
    }

    public InvestmentGoal()
    {
        InvestorGoalType = InvestorGoalType.None;
    }

    public InvestmentGoal(InvestorGoalType goalType)
    {
        InvestorGoalType = goalType;
    }

    public bool NeedsReport => ExecutorId != ControllerId;

    public GameEntity GetExecutor(GameEntity company, GameContext gameContext)
    {
        return ExecutorId == company.company.Id ? company : Companies.Get(gameContext, ExecutorId);
    }

    public GameEntity GetController(GameEntity company, GameContext gameContext)
    {
        return ControllerId == company.company.Id ? company : Companies.Get(gameContext, ControllerId);
    }

    public GameEntity GetProduct(GameEntity company, GameContext gameContext)
    {
        return GetExecutor(company, gameContext);
    }


    public abstract string GetFormattedName();
    public abstract List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext);

    public string GetFormattedRequirements(GameEntity company1, GameContext gameContext)
    {
        var executor = GetExecutor(company1, gameContext);

        return "as " + executor.company.Name + "\n" + string.Join("\n", GetGoalRequirements(executor, gameContext)
            .Select(g => Visuals.Colorize(g.description, IsRequirementMet(g, executor, gameContext))));
    }

    //public abstract bool Redoable();
    //public abstract bool IsPickable(GameEntity company, GameContext gameContext);
    public bool IsCompleted(GameEntity company1, GameContext gameContext)
    {
        var company = GetExecutor(company1, gameContext);

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

    public int GetGoalProgress(GameEntity company, GameContext gameContext)
    {
        var progress = 0;

        var reqs = GetGoalRequirements(company, gameContext);

        //foreach (var r in reqs)
        //{
        //    progress += r.have * 100 / r.need;
        //}

        return 100;
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
        GameEntity product = GetProduct(company, gameContext);
        var loyalty = 13;

        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = (long)Marketing.GetSegmentLoyalty(product, Marketing.GetCoreAudienceId(company)),
                need = loyalty,

                description = $"Loyalty > {loyalty}"
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
        GameEntity product = GetProduct(company, gameContext);

        var loyalty = 15;

        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = (long)Marketing.GetSegmentLoyalty(product, Marketing.GetCoreAudienceId(company)),
                need = loyalty,

                description = $"Loyalty > {loyalty}"
            }
        };
    }
}

public class InvestmentGoalPrepareForRelease : InvestmentGoal
{
    public InvestmentGoalPrepareForRelease() : base(InvestorGoalType.ProductPrepareForRelease)
    {

    }

    public override string GetFormattedName() => "Prepare for release";

    public override List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext)
    {
        GameEntity product = GetProduct(company, gameContext);
        int teams = 3;
        long load = 1_500_000;

        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = Products.GetServerCapacity(product),
                need = load,

                description = $"Servers capacity > {Format.Minify(load)}"
            },
            new GoalRequirements
            {
                have = product.team.Teams.Count,
                need = teams,

                description = $"Has at least {teams} teams"
            },
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
        GameEntity product = GetProduct(company, gameContext);

        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = product.isRelease ? 1 : 0,
                need = 1,

                description = "PRESS RELEASE BUTTON!"
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
        GameEntity product = GetProduct(company, gameContext);

        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = Marketing.GetUsers(product),
                need = TargetUsersAmount,

                description = "Users > " + Format.Minify(TargetUsersAmount)
            }
        };
    }
}

public class InvestmentGoalMillionUsers : InvestmentGoal
{
    public long TargetUsersAmount;

    public InvestmentGoalMillionUsers(long users) : base(InvestorGoalType.ProductMillionUsers)
    {
        TargetUsersAmount = users;
    }

    public override string GetFormattedName() => "ROAD TO MILLION USERS";

    public override List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext)
    {
        GameEntity product = GetProduct(company, gameContext);

        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = Marketing.GetUsers(product),
                need = 1_000_000,

                description = "Users > " + Format.Minify(TargetUsersAmount)
            }
        };
    }
}

public class InvestmentGoalMoreSegments: InvestmentGoal
{
    public long Segments;

    public InvestmentGoalMoreSegments(long segments) : base(InvestorGoalType.GainMoreSegments)
    {
        Segments = segments;
    }

    public override string GetFormattedName() => "Get different audiences";

    public override List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext)
    {
        GameEntity product = GetProduct(company, gameContext);

        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = Marketing.GetAmountOfTargetAudiences(product),
                need = Segments,

                description = "Target audiences >= " + Format.Minify(Segments)
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
        GameEntity product = GetProduct(company, gameContext);

        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = Marketing.GetUsers(product),
                need = TargetUsersAmount,

                description = "Users > " + Format.Minify(TargetUsersAmount)
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

                description = "Income > " + Format.Money(Income)
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

                description = "Income > " + Format.Money(Profit)
            },
        };
    }
}

public class InvestmentGoalRegainLoyalty : InvestmentGoal
{
    public InvestmentGoalRegainLoyalty() : base(InvestorGoalType.ProductRegainLoyalty)
    {

    }

    public override string GetFormattedName()
    {
        return "Restore loyalty";
    }

    public override List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext)
    {
        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = Marketing.GetAudienceInfos().Any(a => Marketing.IsAudienceDisloyal(company, a.ID) && Marketing.IsTargetAudience(company, a.ID)) ? 0 : 1,
                need = 1
            }
        };
    }
}

public class InvestmentGoalStartMonetisation : InvestmentGoal
{
    public InvestmentGoalStartMonetisation() : base(InvestorGoalType.ProductStartMonetising)
    {
    }

    public override string GetFormattedName() => "Start monetisation";

    public override List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext)
    {
        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = Economy.GetIncome(gameContext, company),
                need = 1,

                description = "Income from product > 0"
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

                description = "Cost > " + Format.Money(Cost)
            }
        };
    }
}

public class InvestmentGoalOutcompeteByIncome : InvestmentGoal
{
    public int CompanyId;
    public string CompetitorName;

    public InvestmentGoalOutcompeteByIncome(GameEntity company) : base(InvestorGoalType.OutcompeteCompanyByIncome)
    {
        CompanyId = company.company.Id;
        CompetitorName = company.company.Name;
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

                description = "Your income > income of " + CompetitorName + $"({Format.Money(income)})"
            }
        };
    }
}


public class InvestmentGoalOutcompeteByUsers : InvestmentGoal
{
    public int CompanyId;
    public string CompetitorName;

    public InvestmentGoalOutcompeteByUsers(GameEntity company) : base(InvestorGoalType.OutcompeteCompanyByUsers)
    {
        CompanyId = company.company.Id;
        CompetitorName = company.company.Name;
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

public class InvestmentGoalOutcompeteByCost : InvestmentGoal
{
    public int CompanyId;
    public string CompetitorName;

    public InvestmentGoalOutcompeteByCost(GameEntity company) : base(InvestorGoalType.OutcompeteCompanyByCost)
    {
        CompanyId = company.company.Id;
        CompetitorName = company.company.Name;
    }

    public override string GetFormattedName() => "Outcompete " + CompetitorName + " by cost";

    public override List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext)
    {
        var cost = Economy.CostOf(Companies.Get(gameContext, CompanyId), gameContext);

        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = Economy.CostOf(company, gameContext),
                need = cost,

                description = "Your cost > cost of " + CompetitorName + $"({Format.Minify(cost)})"
            }
        };
    }
}

public class InvestmentGoalBuyBack : InvestmentGoal
{
    public InvestmentGoalBuyBack() : base(InvestorGoalType.BuyBack)
    {
    }

    public override string GetFormattedName()
    {
        return "Buy back all shares";
    }

    public override List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext)
    {
        bool hasSoloShareholder = company.shareholders.Shareholders.Count == 1;
        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = hasSoloShareholder ? 1 : 0,
                need = 1,

                description = "Buy all shares from your investors"
            }
        };
    }
}

public class InvestmentGoalDominateMarket : InvestmentGoal
{
    public NicheType TargetMarket;

    public InvestmentGoalDominateMarket(NicheType market) : base(InvestorGoalType.DominateMarket)
    {
        TargetMarket = market;
    }

    public override string GetFormattedName()
    {
        return "DOMINATE MARKET OF " + Enums.GetFormattedNicheName(TargetMarket).ToUpper();
    }

    public override List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext)
    {
        var companies = Markets.GetProductsOnMarket(gameContext, TargetMarket);

        var have = companies.Count(c => Companies.IsDaughterOf(company, c) && c.isAlive);
        var need = companies.Count(c => c.isAlive);

        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = have,
                need = need,

                description = "Own ALL companies in " + Enums.GetFormattedNicheName(TargetMarket) + $" ({have}/{need})"
            }
        };
    }
}

public class InvestmentGoalAcquireCompany : InvestmentGoal
{
    public int CompanyId;
    public string CompetitorName;

    public InvestmentGoalAcquireCompany(GameEntity company) : base(InvestorGoalType.AcquireCompany)
    {
        CompanyId = company.company.Id;
        CompetitorName = company.company.Name;
    }

    public override string GetFormattedName() => "Acquire " + CompetitorName + "!";

    public override List<GoalRequirements> GetGoalRequirements(GameEntity company, GameContext gameContext)
    {
        var target = Companies.Get(gameContext, CompanyId);

        var group = company.hasProduct ? Companies.GetManagingCompanyOf(company, gameContext) : company;

        bool companyWasBought = Companies.IsDaughterOf(group, target);
        bool companyBecomeBankrupt = !target.isAlive;

        return new List<GoalRequirements>
        {
            new GoalRequirements
            {
                have = (companyWasBought ? 1 : 0) + (companyBecomeBankrupt ? 1 : 0),
                need = 1,

                description = "Buy " + CompetitorName
            }
        };
    }
}
