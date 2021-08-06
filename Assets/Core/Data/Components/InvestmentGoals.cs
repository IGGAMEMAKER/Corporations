using Assets.Core;
using System.Collections.Generic;
using System.Linq;

public struct GoalRequirements
{
    public bool isLess;

    public long have;
    public long need;

    public string description;
}

public abstract class InvestmentGoal
{
    public InvestorGoalType InvestorGoalType;

    public int ExecutorId;
    public int ControllerId;

    public List<long> HaveInitially;

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

    public abstract string GetFormattedName();
}

public class InvestmentGoalUnknown : InvestmentGoal
{
    public InvestmentGoalUnknown(InvestorGoalType investorGoalType) : base(investorGoalType) { }

    public override string GetFormattedName() => "Unknown: " + InvestorGoalType.ToString();
}

public class InvestmentGoalMakePrototype : InvestmentGoal
{
    public InvestmentGoalMakePrototype() : base(InvestorGoalType.ProductPrototype) { }

    public override string GetFormattedName() => "Create a prototype";
}

public class InvestmentGoalMakeProductMarketFit : InvestmentGoal
{
    public InvestmentGoalMakeProductMarketFit() : base(InvestorGoalType.ProductBecomeMarketFit) { }

    public override string GetFormattedName() => "Make product market fit";
}

public class InvestmentGoalPrepareForRelease : InvestmentGoal
{
    public InvestmentGoalPrepareForRelease() : base(InvestorGoalType.ProductPrepareForRelease) { }

    public override string GetFormattedName() => "Prepare for release";
}


public class InvestmentGoalRelease : InvestmentGoal
{
    public InvestmentGoalRelease() : base(InvestorGoalType.ProductRelease) { }

    public override string GetFormattedName() => "Release product";
}

public class InvestmentGoalFirstUsers : InvestmentGoal
{
    public long TargetUsersAmount;

    public InvestmentGoalFirstUsers(long users) : base(InvestorGoalType.ProductFirstUsers)
    {
        TargetUsersAmount = users;
    }

    public override string GetFormattedName() => "Get first users";
}

public class InvestmentGoalMillionUsers : InvestmentGoal
{
    public long TargetUsersAmount;

    public InvestmentGoalMillionUsers(long users) : base(InvestorGoalType.ProductMillionUsers)
    {
        TargetUsersAmount = users;
    }

    public override string GetFormattedName() => "ROAD TO MILLION USERS";
}

public class InvestmentGoalMoreSegments: InvestmentGoal
{
    public long Segments;

    public InvestmentGoalMoreSegments(long segments) : base(InvestorGoalType.GainMoreSegments)
    {
        Segments = segments;
    }

    public override string GetFormattedName() => "Get different audiences";
}

public class InvestmentGoalGrowAudience : InvestmentGoal
{
    public long TargetUsersAmount;

    public InvestmentGoalGrowAudience(long users) : base(InvestorGoalType.GrowUserBase)
    {
        TargetUsersAmount = users;
    }

    public override string GetFormattedName() => "Grow user base";
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
}

public class InvestmentGoalGrowProfit : InvestmentGoal
{
    public long Profit;

    public InvestmentGoalGrowProfit(long profit) : base(InvestorGoalType.GrowIncome)
    {
        Profit = profit;
    }

    public override string GetFormattedName() => "Grow profit";
}

public class InvestmentGoalRegainLoyalty : InvestmentGoal
{
    public InvestmentGoalRegainLoyalty() : base(InvestorGoalType.ProductRegainLoyalty) { }

    public override string GetFormattedName()
    {
        return "Restore loyalty";
    }
}

public class InvestmentGoalStartMonetisation : InvestmentGoal
{
    public InvestmentGoalStartMonetisation() : base(InvestorGoalType.ProductStartMonetising) { }

    public override string GetFormattedName() => "Start monetisation";
}

public class InvestmentGoalGrowCost : InvestmentGoal
{
    public long Cost;

    public InvestmentGoalGrowCost(long cost) : base(InvestorGoalType.GrowCompanyCost)
    {
        Cost = cost;
    }

    public override string GetFormattedName() => "Grow company cost";
}

public class InvestmentGoalOutcompeteByIncome : InvestmentGoal
{
    public int CompanyId;
    public string CompetitorName;

    public InvestmentGoalOutcompeteByIncome(int CompanyId, string CompetitorName) : base(InvestorGoalType.OutcompeteCompanyByIncome)
    {
        this.CompanyId = CompanyId;
        this.CompetitorName = CompetitorName;
    }

    public override string GetFormattedName() => "Outcompete " + CompetitorName; // + " by income";
}


public class InvestmentGoalOutcompeteByUsers : InvestmentGoal
{
    public int CompanyId;
    public string CompetitorName;

    public InvestmentGoalOutcompeteByUsers(int CompanyId, string CompetitorName) : base(InvestorGoalType.OutcompeteCompanyByUsers)
    {
        this.CompanyId = CompanyId;
        this.CompetitorName = CompetitorName;
    }

    public override string GetFormattedName() => "Outcompete " + CompetitorName; // + " by users";
}

public class InvestmentGoalOutcompeteByCost : InvestmentGoal
{
    public int CompanyId;
    public string CompetitorName;

    public InvestmentGoalOutcompeteByCost(int CompanyId, string CompetitorName) : base(InvestorGoalType.OutcompeteCompanyByCost)
    {
        this.CompanyId = CompanyId;
        this.CompetitorName = CompetitorName;
    }

    public override string GetFormattedName() => "Outcompete " + CompetitorName; // + " by cost";
}

public class InvestmentGoalBuyBack : InvestmentGoal
{
    public InvestmentGoalBuyBack() : base(InvestorGoalType.BuyBack) { }

    public override string GetFormattedName()
    {
        return "Buy back all shares";
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
        return "DOMINATE MARKET";
    }
}

public class InvestmentGoalAcquireCompany : InvestmentGoal
{
    public int CompanyId;
    public string CompetitorName;

    public InvestmentGoalAcquireCompany(int CompanyId, string CompetitorName) : base(InvestorGoalType.AcquireCompany)
    {
        this.CompanyId = CompanyId;
        this.CompetitorName = CompetitorName;
    }

    public override string GetFormattedName() => "Acquire " + CompetitorName + "!";
}
