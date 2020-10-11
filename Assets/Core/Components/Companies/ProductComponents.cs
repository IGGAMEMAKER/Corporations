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

public class ServerAttackComponent : IComponent
{
    public long Load;
    public int Resistance;
    public int CurrentResistance;
}

//public enum ProductFeature
//{
//    Acquisition,
//    Retention,
//    Monetisation
//}

public class SupportBonus
{
    // max bonus
    public long Max;

    public SupportBonus(long Max)
    {
        this.Max = Max;
    }
}

public class SupportBonusHighload : SupportBonus
{
    public SupportBonusHighload(long Max) : base(Max)
    {
    }
}

public class SupportBonusMarketingSupport : SupportBonus
{
    public SupportBonusMarketingSupport(long Max) : base(Max)
    {
    }
}


public class FeatureBonus
{
    // max bonus
    public float Max;

    public FeatureBonus(float Max)
    {
        this.Max = Max;
    }

    public bool isAcquisitionFeature => this is FeatureBonusAcquisition;
    public bool isMonetisationFeature => this is FeatureBonusMonetisation;
    public bool isRetentionFeature => this is FeatureBonusRetention;
}

public class FeatureBonusAcquisition : FeatureBonus
{
    public FeatureBonusAcquisition(float Max) : base(Max)
    {
    }
}
public class FeatureBonusRetention : FeatureBonus
{
    public FeatureBonusRetention(float Max) : base(Max)
    {
    }
}
public class FeatureBonusMonetisation : FeatureBonus
{
    public FeatureBonusMonetisation(float Max) : base(Max)
    {
    }
}

public class NewProductFeature
{
    public string Name;
    public FeatureBonus FeatureBonus;
    public List<int> AttitudeToFeature;

    public NewProductFeature(string name, List<int> attitudes, float monetisationBenefit = 0)
    {
        Name = name;

        if (monetisationBenefit > 0)
            FeatureBonus = new FeatureBonusMonetisation(monetisationBenefit);
        else
            FeatureBonus = new FeatureBonusRetention(5);

        AttitudeToFeature = attitudes;
    }
}

public class SupportFeature
{
    public string Name;
    public SupportBonus SupportBonus;
}

public class SupportUpgradesComponent : IComponent
{
    // string - upgrade name, int - amount of upgrades
    public Dictionary<string, int> Upgrades;
}

public class FeaturesComponent : IComponent
{
    //public Dictionary<ProductFeature, int> features;

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
    // int - segmentId
    public Dictionary<int, long> ClientList;
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

public class AudienceInfo
{
    public string Name;
    public int Loyalty;
    public List<int> Needs; // +1, -1, +2, +1, -5, -10, 0, 0
    public string Icon;

    public long Size;
    public List<FeatureBonus> Bonuses;

    public int ID;
}