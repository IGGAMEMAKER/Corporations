public class ShowGroupCompanyView : View, IMenuListener
{
    public CompanyPreviewView CompanyPreviewView;

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        if (screenMode == ScreenMode.CharacterScreen)
            Render();
    }

    void Render()
    {
        // TODO replace myProductEntity by company group
        CompanyPreviewView.SetEntity(myProductEntity);
    }

    // Start is called before the first frame update
    void Start()
    {
        ListenMenuChanges(this);

        Render();
    }
}
