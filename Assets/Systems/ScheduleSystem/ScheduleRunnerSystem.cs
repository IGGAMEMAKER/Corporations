﻿using Assets.Core;
using Entitas;
using UnityEngine;

public class ScheduleRunnerSystem : IExecuteSystem
{
    // TODO TIMER
    readonly GameContext gameContext;
    float totalTime;

    bool isTimerRunning => DateEntity.isTimerRunning;
    int currentSpeed => DateEntity.speed.Speed;

    GameEntity DateContainer;

    GameEntity DateEntity
    {
        get
        {
            if (DateContainer == null || !DateContainer.hasDate)
                DateContainer = ScheduleUtils.GetDateContainer(gameContext);

            return DateContainer;
        }
    }

    public ScheduleRunnerSystem(Contexts contexts)
    {
        gameContext = contexts.game;
    }

    public void Execute()
    {
        if (DateEntity == null)
            return;

        CheckPressedButtons();

        totalTime -= Time.deltaTime;

        if (totalTime < 0 && isTimerRunning)
        {
            var playerCompany = Companies.GetPlayerCompany(gameContext);

            while (ScheduleUtils.IsLastDayOfPeriod(DateEntity) && Economy.IsWillBecomeBankruptOnNextPeriod(gameContext, playerCompany))
            {
                var profit = Economy.GetProfit(gameContext, playerCompany, true);
                var profit2 = Economy.GetProfit(gameContext, playerCompany);

                var balance = Economy.BalanceOf(playerCompany);

                Debug.Log($"BANKRUPTCY THREAT for {playerCompany.company.Name}. Cash: {Format.Money(balance)}, Profit: {Format.Money(profit2)}\n\n{profit.ToString()}");

                TutorialUtils.Unlock(gameContext, TutorialFunctionality.CanRaiseInvestments);
                TutorialUtils.Unlock(gameContext, TutorialFunctionality.BankruptcyWarning);

                NotificationUtils.AddPopup(gameContext, new PopupMessageBankruptcyThreat(playerCompany.company.Id));

                ScheduleUtils.PauseGame(gameContext);

                return;
            }

            // ResetTimer();
            totalTime = 1 / (float) currentSpeed;

            ScheduleUtils.IncreaseDate(gameContext, 1);
        }
    }

    void UpdateSpeed(int change)
    {
        DateEntity.speed.Speed += change;
    }

    void CheckPressedButtons()
    {
        // on right click
        // on right mouse click
        // on right mouse button
        //if (Input.GetMouseButtonUp(1))
        if (Input.GetKeyDown(KeyCode.Space))
            ToggleTimer();

        if (Input.GetKeyUp(KeyCode.KeypadPlus) && currentSpeed < 18)
            UpdateSpeed(2);
        ////currentSpeed += 2;

        if (Input.GetKeyUp(KeyCode.KeypadMinus) && currentSpeed > 2)
            UpdateSpeed(-1);
        //    //currentSpeed--;
    }

    void ToggleTimer()
    {
        ScheduleUtils.ToggleTimer(gameContext);
    }
}
