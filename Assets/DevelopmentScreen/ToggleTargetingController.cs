public class ToggleTargetingController : ButtonController
{
    public override void RareUpdate()
    {
        base.RareUpdate();

        Render();
    }

    void Render()
    {
        AddIsChosenComponent(MyProductEntity.isTargeting);
    }

    public override void Execute()
    {
        TriggerEventTargetingToggle(MyProduct.Id);
    }
}
