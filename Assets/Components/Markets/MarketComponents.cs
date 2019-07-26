using Entitas;
using Entitas.CodeGeneration.Attributes;
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
    public int OpenDate;
}

[Game]
public class ProductPositioningComponent : IComponent
{
    public int Positioning;
}

public struct ProductPositioning
{
    public int marketShare;
    public bool isCompetitive;
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
    public float BasePrice;
    public long ClientBatch;

    public int TechCost;
    public int IdeaCost;
    public int MarketingCost;
    public int AdCost;
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
public class NicheStateComponent : IComponent
{
    public Dictionary<NicheLifecyclePhase, int> Growth;
    public NicheLifecyclePhase Phase;
    public int Duration;
}

[Game]
public class DeadComponent : IComponent { }


    //// when someone innovates, increment this
[Game, Event(EventTarget.Self), Event(EventTarget.Any)]
public class SegmentComponent : IComponent
{
    // int - level
    public Dictionary<UserType, int> Segments;
}

[Game]
public class SegmentLeadersComponent : IComponent
{
    // int - companyId
    // -1 - no absolute leader
    public Dictionary<UserType, int> Leaders;
}