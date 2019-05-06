using Assets.Utils;
using UnityEngine.UI;

public class ToggleDevelopmentController : ButtonController
    , IDevelopmentFocusListener
{
    public DevelopmentFocus DevelopmentFocus;

    public Text Text;

    public override void Execute()
    {
        ProductDevelopmentUtils.ToggleDevelopment(GameContext, MyProductEntity.company.Id, DevelopmentFocus);
    }

    public void OnEnable()
    {
        Render();

        ListenProductChanges().AddDevelopmentFocusListener(this);
    }

    void IDevelopmentFocusListener.OnDevelopmentFocus(GameEntity entity, DevelopmentFocus focus)
    {
        Render();
    }

    void Render()
    {
        AddIsChosenComponent(MyProductEntity.developmentFocus.Focus == DevelopmentFocus);
    }
}
