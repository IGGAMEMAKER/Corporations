using UnityEngine.UI;

public abstract class TimedButton : UpgradedButtonController
{
    public abstract bool HasActiveTimer();
    public abstract string StandardTitle();
    public abstract int TimeRemaining();
    public abstract int QueuedTasks();

    public override void ViewRender()
    {
        base.ViewRender();

        var title = StandardTitle();

        if (HasActiveTimer())
        {
            title = $"will finish in {TimeRemaining()} days";

            var queued = QueuedTasks();
            if (queued > 1)
                title += $" (x{queued})";
        }

        GetComponentInChildren<Text>().text = title;
    }

    // is interactable
    // check if can queue/do parallel tasks
}