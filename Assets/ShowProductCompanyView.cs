public class ShowProductCompanyView : View, IMenuListener
{
    public CompanyPreviewView CompanyPreviewView;

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        if (screenMode == ScreenMode.CharacterScreen)
            Render();
    }

    void Render()
    {
        CompanyPreviewView.SetEntity(MyProductEntity);
    }

    // Start is called before the first frame update
    void Start()
    {
        ListenMenuChanges(this);

        Render();
    }
}
