using Entitas;
using UnityEngine.UI;

public enum TaskType
{
    UpgradeProduct,
    ExploreProduct
}

public class TaggedProgressBar : View
{
    public TaskType TaskType;
    public Text ProgressBarDescription; // task description

    public ProgressBar ProgressBar;

    TaskComponent GetTask(TaskType taskType)
    {
        GameEntity[] gameEntities = Contexts.sharedInstance.game.GetEntities(GameMatcher.Task);
        // TODO: add filtering tasks, which are done by other players!

        if (gameEntities.Length == 0)
            return null;

        return gameEntities[0].task;
    }

    void HideProgressBar()
    {
        ProgressBar.gameObject.SetActive(false);
        ProgressBarDescription.gameObject.SetActive(false);
    }

    void Update()
    {
        TaskComponent taskComponent = GetTask(TaskType);

        if (taskComponent == null)
        {
            HideProgressBar();
            return;
        }

        ProgressBar.gameObject.SetActive(true);
        ProgressBarDescription.gameObject.SetActive(true);

        float progress = (CurrentIntDate - taskComponent.StartTime) * 100f / taskComponent.Duration;

        ProgressBarDescription.text = GetDescriptionByTask(taskComponent.TaskType);
        ProgressBar.SetValue(progress);
    }

    private string GetDescriptionByTask(TaskType taskType)
    {
        switch (taskType)
        {
            case TaskType.UpgradeProduct:
                return "Upgrading product...";

            default:
                return $"progressbar fail {taskType}";
        }
    }
}
