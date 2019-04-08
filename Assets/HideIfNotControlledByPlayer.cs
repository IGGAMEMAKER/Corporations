using UnityEngine;

public class HideIfNotControlledByPlayer : View, IMenuListener
{
    public GameObject target;

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        if (screenMode == ScreenMode.BusinessScreen)
            Render();
    }

    private void Start()
    {
        ListenMenuChanges(this);

        Render();
    }

    void Render()
    {
        //Debug.Log("HideIfNotControlledByPlayer: selected=" + SelectedCompany.company.Name + " product=" + MyProductEntity.company.Name + " group=" + MyGroupEntity?.company.Name);

        bool visible = (SelectedCompany == MyGroupEntity || SelectedCompany == MyProductEntity);

        target.SetActive(visible);
    }
}
