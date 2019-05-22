public class RenderFocusSegmentButtonView : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var UserType = GetComponent<SetTargetUserType>().UserType;

        ToggleIsChosenComponent(MyProductEntity.targetUserType.UserType == UserType);
    }
}
