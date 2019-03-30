using UnityEngine;

// Hides start action buttons for player convenience
public class HideIfTaskIsInProgress : View
{
    public TaskType TaskType;
    public GameObject GameObject;

    void Update()
    {
        GameObject.SetActive(!HasTask(TaskType));
    }

    bool HasTask(TaskType taskType)
    {
        return GetTask(taskType) != null;
    }
}
