using Assets.Core;
using System;
using UnityEngine;
using UnityEngine.UI;

public abstract partial class View : BaseClass
{
    public string RenderName(GameEntity company)
    {
        if (Companies.IsDirectlyRelatedToPlayer(Q, company))
            return Visuals.Colorize(company.company.Name, Colors.COLOR_GOLD);
        else
            return Visuals.Colorize(company.company.Name, Colors.COLOR_WHITE);
    }

    public void Refresh()
    {
        ScreenUtils.UpdateScreen(Q);
    }
}