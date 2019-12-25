using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleRoleButtons : ButtonController
{
    public AddRoleButtons AddRoleButtons;

    public override void Execute()
    {
        AddRoleButtons.ToggleButtons();
    }
}
