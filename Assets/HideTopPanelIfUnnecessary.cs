using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTopPanelIfUnnecessary : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var screen = CurrentScreen;

        var show = screen == ScreenMode.NicheScreen || screen == ScreenMode.GroupManagementScreen;

        return !show;
    }
}
