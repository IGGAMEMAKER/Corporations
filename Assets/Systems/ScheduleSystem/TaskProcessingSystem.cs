using System.Collections.Generic;
using Assets.Core;
using Entitas;
using UnityEngine;

public partial class TaskProcessingSystem : OnDateChange
{
    public TaskProcessingSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity[] tasks = ScheduleUtils.GetTasks(gameContext);
        var date = ScheduleUtils.GetCurrentDate(gameContext);

        for (var i = tasks.Length - 1; i >= 0; i--)
        {
            var t = tasks[i];
            var task = t.task;

            var EndTime = task.EndTime;

            if (date >= EndTime && !task.isCompleted)
            {
                //Debug.Log("Finishing task " + task.CompanyTask.CompanyTaskType);

                ProcessTask(task);
                t.task.isCompleted = true;
            }

            // 
            if (date > EndTime)
                t.Destroy();
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasDate;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Date);
    }
}

public partial class TaskProcessingSystem : OnDateChange
{
    private void ProcessTask(TaskComponent taskComponent)
    {
        var task = taskComponent.CompanyTask;
        switch (task.CompanyTaskType)
        {
            case CompanyTaskType.ExploreMarket: ExploreMarket(task); break;
            case CompanyTaskType.ExploreCompany: ExploreCompany(task); break;
            case CompanyTaskType.AcquiringCompany: AcquireCompany(task); break;
            case CompanyTaskType.UpgradeFeature: UpgradeFeature(task); break;
            case CompanyTaskType.ReleasingApp: ReleaseApp(task); break;

            case CompanyTaskType.TestCampaign: TestCampaign(task); break;
            case CompanyTaskType.RegularCampaign: RegularCampaign(task); break;
            case CompanyTaskType.BrandingCampaign: BrandingCampaign(task); break;
        }
    }

    void ReleaseApp(CompanyTask task)
    {

    }

    void TestCampaign(CompanyTask task)
    {
        var t = task as CompanyTaskMarketingTestCampaign;

        var c = Companies.GetCompany(gameContext, t.CompanyId);
        MarketingUtils.AddClients(c, 100);
    }

    void RegularCampaign(CompanyTask task)
    {
        var t = task as CompanyTaskMarketingRegularCampaign;

        var c = Companies.GetCompany(gameContext, t.CompanyId);

        var clients = MarketingUtils.GetAudienceGrowth(c, gameContext);

        MarketingUtils.AddClients(c, clients);
    }

    void BrandingCampaign(CompanyTask task)
    {
        var t = task as CompanyTaskBrandingCampaign;

        var c = Companies.GetCompany(gameContext, t.CompanyId);

        MarketingUtils.AddBrandPower(c, Balance.BRAND_CAMPAIGN_BRAND_POWER_GAIN);

        var clients = MarketingUtils.GetAudienceGrowth(c, gameContext);
        MarketingUtils.AddClients(c, clients);
    }



    void AcquireCompany(CompanyTask task)
    {
        //var nicheType = (task as CompanyTaskExploreMarket).NicheType;

        //var niche = NicheUtils.GetNicheEntity(gameContext, nicheType);
        //niche.AddResearch(1);
    }

    void ExploreMarket(CompanyTask task)
    {
        var nicheType = (task as CompanyTaskExploreMarket).NicheType;

        var niche = Markets.GetNiche(gameContext, nicheType);
        niche.AddResearch(1);
    }

    void ExploreCompany(CompanyTask task)
    {
        var c = Companies.GetCompany(gameContext, task.CompanyId);
        c.AddResearch(1);
    }

    void UpgradeFeature(CompanyTask task)
    {
        var t = (task as CompanyTaskUpgradeFeature);

        var product = Companies.GetCompany(gameContext, t.CompanyId);

        product.features.features[t.ProductImprovement]++;
        product.features.Count++;
    }
}