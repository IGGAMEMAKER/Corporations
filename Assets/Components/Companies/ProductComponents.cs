using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;
using UnityEngine;


public class ExpertiseComponent : IComponent
{
    public int ExpertiseLevel;
}

[Game, Event(EventTarget.Self)]
public class ProductComponent : IComponent
{
    public NicheType Niche;

    public int Concept;
}

public enum ProductImprovement
{
    Acquisition,
    Retention,
    Monetisation
}
public class FeaturesComponent : IComponent
{
    public Dictionary<ProductImprovement, int> features;
    public int Count;
}

[Game, Event(EventTarget.Self)]
public class MarketingComponent : IComponent
{
    public long clients;
}

public class BrandingComponent : IComponent
{
    public float BrandPower;
}

public enum Financing
{
    Marketing,
    Development,
    Team
}
public class FinancingComponent : IComponent
{
    public Dictionary<Financing, int> Financing;
}

[Game, Event(EventTarget.Self)]
public class ReleaseComponent : IComponent { }

[Game, Event(EventTarget.Self), Event(EventTarget.Any)]
public class TechnologyLeaderComponent : IComponent { }

[Game, Event(EventTarget.Self)]
public class CrunchingComponent : IComponent { }

public class DumpingComponent : IComponent { }

