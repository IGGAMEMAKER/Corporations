public class ToggleTargetingController : ButtonController
{
    public void OnEnable()
    {
        Render();
    }

    void Render()
    {
        AddIsChosenComponent(MyProductEntity.isTargeting);
    }

    public override void Execute()
    {
        TriggerEventTargetingToggle(MyProduct.Id);

        Render();
    }
}
