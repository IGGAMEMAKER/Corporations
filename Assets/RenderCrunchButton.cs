﻿public class RenderCrunchButton : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        ToggleIsChosenComponent(MyProductEntity.isCrunching);
    }
}
