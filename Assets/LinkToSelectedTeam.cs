using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkToSelectedTeam : ButtonController
{
    public override void Execute()
    {
        Navigate(ScreenMode.TeamScreen);
    }
}
