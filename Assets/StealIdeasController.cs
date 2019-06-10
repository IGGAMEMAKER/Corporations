using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealIdeasController : ButtonController
{
    public override void Execute()
    {
        Debug.Log("Steal ideas!");

        ProductUtils.StealIdeas(MyProductEntity, SelectedCompany, GameContext);
    }
}
