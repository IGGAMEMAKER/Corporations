using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;
using UnityEngine;


// tasks
public class TaskComponent : IComponent
{
    public bool isCompleted;
    public CompanyTask CompanyTask;

    public int StartTime;
    public int Duration;
    public int EndTime;


}

//[Game]
//public class CooldownContainerComponent : IComponent
//{
//    public Dictionary<string, Cooldown> Cooldowns;
//}


public enum CompanyTaskType
{
    ExploreMarket,
    ExploreCompany,

    AcquiringCompany,
    AcquiringParlay,

    UpgradeFeature,
    TestCampaign,
    RegularCampaign,
    BrandingCampaign,

    ReleasingApp,

    CorporateCulture,
    ImproveSegment,
}

public abstract class CompanyTask
{
    public CompanyTaskType CompanyTaskType;
    public int CompanyId;

    public bool Equals(CompanyTask obj)
    {
        return CompanyTaskType == obj.CompanyTaskType && CompanyId == obj.CompanyId && EqualsExactly(obj);
    }
    public virtual bool EqualsExactly(CompanyTask obj) => true;
}

public class CompanyTaskMarketingTestCampaign : CompanyTask
{
    public CompanyTaskMarketingTestCampaign(int companyId)
    {
        CompanyId = companyId;
        CompanyTaskType = CompanyTaskType.TestCampaign;
    }
}

public class CompanyTaskMarketingRegularCampaign : CompanyTask
{
    public CompanyTaskMarketingRegularCampaign(int companyId)
    {
        CompanyId = companyId;
        CompanyTaskType = CompanyTaskType.RegularCampaign;
    }
}

public class CompanyTaskBrandingCampaign : CompanyTask
{
    public CompanyTaskBrandingCampaign(int companyId)
    {
        CompanyId = companyId;
        CompanyTaskType = CompanyTaskType.BrandingCampaign;
    }
}

public class CompanyTaskReleaseApp : CompanyTask
{
    public CompanyTaskReleaseApp(int companyId)
    {
        CompanyId = companyId;
        CompanyTaskType = CompanyTaskType.ReleasingApp;
    }
}

public class CompanyTaskUpgradeFeature : CompanyTask
{
    public ProductFeature ProductImprovement;

    public CompanyTaskUpgradeFeature(int companyId, ProductFeature improvement)
    {
        CompanyId = companyId;
        CompanyTaskType = CompanyTaskType.UpgradeFeature;

        ProductImprovement = improvement;
    }

    public override bool EqualsExactly(CompanyTask obj)
    {
        return (obj as CompanyTaskUpgradeFeature).ProductImprovement == ProductImprovement;
    }
}

public class CompanyTaskAcquisition : CompanyTask
{
    public CompanyTaskAcquisition(int companyId)
    {
        CompanyId = companyId;
        CompanyTaskType = CompanyTaskType.AcquiringCompany;
    }
}

public class CompanyTaskExploreCompany : CompanyTask
{
    public CompanyTaskExploreCompany(int companyId)
    {
        CompanyTaskType = CompanyTaskType.ExploreCompany;
        CompanyId = companyId;
    }
}

// markets
public class CompanyTaskExploreMarket : CompanyTask
{
    public NicheType NicheType;

    public CompanyTaskExploreMarket(NicheType nicheType)
    {
        CompanyTaskType = CompanyTaskType.ExploreMarket;
        NicheType = nicheType;

        CompanyId = -1;
    }

    public override bool EqualsExactly(CompanyTask obj)
    {
        return NicheType == (obj as CompanyTaskExploreMarket).NicheType;
    }
}
