using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketingChannelsContainer : View
{
    public GameObject SEO;
    public GameObject Testing;
    public GameObject Branding;
    public GameObject Targeting;
    public GameObject Release;

    public override void ViewRender()
    {
        base.ViewRender();

        if (MyProductEntity.isRelease)
        {
            Branding.SetActive(true);
            Targeting.SetActive(true);
            SEO.SetActive(true);

            Testing.SetActive(false);
            Release.SetActive(false);
        } else
        {
            Branding.SetActive(false);
            Targeting.SetActive(false);
            SEO.SetActive(false);

            Testing.SetActive(true);
            Release.SetActive(true);
        }
    }
}
