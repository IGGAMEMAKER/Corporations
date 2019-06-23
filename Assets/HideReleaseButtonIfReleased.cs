using UnityEngine;

public class HideReleaseButtonIfReleased : View
{
    public GameObject ReleaseCampaign;

    public override void ViewRender()
    {
        base.ViewRender();

        if (MyProductEntity.isRelease)
            ReleaseCampaign.SetActive(false);
    }
}
