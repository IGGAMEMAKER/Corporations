using Entitas;
using System.Collections.Generic;


[Game]
public class IndustryComponent : IComponent
{
    public IndustryType IndustryType;
}


public struct MarketCompatibility
{
    public NicheType NicheType;
    public int Compatibility;
}

public class SpecificProductFeature
{
    public string Name;
}

[Game]
public class NicheComponent : IComponent
{
    public NicheType NicheType;
    public IndustryType IndustryType;

    // cross promotions
    public List<MarketCompatibility> MarketCompatibilities;
    public List<NicheType> CompetingNiches;

    public NicheType Parent;
}

public class NicheBaseProfileComponent : IComponent
{
    public MarketProfile Profile;
}

public class ChannelInfo
{
    public int ID;
    public long Audience;
    public long Batch;

    // int - companyId, long - amount of gained clients
    public Dictionary<int, long> Companies;

    // cost per user
    public float costPerAd;
    public float relativeCost;

    public float costInWorkers;
}

public enum ClientContainerType
{
    ProductCompany,

    Forum, // per industry ... per market??
    SocialNetwork, // general market
    Messenger, // general & focused

    // news channel
    IndustrialMedia,
    NicheMedia, // most focused
    EntertainingMedia,
}

public class MarketingChannelComponent : IComponent
{
    public long Clients;
    public ClientContainerType ContainerType;
    public ChannelInfo ChannelInfo;
}

public class SourceOfClientsComponent : IComponent
{
    // int - marketing channel ID, long - amount of clients, that company gained through this channel
    public Dictionary<int, long> Channels;
}

public class ChannelMarketingActivitiesComponent : IComponent
{
    // int - companyID, long - true / false
    public Dictionary<int, long> Companies;
}

[Game]
public class ChannelExplorationComponent : IComponent
{
    // int - channelId, int - progress in ?days / seconds
    public Dictionary<int, int> InProgress;
    public List<int> Explored;


    public int AmountOfExploredChannels;
}

public class CompanyMarketingActivitiesComponent : IComponent
{
    // int - companyID, long - true / false
    public Dictionary<int, long> Channels;
}



public enum NicheSpeed
{
    Quarter = 3,
    HalfYear = 6,
    Year = 12,
    ThreeYears = 36
}

public enum AudienceSize
{
    BigEnterprise = 2000, // 2K
    SmallEnterprise = 50000, // 50K
    SmallUtil = SmallEnterprise,
    Million = 1000000, // 1M
    Million100 = 100000000, // 100M // usefull util AdBlock
    Global = 1000000000 // 1-2B
}

public enum Monetisation
{
    Adverts = 1,
    Service = 10,
    IrregularPaid = 25,
    Paid = 50, // (max income when making ads) + small additional payments
    Enterprise = 1000,
}

public enum Margin
{
    Low = 5,
    Mid = 10,
    High = 20
}

public enum AppComplexity
{
    Solo = 1,
    Easy = 3,
    Average = 6,
    Hard = 10,
    Humongous = 12
}

[Game]
public class ProductPositioningComponent : IComponent
{
    public int Positioning;
}

public struct ProductPositioning
{
    public string name;
    public int marketShare;
    public bool isCompetitive;

    public float priceModifier;
}

[Game]
public class NicheSegmentsComponent : IComponent
{
    // int - positioning id
    public Dictionary<int, ProductPositioning> Positionings;
}

[Game]
public class NicheClientsContainerComponent : IComponent
{
    public Dictionary<int, long> Clients;
}


[Game]
public class NicheCostsComponent : IComponent
{
    public float BaseIncome;
    public long Audience;

    public int TechCost;
    public float AcquisitionCost;
}

public enum MarketState
{
    Idle,
    Innovation,
    Trending,
    MassGrowth,
    MassUsage,
    Decay,
    Death
}



[Game]
public class NicheLifecycleComponent : IComponent
{
    public int OpenDate;
    public Dictionary<MarketState, int> Growth;
    public int EndDate;
}

[Game]
public class NicheStateComponent : IComponent
{
    public MarketState Phase;
    public int Duration;
}

[Game]
public class DeadComponent : IComponent { }


    //// when someone innovates, increment this
//[Game, Event(EventTarget.Self), Event(EventTarget.Any)]
public class SegmentComponent : IComponent
{
    // int - level
    public int Level;
}