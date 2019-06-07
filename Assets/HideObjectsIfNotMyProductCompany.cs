using UnityEngine;

public class HideObjectsIfNotMyProductCompany : View
{
    public GameObject[] Objects;

    public override void ViewRender()
    {
        base.ViewRender();

        if (!IsMyProductCompany)
        {
            foreach (var o in Objects)
                o.SetActive(false);
        }
    }
}
