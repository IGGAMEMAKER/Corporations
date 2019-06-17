using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HireWorker : ButtonController
{
    public override void Execute()
    {
        HumanUtils.Recruit(SelectedHuman, MyProductEntity);
    }
}
