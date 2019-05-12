// advices

// iterate
// foreach (InvestorGoal goal in (InvestorGoal[])Enum.GetValues(typeof(InvestorGoal)))

// cast
// var investorGoal = (InvestorGoal) Enum.Parse(typeof(InvestorGoal), arg0.ToString());

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

    // round d
    Strategic,

    // gives money and wants more investors to come (aka rounds) round a, b, c
    VentureInvestor,

    // wants dividends
    StockExchange,

    // seed
    Angel,

    // friends family fools - preseed
    FFF
}

public enum InvestorGoal
{
    BecomeMarketFit = 0,
    BecomeProfitable = 1,
        //BecomeBestByTech = 2, // 
        //GrowClientBase = 3, // 

        //ProceedToNextRound = 4, // 

    GrowCompanyCost = 5, // +20%
    GrowProfit = 6, // +10%

    IPO = 7
}

public enum InvestmentRound
{
    Preseed,
    Seed,
    A,
    B,
    C,
    D,
    E
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
