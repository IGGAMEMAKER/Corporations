public abstract class ToggleButtonController : ButtonController
{
    public override void Initialize()
    {
        base.Initialize();

        ViewRender();
    }
    public virtual void ViewRender() { }
}
