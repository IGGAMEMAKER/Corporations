using Entitas;
using UnityEngine;

public enum TaskType
{
    UpgradeProduct,
}

public class TaggedProgressBar : View
{
    public TaskType TaskType;
    ProgressBar ProgressBar;
    public int CurrentDate = 5;



    private void Awake()
    {
        ProgressBar = GetComponent<ProgressBar>();
    }

    TaskComponent GetTask(TaskType taskType)
    {
        GameEntity[] gameEntities = Contexts.sharedInstance.game
            .GetEntities(GameMatcher.AllOf(GameMatcher.Task));

        if (gameEntities.Length == 0)
        {
            TaskComponent taskComponent = new TaskComponent
            {
                Duration = 10,
                StartTime = 5
            };
            taskComponent.EndTime = taskComponent.StartTime + taskComponent.Duration;

            return taskComponent;
            return null;
        }

        Debug.Log("Make proper TaskType filter in TaskedProgressBar!");

        return gameEntities[0].task;
    }

    // Update is called once per frame
    void Update()
    {
        TaskComponent taskComponent = GetTask(TaskType);

        if (taskComponent == null)
            return;

        int duration = CurrentDate - taskComponent.StartTime;

        ProgressBar.SetValue(duration * 100f / taskComponent.Duration);
    }
}
