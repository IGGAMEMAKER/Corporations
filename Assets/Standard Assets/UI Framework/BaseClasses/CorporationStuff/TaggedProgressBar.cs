using Assets.Core;
using UnityEngine.UI;

public class TaggedProgressBar : View
{
    public CompanyTask TaskType;
    public Text ProgressBarDescription; // task description

    public ProgressBar ProgressBar;
    public Slider Slider;

    public override void ViewRender()
    {
        base.ViewRender();

        TimedActionComponent taskComponent = ScheduleUtils.GetTask(Q, TaskType);

        if (taskComponent == null)
        {
            HideProgressBar();
            return;
        }

        RenderProgressBar(true);


        ProgressBarDescription.text = GetDescriptionByTask(taskComponent.CompanyTask);

        // set value
        float progress = ScheduleUtils.GetTaskCompletionPercentage(Q, taskComponent);

        ProgressBar.SetValue(progress);
        if (Slider != null)
            Slider.value = (int)progress;
    }


    void HideProgressBar()
    {
        RenderProgressBar(false);
    }

    void RenderProgressBar(bool show)
    {
        Slider.gameObject.SetActive(show);
        ProgressBar.gameObject.SetActive(false);

        ProgressBarDescription.gameObject.SetActive(show);
    }

    private string GetDescriptionByTask(CompanyTask taskType)
    {
        return taskType.CompanyTaskType.ToString();
    }
}
