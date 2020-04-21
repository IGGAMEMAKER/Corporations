using Assets.Core;
using Entitas;
using UnityEngine;

public class ScheduleRunnerSystem : IExecuteSystem
{
    // TODO TIMER
    readonly GameContext _context;
    float totalTime;

    bool isTimerRunning => DateEntity.isTimerRunning;
    int currentSpeed => DateEntity.speed.Speed;

    GameEntity DateContainer;

    GameEntity DateEntity
    {
        get
        {
            if (DateContainer == null || !DateContainer.hasDate)
                DateContainer = ScheduleUtils.GetDateContainer(_context);

            return DateContainer;
        }
    }

    public ScheduleRunnerSystem(Contexts contexts)
    {
        _context = contexts.game;
    }

    public void Execute()
    {
        if (DateEntity == null)
            return;

        CheckPressedButtons();

        totalTime -= Time.deltaTime;

        if (totalTime < 0 && isTimerRunning)
        {


            // ResetTimer();
            totalTime = 1 / (float) currentSpeed;

            ScheduleUtils.IncreaseDate(_context, 1);
        }
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

    void ToggleTimer()
    {
        ScheduleUtils.ToggleTimer(_context);
    }
}
