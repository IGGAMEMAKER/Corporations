using UnityEngine;

public abstract class ToggleOnSomeCondition : View
{
    public bool Flip;

    public GameObject[] Items1;
    public GameObject[] Items2;

    public bool Auto = false;

    public override void ViewRender()
    {
        base.ViewRender();

        bool show = !HideIf();

        if (!Flip)
            show = !show;

        foreach (var item in Items2)
            item.SetActive(show);

        foreach (var item in Items1)
            item.SetActive(!show);
    }

    // hide if true
    public abstract bool HideIf();
}
