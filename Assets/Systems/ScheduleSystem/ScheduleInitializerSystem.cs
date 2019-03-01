using Entitas;

public class ScheduleInitializerSystem : IInitializeSystem
{
    readonly GameContext _context;

    public ScheduleInitializerSystem(Contexts contexts)
    {
        _context = contexts.game;
    }

    public void Initialize()
    {
        _context.CreateEntity().AddDate(0);
    }
}
