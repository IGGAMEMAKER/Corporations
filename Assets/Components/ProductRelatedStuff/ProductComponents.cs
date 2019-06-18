using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;
using UnityEngine;


[Game, Event(EventTarget.Self)]
public class DevelopmentFocusComponent : IComponent
{
    public DevelopmentFocus Focus;
}

[Game, Event(EventTarget.Self)]
public class ProductComponent : IComponent
{
    public int Id;
    public NicheType Niche;

    // features
    public Dictionary<UserType, int> Segments;
}

[Game, Event(EventTarget.Self)]
public class MarketingComponent : IComponent
{
    public long BrandPower;

    // long clients
    public Dictionary<UserType, long> Segments;
}

[Game, Event(EventTarget.Self)]
public class FinanceComponent : IComponent
{
    public Pricing price;
    public MarketingFinancing marketingFinancing;
    public int salaries;
    public float basePrice;
}

[Game, Event(EventTarget.Self)]
public class ReleaseComponent : IComponent { }

[Game, Event(EventTarget.Self)]
public class TargetingComponent : IComponent { }

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


