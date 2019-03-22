public class ToggleTargetingController : ButtonController
{
    public override void Execute()
    {
        TriggerEventTargetingToggle(ControlledProduct.Id);
    }
}
