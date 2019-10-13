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

public class ProductImprovementsComponent : IComponent
{
    public Dictionary<ProductImprovement, int> Improvements;
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

[Game, Event(EventTarget.Self)]
public class ReleaseComponent : IComponent { }

[Game, Event(EventTarget.Self)]
public class TargetingComponent : IComponent { }
public class BrandingCampaignComponent : IComponent { }


[Game, Event(EventTarget.Self), Event(EventTarget.Any)]
public class TechnologyLeaderComponent : IComponent { }

[Game, Event(EventTarget.Self)]
public class CrunchingComponent : IComponent { }

//[Game]
//public class CustomCooldownComponent : IComponent
//{
//    // string => expires
//    // string aka: steal-88, where 88 is Id of target company
//    public Dictionary<string, int> targets;
//}


