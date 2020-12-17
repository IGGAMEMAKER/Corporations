using Assets;

public class CloseMyModalWindowIfListIsBlank : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        if (GetComponent<ListView>().count == 0)
        {
            SoundManager.Play(Sound.CorporatePolicyTweak);
            Hide(GetComponentInParent<MyModalWindow>());
        }
    }
}
