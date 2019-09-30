using Assets.Utils;
using UnityEngine;

// Hides start action buttons for player convenience
public class HideIfTaskIsInProgress : View
{
    public CompanyTaskType TaskType;
    public GameObject GameObject;

    void Update()
    {
        GameObject.SetActive(!HasTask(TaskType));
    }

    bool HasTask(CompanyTaskType taskType)
    {
        // TODO

        return ScheduleUtils.GetTask(GameContext, taskType) != null;
    }
}
