using Entitas;
using UnityEngine.UI;

public enum TaskType
{
    UpgradeProduct,
    //ExploreProduct,
    StealIdeas,
    ShareExpertise,
    ImproveRelations,
    CrossPromotion,
}

public class TaggedProgressBar : View
{
    public TaskType TaskType;
    public Text ProgressBarDescription; // task description

    public ProgressBar ProgressBar;

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

            case TaskType.StealIdeas:
                return "Analysing competitor...";

            case TaskType.ShareExpertise:
                return "Exchanging expertise...";

            case TaskType.CrossPromotion:
                return "Exchanging users...";

            default:
                return $"progressbar fail {taskType}";
        }
    }
}
