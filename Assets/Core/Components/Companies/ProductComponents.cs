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

    // string => feature name, float - rating 0...100f
    public Dictionary<string, float> Upgrades;
    public int Count;
}

public enum ProductUpgrade
{
    // one time stuff
    // ------------------

    SimpleConcept,
    TestCampaign,

    // 1+ worker
    Team3,
    Team7,
    Team20,
    Team100,

    AutorecruitWorkers,

    // add newPlatformCooldown
    // --> +brand
    // --> +marketing cost
    // --> slight reach increase

    // on revoke
    // --> -Brand

    PlatformDesktop,
    PlatformWeb,
    PlatformMobileAndroid,
    PlatformMobileIOS,

    // +big reach increase of targeting campaigns
    // +cost of marketing
    MarketingDesktop,
    MarketingWeb,
    MarketingAndroid,
    MarketingIOS,

    // upgradeable stuff
    // -----------------

    TargetingCampaign,
    TargetingCampaign2,
    TargetingCampaign3,

    // +brand
    BrandCampaign,
    BrandCampaign2,
    BrandCampaign3,

    // +income
    // -brand
    Monetisation,
    Monetisation2,
    Monetisation3,

    // - brand decay
    Support,
    Support2,
    Support3,

    // + brand on revolutions
    // + brand base
    QA,
    QA2,
    QA3,

    // -chance of fuckup: -clients -product level
    Backups,
    Backups2,
    Backups3,

    CreateSupportTeam,
    CreateQATeam,
    CreateManagementTeam,
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

[Game, Event(EventTarget.Self)]
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

