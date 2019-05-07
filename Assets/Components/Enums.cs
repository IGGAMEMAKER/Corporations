
public enum NicheType
{
    None,

    SocialNetwork,
    Messenger,

    SearchEngine,

    CloudComputing,

    OSCommonPurpose,
    OSSciencePurpose,
}

public enum IndustryType
{
    Communications,
    Search,
    OS,
    CloudComputing
}

public enum CompanyType
{
    ProductCompany,

    FinancialGroup,

    Group,
    Holding,
    Corporation
}

public enum CooldownType
{
    TargetingActivity,
    
    // fame or loyalty
    MarketingFocus,

    // loyalty, ideas, 
    ProductFocus,
}

public struct Cooldown
{
    public int EndDate;
}


public enum InvestorType
{
    // both value long term solutions
    // aka team effeciency
    // product quality
    // branding
    // Founder - human with strategic goals, Strategic - company with strategic goals
    Founder,
    Strategic,

    // gives less money and wants more investors to come (aka rounds)
    VentureInvestor,

    // wants dividends
    SmallInvestor
}

public enum InvestorGoal
{
    BecomeMarketFit,
    BecomeBestByTech,

    ProceedToNextRound,

    GrowCompanyCost,
    GrowProfit,
    GrowClientBase,

    //ImproveManagement,
    //ImproveBranding
}

public enum InvestmentRound
{
    Preseed,
    Seed,
    A,
    B,
    C,
    D,
    E,
    F,
    G,
    H,
    I,
    J
}


public enum UserType
{
    Newbie,
    Regular,
    Core
}

public enum Pricing
{
    Free = 0,
    Low = 100,
    Medium = 170,
    High = 200
}

public enum MarketingFinancing
{
    Low,
    Medium,
    High
}