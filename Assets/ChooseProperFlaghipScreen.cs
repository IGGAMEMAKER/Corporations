using Assets.Core;
using UnityEngine;

public class ChooseProperFlaghipScreen : View
{
    public GameObject NonReleasedScreen;
    public GameObject ReleasedScreen;

    public override void ViewRender()
    {
        base.ViewRender();

        NonReleasedScreen.SetActive(!Flagship.isRelease);

        ReleasedScreen.SetActive(Flagship.isRelease);
    }
}
