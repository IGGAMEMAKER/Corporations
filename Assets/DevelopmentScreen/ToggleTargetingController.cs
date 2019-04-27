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
        if (MyProductEntity.isTargeting)
        {
            if (gameObject.GetComponent<IsChosenComponent>() == null)
                gameObject.AddComponent<IsChosenComponent>();
        }
        else
        {
            var c = gameObject.GetComponent<IsChosenComponent>();

            if (c != null)
                Destroy(c);
        }
    }

    public override void Execute()
    {
        TriggerEventTargetingToggle(MyProduct.Id);
    }
}
