// advices

// iterate
// foreach (InvestorGoal goal in (InvestorGoal[])Enum.GetValues(typeof(InvestorGoal)))

// cast
// https://stackoverflow.com/questions/29482/cast-int-to-enum-in-c-sharp
// var investorGoal = (InvestorGoal) Enum.Parse(typeof(InvestorGoal), arg0.ToString());

public enum NicheType
{
    None,

    SocialNetwork,
    Messenger,

    SearchEngine,

    CloudComputing,

    OSDesktop,
    //OSSciencePurpose,
}

public enum IndustryType
{
    Communications,
    Search,
    //OS,
    //CloudComputing
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

    // ads
    BrandingCampaign,
    TestCampaign,

    CompanyGoal,
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
    BecomeMarketFit,
    BecomeProfitable,
        //BecomeBestByTech, // 
        //GrowClientBase, // 

        //ProceedToNextRound, // 

    GrowCompanyCost, // +20%
    //GrowProfit, // +10%

    IPO
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
