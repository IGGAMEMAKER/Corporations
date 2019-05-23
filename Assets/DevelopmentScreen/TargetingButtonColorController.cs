public class TargetingButtonColorController : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        ToggleIsChosenComponent(MyProductEntity.isTargeting);
    }
}
