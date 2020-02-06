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

    Com_SocialNetwork,
    Com_Messenger,
    Com_Dating,
    Com_Blogs,
    Com_Forums,
    Com_Email,


    Tech_SearchEngine,
    Tech_Clouds,
    Tech_OSDesktop,
    Tech_Browser,
    Tech_MobileOS,
    // Databases

    // Entertainment
    // gambling
    Ent_Lottery,
    Ent_Casino, // slots, totalisator
    Ent_Betting,
    Ent_Poker,

    // games
    Ent_MMOs,
    Ent_FreeToPlay,
    Ent_SinglePlayer,
    //Ent_Publishing,

    // streaming
    Ent_StreamingService, // Twitch
    Ent_TVStreamingService, // Netflix like
    Ent_VideoHosting, // Youtube

    // Ecommerce
    /// Finances
    ECom_PaymentSystem,
    ECom_OnlineBanking,
    ECom_Exchanging,
    ECom_Blockchain,
    ECom_TradingBot,

    // mixed with offline
    ECom_OnlineTaxi,
    ECom_Marketplace, // amazon, ali e.t.c.
    
    // transport/tourism
    ECom_BookingTransportTickets, // rzd, aero
    ECom_BookingHotels, // hotels
    ECom_BookingTours,
    ECom_BookingAppartments, // airbnb


    // Useful apps
    Qol_PdfReader, // pdf readers
    Qol_DocumentEditors, // micr office
    Qol_GraphicalEditor, // Photoshop
    Qol_3DGraphicalEditor, // 3d max
    Qol_AdBlocker, 

    Qol_DiskFormattingUtils, // small
    Qol_RSSReader, // small
    Qol_Archivers, // small-average
    Qol_MusicPlayers, // small-average
    Qol_VideoPlayers, // average
    Qol_VideoEditingTool, // average
    Qol_MusicSearch, // average shazam

    Qol_Encyclopedia,
    Qol_Antivirus,

    Qol_OnlineEducation,
    Qol_Maps
}

public enum IndustryType
{
    Communications,
    Technology,
    Entertainment,
    Ecommerce,
        Finances,
        Tourism,
    WorkAndLife
}

public enum CompanyType
{
    ProductCompany,

    FinancialGroup,

    Group,
    Holding,
    Corporation
}


//public enum CooldownType
//{
//    ImproveSegment,

//    CorporateCulture,
//}

//public class Cooldown
//{
//    public CooldownType CooldownType;
//    public int EndDate;

//    public bool Compare (CooldownType cooldownType)
//    {
//        return Compare(new Cooldown { CooldownType = cooldownType });
//    }

//    public bool Compare (Cooldown cooldown)
//    {
//        if (CooldownType != cooldown.CooldownType)
//            return false;

//        return IsEqual(cooldown);
//    }

//    public virtual string GetKey()
//    {
//        return $"StandardCooldown-expires-{EndDate}";
//    }

//    public virtual bool IsEqual(Cooldown comparable)
//    {
//        return true;
//    }
//}

//public class CooldownImproveConcept : Cooldown
//{
//    public int companyId;

//    public CooldownImproveConcept(int companyId)
//    {
//        this.companyId = companyId;
//        CooldownType = CooldownType.ImproveSegment;
//    }

//    public override string GetKey()
//    {
//        return $"Upgrade-{companyId}";
//    }

//    public override bool IsEqual(Cooldown comparable)
//    {
//        return (comparable as CooldownImproveConcept).companyId == companyId;
//    }
//}

//public class CooldownUpgradeCorporateCulture : Cooldown
//{
//    public int companyId;

//    public CooldownUpgradeCorporateCulture(int companyId)
//    {
//        this.companyId = companyId;
//        CooldownType = CooldownType.CorporateCulture;
//    }

//    public override string GetKey()
//    {
//        return $"Culture-{companyId}";
//    }

//    public override bool IsEqual(Cooldown comparable)
//    {
//        return (comparable as CooldownUpgradeCorporateCulture).companyId == companyId;
//    }
//}




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
