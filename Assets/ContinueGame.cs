using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueGame : ButtonController
{
    public override void Execute()
    {
        State.LoadGameData(Q);
        State.LoadGameScene();
    }
}
