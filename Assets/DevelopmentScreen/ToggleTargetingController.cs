using UnityEngine;

public class ToggleTargetingController : ButtonController
    , ITargetingListener
{

    //public override void ButtonStart()
    //{
    //    MyProductEntity.AddTargetingListener(this);

    //    Render();
    //}

    void ITargetingListener.OnTargeting(GameEntity entity)
    {
        Debug.Log("OnTargeting");

        Render();
    }

    void Update()
    {
        Render();
    }

    void Render()
    {
        AddIsChosenComponent(MyProductEntity.isTargeting);
    }

    public override void Execute()
    {
        TriggerEventTargetingToggle(MyProduct.Id);
    }
}
