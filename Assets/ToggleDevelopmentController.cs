using Assets.Utils;
using UnityEngine.UI;

public class ToggleDevelopmentController : ButtonController
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
    }

    public override void RareUpdate()
    {
        base.RareUpdate();

        Render();
    }

    void Render()
    {
        AddIsChosenComponent(MyProductEntity.developmentFocus.Focus == DevelopmentFocus);
    }
}
