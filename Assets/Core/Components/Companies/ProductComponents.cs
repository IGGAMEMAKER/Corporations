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

public enum ProductFeature
{
    Acquisition,
    Retention,
    Monetisation
}
public class FeaturesComponent : IComponent
{
    public Dictionary<ProductFeature, int> features;
    public int Count;
}

public enum ProductUpgrade
{
    SimpleConcept,
    TestCampaign,

    Targeting1,
    Targeting2,
    Targeting3,

    //EmailCampaign,
    BrandCampaign,
    BrandCampaign2,
    BrandCampaign3,

    // 1+ worker
    Team3,
    Team7,
    Team20,
    Team100,

    AutorecruitWorkers,

    PlatformDesktop,
    PlatformWeb,
    PlatformMobileAndroid,
    PlatformMobileIOS,

    Monetisation,
    Monetisation2,
    Monetisation3,

    Support,
    Support2,
    Support3,

    QA,
    QA2,
    QA3,

    Backups,
    Backups2,
    Backups3,
}

public class ProductUpgradesComponent : IComponent
{
    public Dictionary<ProductUpgrade, bool> upgrades;
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

