using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Core;
using Entitas;
using UnityEngine;

public class MainSystem : Feature
{
    public MainSystem(Contexts contexts) : base("Main System")
    {
        // Start point of all our systems

        //Add(new TestSystems(contexts));

        //Add(new TutorialInitializeSystem(contexts));
        //Add(new ScheduleInitializeSystem(contexts));
        //Add(new MenuInitializeSystem(contexts));
        //Add(new NotificationInitializerSystem(contexts));
        //Add(new AnnualReportInitializerSystem(contexts));

        // Initialization
        //Add(new CampaignStatsInitializerSystem(contexts));
        //Add(new MarketInitializerSystem(contexts));
        //Add(new ProductInitializerSystem(contexts));

        // Simulate world to start date
        //Add(new WorldFillerSystem(contexts));


        // ---------------------------------------------------

        // Execution
        Add(new ScheduleSystems(contexts));
        Add(new NotificationSystems(contexts));

        // companies
        Add(new CompanyManagementSystems(contexts));

        // markets
        Add(new MarketSystems(contexts));
        Add(new SpawnerSystems(contexts));

        Add(new StatsSystems(contexts));
    }
}

public class ProfilingSystem : OnDateChange
{
    public ProfilingSystem(Contexts contexts) : base(contexts)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        // GameEntity[] companies = contexts.game
        //     .GetEntities(GameMatcher.AllOf(GameMatcher.Company, GameMatcher.CompanyResource, GameMatcher.MetricsHistory));
        var profiler = gameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Profiling)).First().profiling;
        
        var myProfiler = profiler.MyProfiler;
        var profilerMilliseconds = profiler.ProfilerMilliseconds;
        int date = ScheduleUtils.GetCurrentDate(gameContext);
        
        if (myProfiler.Length > 0)
        {
            bool isPeriodEnd = ScheduleUtils.IsPeriodEnd(date);
            bool isMonthEnd = ScheduleUtils.IsMonthEnd(date);

            var prefix = "";

            prefix += "Total: " + profilerMilliseconds + "ms ";

            if (isMonthEnd)
            {
                prefix += "<b>MONTH</b>: ";
            }

            if (isPeriodEnd)
            {
                prefix += "<b>PERIOD</b>: ";
            }

            prefix += " SYSTEM\n";

            Debug.Log(prefix + myProfiler.ToString());
            
            myProfiler.Clear();
            profiler.ProfilerMilliseconds = 0;
        }
    }
}