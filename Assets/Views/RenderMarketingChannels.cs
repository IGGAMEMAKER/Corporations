using UnityEngine;

public class RenderMarketingChannels : View
{
    public GameObject SEO;

    public GameObject Release;
    public GameObject Test;

    public GameObject Branding;
    public GameObject Targeting;

    public override void ViewRender()
    {
        base.ViewRender();

        var isReleased = SelectedCompany.isRelease;

        Release.SetActive(!isReleased);
        Test.SetActive(!isReleased);

        SEO.SetActive(isReleased);
        Branding.SetActive(isReleased);
        Targeting.SetActive(isReleased);
    }
}
