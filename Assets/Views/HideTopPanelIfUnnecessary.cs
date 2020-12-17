﻿public class HideTopPanelIfUnnecessary : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var screen = CurrentScreen;

        // screen == ScreenMode.NicheScreen ||
        var show = screen == ScreenMode.GroupManagementScreen;

        return !show;
    }
}
