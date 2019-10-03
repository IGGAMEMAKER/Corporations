public abstract class HideTaskView : HideOnSomeCondition
{
    bool show = false;
    public TaskView TaskView;

    public override bool HideIf()
    {
        var task = GetTask();
        var hasTask = task != null;

        if (hasTask && !show)
        {
            show = true;
            TaskView.SetEntity(task);
        }

        show = hasTask && !task.isCompleted;

        return !show;
    }

    public abstract TaskComponent GetTask();
}
