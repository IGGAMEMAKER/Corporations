using Assets.Core;
using Entitas;
using UnityEngine;

public class ScheduleRunnerSystem : IInitializeSystem, IExecuteSystem
{
    // TODO TIMER
    readonly GameContext _context;
    bool isTimerRunning => DateEntity.isTimerRunning;

    float totalTime;
    int currentSpeed => DateEntity.speed.Speed;

    GameEntity DateEntity;

    public ScheduleRunnerSystem(Contexts contexts)
    {
        _context = contexts.game;
    }

    public void Initialize()
    {
        DateEntity = _context.CreateEntity();
        DateEntity.AddDate(0, 3);
        DateEntity.AddSpeed(3);
        DateEntity.AddTargetDate(0);

        ScheduleUtils.PauseGame(_context);

        ResetTimer();
    }

    public void Execute()
    {
        CheckPressedButtons();

        totalTime -= Time.deltaTime;

        if (totalTime < 0 && isTimerRunning)
        {
            ResetTimer();

            ScheduleUtils.IncreaseDate(_context, 1);

            //if (DateEntity.date.Date == DateEntity.targetDate.Date)
            //    ScheduleUtils.PauseGame(_context);
        }
    }

    void ResetTimer()
    {
        totalTime = 1 / (float) currentSpeed;
    }


    void ToggleTimer()
    {
        //DateEntity.isTimerRunning = !isTimerRunning;
        ScheduleUtils.ToggleTimer(_context);
    }

    void CheckPressedButtons()
    {
        // on right click
        // on right mouse click
        // on right mouse button
        //if (Input.GetMouseButtonUp(1))
        //    ToggleTimer();

        //if (Input.GetKeyUp(KeyCode.Space))
        //    ToggleTimer();

        //if (Input.GetKeyUp(KeyCode.KeypadPlus) && currentSpeed < 18)
        //    UpdateSpeed(2);
        ////currentSpeed += 2;

        //if (Input.GetKeyUp(KeyCode.KeypadMinus) && currentSpeed > 2)
        //    UpdateSpeed(-1);
        //    //currentSpeed--;
    }
}
