using UnityEngine;

public abstract class ShowControlledCompany : View, IMenuListener
{
    public CompanyPreviewView CompanyPreviewView;
    public GameObject LeaveCEOButton;

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        if (screenMode == ScreenMode.CharacterScreen)
            Render();
    }

    public abstract GameEntity GetControlledEntity();

    void Render()
    {
        var e = GetControlledEntity();

        bool visible = e != null;

        CompanyPreviewView.gameObject.SetActive(visible);
        LeaveCEOButton.SetActive(visible);

        if (visible)
            CompanyPreviewView.SetEntity(e);
    }

    // Start is called before the first frame update
    void Start()
    {
        ListenMenuChanges(this);

        Render();
    }
}
