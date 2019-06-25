public class TargetingButtonColorController : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        if (HasProductCompany)
            ToggleIsChosenComponent(MyProductEntity.isTargeting);
    }
}
