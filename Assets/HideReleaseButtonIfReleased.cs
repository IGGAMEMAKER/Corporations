using UnityEngine;

public class HideReleaseButtonIfReleased : View
{
    public GameObject ReleaseCampaign;

    public override void ViewRender()
    {
        base.ViewRender();

        if (!HasProductCompany)
        {
            ReleaseCampaign.SetActive(false);
            return;
        }

        if (MyProductEntity.isRelease)
            ReleaseCampaign.SetActive(false);
    }
}
