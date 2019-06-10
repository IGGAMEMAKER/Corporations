using Assets.Utils;
using UnityEngine;

public class StealIdeasController : ButtonController
{
    public override void Execute()
    {
        ProductUtils.StealIdeas(MyProductEntity, SelectedCompany, GameContext);
    }
}
