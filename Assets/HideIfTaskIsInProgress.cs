using UnityEngine;

// Hides start action buttons for player convenience
public class HideIfTaskIsInProgress : View
{
    public TaskType TaskType;
    public GameObject GameObject;

    bool HasTask(TaskType taskType)
    {
        return GetTask(taskType) != null;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.SetActive(!HasTask(TaskType));
    }
}
