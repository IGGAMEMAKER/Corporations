using System.Collections.Generic;
using System.Text;

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

public class StartProfilingSystem : OnDateChange
{
    private static StringBuilder _profiler;
    public static long ProfilerMilliseconds;

    public static bool StartMeasuring;

    public static StringBuilder MyProfiler
    {
        get
        {
            if (_profiler == null)
                _profiler = new StringBuilder();

            return _profiler;
        }
    }
    
    public StartProfilingSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        if (StartMeasuring)
        {
            // print results
        }
        else
        {
            
        }
    }
}