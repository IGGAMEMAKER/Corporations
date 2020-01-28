using Assets.Core;
using UnityEngine.UI;

public abstract class TimedButton : UpgradedButtonController
{
    internal int CompanyId;

    public void SetCompanyId(int companyId)
    {
        CompanyId = companyId;

        ViewRender();
    }

    public abstract string StandardTitle();
    public abstract string ShortTitle();
    //public abstract int QueuedTasks();

    public abstract CompanyTask GetCompanyTask();

    public bool HasActiveTimer()
    {
        return Cooldowns.IsHasTask(GameContext, GetCompanyTask());
    }
    int TimeRemaining()
    {
        var task = Cooldowns.GetTask(GameContext, GetCompanyTask());

        if (task == null)
            return 0;

        return task.EndTime - CurrentIntDate;
    }


    public override void ViewRender()
    {
        base.ViewRender();

        var title = StandardTitle();

        if (HasActiveTimer())
        {
            title = ShortTitle() + "\n";
            title += $"will finish in {TimeRemaining()} days";
        }

        GetComponentInChildren<Text>().text = title;
        GetComponent<Button>().interactable = IsInteractable();
    }

    // is interactable
    // check if can queue/do parallel tasks
}