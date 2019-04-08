using UnityEngine;

public class HideIfNotControlledByPlayer : View, IMenuListener
{
    public GameObject target;

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        if (screenMode == ScreenMode.BusinessScreen)
            Render();
    }

    private void Awake()
    {
        ListenMenuChanges(this);

        Render();
    }

    void Render()
    {
        bool visible = (SelectedCompany == MyGroupEntity || SelectedCompany == MyProductEntity);

        target.SetActive(visible);
    }
}
