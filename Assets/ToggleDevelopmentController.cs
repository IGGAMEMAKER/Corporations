using Assets.Utils;
using UnityEngine;
using UnityEngine.UI;

public class ToggleDevelopmentController : ButtonController
    , IDevelopmentFocusListener
{
    public DevelopmentFocus DevelopmentFocus;

    public Text Text;

    public override void Execute()
    {
        Debug.Log("ToggleDevelopmentController nothing to Execute");
        //ProductDevelopmentUtils.ToggleDevelopment(GameContext, MyProductEntity.company.Id, DevelopmentFocus);
    }

    public void OnEnable()
    {
        Render();

        MyProductEntity.AddDevelopmentFocusListener(this);
    }

    void IDevelopmentFocusListener.OnDevelopmentFocus(GameEntity entity, DevelopmentFocus focus)
    {
        Render();
    }

    void Render()
    {
        ToggleIsChosenComponent(MyProductEntity.developmentFocus.Focus == DevelopmentFocus);
    }
}
