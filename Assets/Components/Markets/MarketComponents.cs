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
    public int OpenDate;
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

[Game]
public class NicheStateComponent : IComponent
{
    public int[] Growth;

    // when someone innovates, increment this
    public int Level;
}
