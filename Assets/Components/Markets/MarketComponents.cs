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

[Game]
public class AggressiveMarketingComponent: IComponent {}

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

public enum NicheLifecyclePhase
{
    Idle,
    Innovation,
    Trending,
    MassUse,
    Decay,
    Death
}



[Game]
public class NicheLifecycleComponent : IComponent
{
    public int OpenDate;
    public Dictionary<NicheLifecyclePhase, int> Growth;
}

[Game]
public class NicheStateComponent : IComponent
{
    public NicheLifecyclePhase Phase;
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