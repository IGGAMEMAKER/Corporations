using Assets.Utils;
using UnityEngine;

public class StealIdeasController : ButtonController
{
    public override void Execute()
    {
        Debug.Log("Steal ideas!");

        ProductUtils.StealIdeas(MyProductEntity, SelectedCompany, GameContext);
    }
}
