using Entitas;

public enum TaskType
{
    UpgradeProduct,
}

public class TaggedProgressBar : View
{
    public TaskType TaskType;
    ProgressBar ProgressBar;

    private void Awake()
    {
        ProgressBar = GetComponent<ProgressBar>();
    }

    TaskComponent GetTask(TaskType taskType)
    {
        GameEntity[] gameEntities = Contexts.sharedInstance.game.GetEntities(GameMatcher.Task);
        // TODO: add filtering tasks, which are done by other players!

        if (gameEntities.Length == 0)
            return null;

        return gameEntities[0].task;
    }

    void Update()
    {
        TaskComponent taskComponent = GetTask(TaskType);

        float progress;

        if (taskComponent == null)
            progress = 0;
        else
            progress = (CurrentIntDate - taskComponent.StartTime) * 100f / taskComponent.Duration;

        ProgressBar.SetValue(progress);
    }
}
