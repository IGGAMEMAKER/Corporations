using Assets.Utils;
using Entitas;
using UnityEngine;

public class ScheduleRunnerSystem : IInitializeSystem, IExecuteSystem
{
    // TODO TIMER
    readonly GameContext _context;
    bool isTimerRunning => DateEntity.isTimerRunning;

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
        DateEntity.AddTargetDate(0);

        ScheduleUtils.PauseGame(Contexts.sharedInstance.game);

        //DateEntity.isTimerRunning = false;

        ResetTimer();
    }

    public void Execute()
    {
        CheckPressedButtons();

        totalTime -= Time.deltaTime;

        if (totalTime < 0 && isTimerRunning)
        {
            ResetTimer();

            DateEntity.ReplaceDate(DateEntity.date.Date + 1);

            if (DateEntity.date.Date == DateEntity.targetDate.Date)
                ScheduleUtils.PauseGame(_context);
        }
    }


    void ResetTimer()
    {
        totalTime = baseSpeed / currentSpeed;
    }


    void ToggleTimer()
    {
        DateEntity.isTimerRunning = !isTimerRunning;
    }

    void CheckPressedButtons()
    {
        // on right click
        //if (Input.GetMouseButtonUp(1))
        //    ToggleTimer();
        if (Input.GetKeyUp(KeyCode.Space))
            ToggleTimer();

        if (Input.GetKeyUp(KeyCode.KeypadPlus) && currentSpeed < 18)
            currentSpeed += 2;

        if (Input.GetKeyUp(KeyCode.KeypadMinus) && currentSpeed > 1)
            currentSpeed--;
    }
}
