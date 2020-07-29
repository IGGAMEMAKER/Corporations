public abstract class ButtonView : ButtonController
{
    public override void Initialize()
    {
        base.Initialize();

        ViewRender();
    }
    public virtual void ViewRender() { }
}
