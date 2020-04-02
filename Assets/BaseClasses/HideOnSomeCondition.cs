using UnityEngine;

public abstract class HideOnSomeCondition : View
{
    public GameObject[] HideableItems;
    public bool Auto = false;

    public override void ViewRender()
    {
        base.ViewRender();

        bool show = !HideIf();

        //if (Auto)
        //{
        //    HideableItems = transform.GetComponentInChildren()
        //}

        foreach (var item in HideableItems)
            item.SetActive(show);
    }

    // hide if true
    public abstract bool HideIf();
}
