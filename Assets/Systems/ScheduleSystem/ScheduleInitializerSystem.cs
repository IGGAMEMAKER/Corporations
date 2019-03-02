using Entitas;
using UnityEngine;

public class ScheduleInitializerSystem : IInitializeSystem, IExecuteSystem
{
    readonly GameContext _context;
    bool isTimerRunning = false;

    float totalTime;
    float currentSpeed = 1;

    GameEntity DateEntity;

    public ScheduleInitializerSystem(Contexts contexts)
    {
        _context = contexts.game;
    }

    public void Initialize()
    {
        DateEntity = _context.CreateEntity();
        DateEntity.AddDate(0);

        ResetTimer();
    }

    public void Execute()
    {
        CheckPressedButtons();

        UpdateTimer();
    }

    void ToggleTimer()
    {
        isTimerRunning = !isTimerRunning;
    }

    void ProcessTasks()
    {
        GameEntity[] tasks = _context.GetEntities(GameMatcher.Task);

        foreach (var t in tasks)
        {
            if (t.task.EndTime >= DateEntity.date.Date)
                t.ReplaceTask(true, t.task.TaskType, t.task.StartTime, t.task.Duration, t.task.EndTime);
        }
    }

    void UpdateWorld()
    {
        ResetTimer();

        Debug.Log("timer++");

        DateEntity.ReplaceDate(DateEntity.date.Date + 1);

        ProcessTasks();
    }

    void ResetTimer()
    {
        totalTime = 0.25f / currentSpeed;
    }

    private void UpdateTimer()
    {
        totalTime -= Time.deltaTime;

        if (totalTime < 0 && isTimerRunning)
            UpdateWorld();
    }

    void CheckPressedButtons()
    {
        // on right click
        if (Input.GetMouseButtonUp(1))
            ToggleTimer();

        if (Input.GetKeyUp(KeyCode.Space))
            ToggleTimer();

        if (Input.GetKeyUp(KeyCode.KeypadPlus) && currentSpeed < 3)
            currentSpeed++;

        if (Input.GetKeyUp(KeyCode.KeypadMinus) && currentSpeed > 1)
            currentSpeed--;
    }
}
