using UnityEngine;

public abstract class HideOnSomeCondition : View
{
    public GameObject[] HideableItems;

    public override void ViewRender()
    {
        base.ViewRender();

        bool show = CheckConditions();

        foreach (var item in HideableItems)
            item.SetActive(show);
    }

    // show if true
    public abstract bool CheckConditions();
}
