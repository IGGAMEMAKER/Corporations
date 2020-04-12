using Assets.Core;
using Entitas;

public class ScheduleInitializeSystem : IInitializeSystem
{
    readonly GameContext _context;

    public ScheduleInitializeSystem(Contexts contexts)
    {
        _context = contexts.game;
    }

    void IInitializeSystem.Initialize()
    {
        var DateEntity = _context.CreateEntity();
        DateEntity.AddDate(0, 3);
        DateEntity.AddSpeed(3);
        DateEntity.AddTargetDate(0);

        ScheduleUtils.PauseGame(_context);
    }
}
