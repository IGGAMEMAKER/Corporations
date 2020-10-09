using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualExit : ButtonController
{
    public override void Execute()
    {
        Application.Quit();
    }
}
