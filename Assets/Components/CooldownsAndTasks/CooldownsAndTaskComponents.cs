using Entitas;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;
using UnityEngine;


// tasks
public class TaskComponent : IComponent
{
    public bool isCompleted;
    //public TaskType TaskType;
    //public CompanyTaskType TaskType;
    public CompanyTask CompanyTask;
    public int StartTime;
    public int Duration;
    public int EndTime;
}

[Game]
public class CooldownContainerComponent : IComponent
{
    public Dictionary<string, Cooldown> Cooldowns;
}


public enum CompanyTaskType
{
    ExploreMarket,
    ExploreCompany,

    AcquiringCompany,
    AcquiringParlay,

    UpgradeFeature,
    MarketingActivity,

    ReleasingApp
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

public class CompanyTaskMarketingActivity : CompanyTask
{
    public CompanyTaskMarketingActivity(int companyId)
    {
        CompanyId = companyId;
        CompanyTaskType = CompanyTaskType.MarketingActivity;
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
    public ProductImprovement ProductImprovement;

    public CompanyTaskUpgradeFeature(int companyId, ProductImprovement improvement)
    {
        CompanyId = companyId;
        CompanyTaskType = CompanyTaskType.UpgradeFeature;

        ProductImprovement = improvement;
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
