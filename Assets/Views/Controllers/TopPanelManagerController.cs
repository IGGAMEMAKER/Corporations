using Michsky.UI.Frost;

public class TopPanelManagerController : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        GetComponent<TopPanelManager>().Load();
    }
}
