using UnityEngine;

public class HideCrunchButton : View
{
    public GameObject CrunchButton;

    public override void ViewRender()
    {
        base.ViewRender();

        //CrunchButton.SetActive(IsMyProductCompany);
    }
}
