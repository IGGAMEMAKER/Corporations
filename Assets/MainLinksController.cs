using UnityEngine;

public class MainLinksController : View
    , IMenuListener
{
    public GameObject LinkToDevelopment;
    public GameObject LinkToGroup;

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        Render();
    }

    void Render()
    {
        //LinkToDevelopment.SetActive(MyProductEntity != null);
        //LinkToGroup.SetActive(MyGroupEntity != null);
    }

    // Start is called before the first frame update
    void Start()
    {
        ListenMenuChanges(this);

        Render();
    }
}
