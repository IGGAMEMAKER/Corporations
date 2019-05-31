using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromoteTeamButton : ButtonController
{
    public override void Execute()
    {
        TeamUtils.Promote(MyProductEntity);
    }
}
