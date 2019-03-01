public class ScheduleSystem : Feature
{
    public ScheduleSystem(Contexts contexts) : base("Schedule Systems")
    {
        Add(new ScheduleInitializerSystem(contexts));
    }
}
