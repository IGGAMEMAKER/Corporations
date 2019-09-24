// advices

// iterate
// foreach (InvestorGoal goal in (InvestorGoal[])Enum.GetValues(typeof(InvestorGoal)))

// cast
// https://stackoverflow.com/questions/29482/cast-int-to-enum-in-c-sharp
// var investorGoal = (InvestorGoal) Enum.Parse(typeof(InvestorGoal), arg0.ToString());

// cast 2
// https://stackoverflow.com/questions/3260762/can-i-cast-from-a-generic-type-to-an-enum-in-c



public enum NicheType
{
    //None,

    SocialNetwork,
    Messenger,
    //Dating,
    Blogs,
    Forums,
    Email,


    SearchEngine,
    CloudComputing,
    OSDesktop,
    Browser,
    //OSSciencePurpose,

    // Gaming and gambling
    GamingPoker,
    GamingCasino,
    GamingBetting,
    GamingSlots,
    GamingTotalisator,


    Ecommerce,
}

//public enum NicheType
//{
//    //None,

    //    SocialNetwork,
    //    Messenger,
    //    //Dating,
    //    Blogs,
    //    Forums,
    //    Email,


    //    SearchEngine,
    //    CloudComputing,
    //    OSDesktop,
    //    Browser,
    //    //OSSciencePurpose,

    //    // Gaming and gambling
    //    GamingPoker,
    //    GamingCasino,
    //    GamingBetting,


    //    Ecommerce,


    //    SocialNetwork1,
    //    Messenger1,
    //    //Dating,
    //    Blog1s,
    //    Forums1,
    //    Emai1l,


    //    SearchEngine1,
    //    CloudComputing1,
    //    OSDesktop1,
    //    Browser1,

    //    GamingPoker1,
    //    GamingCasino1,
    //    GamingBetting1,


    //    Ecommerce1,


    //    SocialNetwork12,
    //    Messenger12,
    //    //Dating,
    //    Blog12s,
    //    Forums12,
    //    Emai1l2,


    //    SearchEngine12,
    //    CloudComputing12,
    //    OSDesktop12,
    //    Browser12,

    //    GamingPoker12,
    //    GamingCasino12,
    //    GamingBetting12,


    //    Ecommerce12,


    //    SocialNetwork123,
    //    Messenger123,
    //    //Dating,
    //    Blog123s,
    //    Forums123,
    //    Emai1l23,


    //    SearchEngine123,
    //    CloudComputing123,
    //    OSDesktop123,
    //    Browser123,

    //    GamingPoker123,
    //    GamingCasino123,
    //    GamingBetting123,


    //    Ecommerce123,


    //    SocialNetwork1234,
    //    Messenger1234,
    //    //Dating,
    //    Blog1234s,
    //    Forums1234,
    //    Emai1l234,


    //    SearchEngine1234,
    //    CloudComputing1234,
    //    OSDesktop1234,
    //    Browser1234,

    //    GamingPoker1234,
    //    GamingCasino1234,
    //    GamingBetting1234,


    //    Ecommerce1234,


    //    SocialNetwork12345,
    //    Messenger12345,
    //    //Dating,
    //    Blog12345s,
    //    Forums12345,
    //    Emai1l2345,


    //    SearchEngine12345,
    //    CloudComputing1235,
    //    OSDesktop1235,
    //    Browser1235,

    //    GamingPoker1235,
    //    GamingCasino1235,
    //    GamingBetting1235,


    //    Ecommerce1235,


    //    SocialNetwork12346,
    //    Messenger12346,
    //    //Dating,
    //    Blog12346s,
    //    Forums12346,
    //    Emai1l2346,


    //    SearchEngine12346,
    //    CloudComputing12346,
    //    OSDesktop12346,
    //    Browser12346,

    //    GamingPoker12346,
    //    GamingCasino12346,
    //    GamingBetting12346,


    //    Ecommerce12346,
    //}

public enum IndustryType
{
    Communications,
    Fundamental,
    Entertainment,
    Ecommerce
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

    // TODO REMOVE
    StealIdeas,

    ImproveSegment,

    //
    MarketResearch
}

public class Cooldown
{
    public CooldownType CooldownType;
    public int EndDate;

    public bool Compare (CooldownType cooldownType)
    {
        return Compare(new Cooldown { CooldownType = cooldownType });
    }

    public bool Compare (Cooldown cooldown)
    {
        if (CooldownType != cooldown.CooldownType)
            return false;

        return IsEqual(cooldown);
    }

    public virtual string GetKey()
    {
        return $"StandardCooldown-expires-{EndDate}";
    }

    public virtual bool IsEqual(Cooldown comparable)
    {
        return true;
    }
}

public class CooldownImproveConcept : Cooldown
{
    public int companyId;

    public CooldownImproveConcept(int companyId)
    {
        this.companyId = companyId;
        CooldownType = CooldownType.ImproveSegment;
    }

    public override string GetKey()
    {
        return $"Upgrade-{companyId}";
    }

    public override bool IsEqual(Cooldown comparable)
    {
        return (comparable as CooldownImproveConcept).companyId == companyId;
    }
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
    Prototype,
    FirstUsers,

    BecomeMarketFit,
    Release,
    BecomeProfitable,
        //BecomeBestByTech, // 
        //GrowClientBase, // 

        //ProceedToNextRound, // 

    GrowCompanyCost, // +20%
    //GrowProfit, // +10%

    IPO,
    
    // products only
    Operationing
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
    Zero,
    Low,
    Medium,
    High
}
