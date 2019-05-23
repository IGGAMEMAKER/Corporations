public class ToggleTargetingController : ButtonController
{
    public override void Execute()
    {
        TriggerEventTargetingToggle(MyProduct.Id);

        ReNavigate();
    }
}
