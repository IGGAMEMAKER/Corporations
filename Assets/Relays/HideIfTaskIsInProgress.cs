using Assets.Core;
using UnityEngine;

// Hides start action buttons for player convenience
public class HideIfTaskIsInProgress : View
{
    public CompanyTask TaskType;
    public GameObject GameObject;

    void Update()
    {
        GameObject.SetActive(!HasTask(TaskType));
    }

    bool HasTask(CompanyTask taskType)
    {
        // TODO

        return ScheduleUtils.GetTask(GameContext, taskType) != null;
    }
}
