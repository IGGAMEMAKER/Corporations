using System.Collections.Generic;
using Assets.Core;
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
    // private ProfilingComponent _profiler;

    // public static ProfilingComponent MyProfiler
    // {
    //     get
    //     {
    //         if (_profiler == null)
    //             _profiler = Companies.GetProfilingComponent(Contexts);
    //
    //         return _profiler;
    //     }
    // }
    
    public ProfilingSystem(Contexts contexts) : base(contexts)
    {
        // _profiler = Companies.GetProfilingComponent(gameContext);

    }

    protected override void Execute(List<GameEntity> entities)
    {
        var profiler = Companies.GetProfilingComponent(gameContext);
        
        var myProfiler = profiler.MyProfiler;
        var profilerMilliseconds = profiler.ProfilerMilliseconds;
        
        int date = ScheduleUtils.GetCurrentDate(gameContext);
        
        if (myProfiler.Length > 0)
        {
            bool isPeriodEnd = ScheduleUtils.IsPeriodEnd(date);
            bool isMonthEnd = ScheduleUtils.IsMonthEnd(date);

            var prefix = "";

            prefix += "Total: <b>" + profilerMilliseconds + "ms</b> ";

            if (isMonthEnd)
            {
                prefix += "<b>MONTH</b>: ";
            }

            if (isPeriodEnd)
            {
                prefix += "<b>PERIOD</b>: ";
            }

            prefix += "\n\n";

            var text = prefix + myProfiler.ToString() + "\n\n<b>TAGS</b>\n";

            foreach (var pair in profiler.Tags)
            {
                text += $"\n{pair.Key}: {pair.Value}ms";
            }

            Debug.Log(text);
            
            myProfiler.Clear();
            profiler.ProfilerMilliseconds = 0;
            profiler.Tags.Clear();
        }
    }
}