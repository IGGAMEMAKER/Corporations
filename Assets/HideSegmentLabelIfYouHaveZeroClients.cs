using Assets.Utils;
using UnityEngine;

public class HideSegmentLabelIfYouHaveZeroClients : View
{
    public GameObject SegmentLabel;

    public override void ViewRender()
    {
        base.ViewRender();

        SegmentLabel.SetActive(MarketingUtils.GetClients(MyProductEntity) > 0);
    }
}
