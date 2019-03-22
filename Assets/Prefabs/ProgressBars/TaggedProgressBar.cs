using UnityEngine;
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

    void Render()
    {
        TaskComponent taskComponent = GetTask(TaskType);

        if (taskComponent == null)
        {
            HideProgressBar();
            return;
        }

        //Debug.Log("There is the task " + taskComponent.StartTime);

        ProgressBar.gameObject.SetActive(true);
        ProgressBarDescription.gameObject.SetActive(true);

        float progress = GetTaskCompletionPercentage(taskComponent); // (CurrentIntDate - taskComponent.StartTime) * 100f / taskComponent.Duration;

        ProgressBarDescription.text = GetDescriptionByTask(taskComponent.TaskType);
        ProgressBar.SetValue(progress);
    }

    void Update()
    {
        Render();
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
