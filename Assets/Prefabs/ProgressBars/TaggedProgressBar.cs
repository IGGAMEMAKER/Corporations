using Assets.Utils;
using UnityEngine.UI;

public enum TaskType
{
    UpgradeProduct,
    StealIdeas,
    ShareExpertise,
    ImproveRelations,
    CrossPromotion,
}

public class TaggedProgressBar : View
    , IAnyDateListener
{
    public CompanyTask TaskType;
    public Text ProgressBarDescription; // task description

    public ProgressBar ProgressBar;

    void OnEnable()
    {
        // TODO Update/Unsub
        ListenDateChanges(this);

        Render();
    }

    void HideProgressBar()
    {
        ProgressBar.gameObject.SetActive(false);
        ProgressBarDescription.gameObject.SetActive(false);
    }

    void Render()
    {
        TaskComponent taskComponent = ScheduleUtils.GetTask(GameContext, TaskType);

        if (taskComponent == null)
        {
            HideProgressBar();
            return;
        }

        //Debug.Log("There is the task " + taskComponent.StartTime);

        ProgressBar.gameObject.SetActive(true);
        ProgressBarDescription.gameObject.SetActive(true);

        float progress = ScheduleUtils.GetTaskCompletionPercentage(GameContext, taskComponent);

        ProgressBarDescription.text = GetDescriptionByTask(taskComponent.CompanyTask);
        ProgressBar.SetValue(progress);
    }

    private string GetDescriptionByTask(CompanyTask taskType)
    {
        switch (taskType)
        {
            default:
                return $"progressbar fail {taskType.CompanyTaskType}";
        }
    }

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }
}
