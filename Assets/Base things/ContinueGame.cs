using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueGame : ButtonController
{
    public GameObject[] Buttons;

    public override void Execute()
    {
        foreach (var b in Buttons)
        {
            b.SetActive(false);
        }

        State.LoadGameData(Q);
        State.LoadGameScene();
    }
}
