using UnityEngine;

public class HideActionsIfItIsNotMyProductCompany : View
{
    public GameObject TeamActionContainer;

    public override void ViewRender()
    {
        base.ViewRender();

        TeamActionContainer.SetActive(IsMyProductCompany);
    }
}
