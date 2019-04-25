using Entitas;
using UnityEngine;

public class ScheduleRunnerSystem : IInitializeSystem, IExecuteSystem
{
    readonly GameContext _context;
    bool isTimerRunning = false;

    float totalTime;
    float currentSpeed = 1;
    float baseSpeed = 0.5f;

    GameEntity DateEntity;

    public ScheduleRunnerSystem(Contexts contexts)
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

    void UpdateWorld()
    {
        ResetTimer();

        DateEntity.ReplaceDate(DateEntity.date.Date + 1);
    }

    void ResetTimer()
    {
        totalTime = baseSpeed / currentSpeed;
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
