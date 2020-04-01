using Assets.Core;
using TMPro;
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
        var t = GetCompanyTask();

        return Cooldowns.GetTask(Q, t) != null;
    }

    int TimeRemaining()
    {
        var task = Cooldowns.GetTask(Q, GetCompanyTask());

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

        GetComponentInChildren<TextMeshProUGUI>().text = title;
        GetComponentInChildren<Button>().interactable = IsInteractable();
    }

    // is interactable
    // check if can queue/do parallel tasks
}