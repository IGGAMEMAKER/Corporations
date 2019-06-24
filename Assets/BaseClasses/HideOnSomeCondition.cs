using UnityEngine;

public abstract class HideOnSomeCondition : View
{
    public GameObject[] HideableItems;

    public override void ViewRender()
    {
        base.ViewRender();

        bool show = !HideIf();

        foreach (var item in HideableItems)
            item.SetActive(show);
    }

    // hide if true
    public abstract bool HideIf();
}
