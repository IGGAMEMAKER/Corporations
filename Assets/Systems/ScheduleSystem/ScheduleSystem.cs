public class ScheduleSystems : Feature
{
    public ScheduleSystems(Contexts contexts) : base("Schedule Systems")
    {
        Add(new CooldownContainerInitializerSystem(contexts));
        Add(new ScheduleRunnerSystem(contexts));
        Add(new ScheduleTaskProcessingSystem(contexts));
        Add(new ScheduleCooldownProcessingSystem(contexts));
        Add(new TaskProcessingSystem(contexts));
    }
}
