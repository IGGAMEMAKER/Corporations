using Assets.Core;
using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class TestRunnerSystem : IInitializeSystem, IExecuteSystem
{
    readonly GameContext _context;

    GameEntity TestEntity;

    public TestRunnerSystem(Contexts contexts)
    {
        _context = contexts.game;
    }

    public void Initialize()
    {
        TestEntity = _context.CreateEntity();
        TestEntity.AddTest(
            new Dictionary<LogTypes, bool>
            {
                [LogTypes.MyProductCompany] = true,
                [LogTypes.MyProductCompanyCompetitors] = false,
            });
    }

    public void Execute()
    {
        CheckPressedButtons();
    }

    void UpdateLogs(LogTypes logTypes, string format)
    {
        var logs = TestEntity.test.logs;

        bool value = !logs[logTypes];

        logs[logTypes] = value;

        Debug.LogFormat(format, value ? Visuals.Positive("ENABLED") : Visuals.Negative("DISABLED"));

        TestEntity.ReplaceTest(logs);
    }

    void CheckLogs(KeyCode keyCode, LogTypes logTypes, string format = "")
    {
        if (Input.GetKeyUp(keyCode))
            UpdateLogs(logTypes, format);
    }

    void CheckPressedButtons()
    {
        CheckLogs(KeyCode.Alpha1, LogTypes.MyProductCompany, "My product company logs {0}");
        CheckLogs(KeyCode.Alpha2, LogTypes.MyProductCompanyCompetitors, "My competitors logs {0}");
    }
}
