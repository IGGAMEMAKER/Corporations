using UnityEngine;

public abstract class ShowControlledCompany : View, IMenuListener
{
    public CompanyPreviewView CompanyPreviewView;

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

        if (visible)
            CompanyPreviewView.SetEntity(e);
    }

    void Start()
    {
        ListenMenuChanges(this);

        Render();
    }
}
