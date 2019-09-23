using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;
using UnityEngine;


[Game, Event(EventTarget.Self)]
public class DevelopmentFocusComponent : IComponent
{
    public DevelopmentFocus Focus;
}

public class ExpertiseComponent : IComponent
{
    public int ExpertiseLevel;
}

[Game, Event(EventTarget.Self)]
public class ProductComponent : IComponent
{
    public int Id;
    public NicheType Niche;

    public int Concept;
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
public class FinanceComponent : IComponent
{
    public Pricing price;
    public MarketingFinancing marketingFinancing;
    public int salaries;
    public float basePrice;
}

public class TeamImprovementsComponent : IComponent
{
    public Dictionary<TeamUpgrade, int> Upgrades;
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


