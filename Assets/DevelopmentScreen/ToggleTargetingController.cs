public class ToggleTargetingController : ButtonController
    , ITargetingListener
{
    void OnEnable()
    {
        ListenProductChanges().AddTargetingListener(this);

        Render();
    }

    void Render()
    {
        ToggleIsChosenComponent(MyProductEntity.isTargeting);
    }

    public override void Execute()
    {
        TriggerEventTargetingToggle(MyProduct.Id);
    }

    void ITargetingListener.OnTargeting(GameEntity entity)
    {
        Render();
    }
}
