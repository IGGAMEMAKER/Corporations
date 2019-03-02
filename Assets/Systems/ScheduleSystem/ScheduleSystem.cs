public class ScheduleSystem : Feature
{
    public ScheduleSystem(Contexts contexts) : base("Schedule Systems")
    {
        Add(new ScheduleRunnerSystem(contexts));
        Add(new ScheduleTaskProcessingSystem(contexts));
    }
}
