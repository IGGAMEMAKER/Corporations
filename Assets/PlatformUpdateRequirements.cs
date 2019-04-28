using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformUpdateRequirements : View
{
    public ProgressBar IdeaProgressBar;
    public ProgressBar ProgrammingProgressBar;

    void Update()
    {
        Render();
    }

    void Render()
    {
        var devCost = ProductDevelopmentUtils.GetDevelopmentCost(MyProductEntity, GameContext);
        var have = MyProductEntity.companyResource;

        IdeaProgressBar.SetValue(have.Resources.ideaPoints, devCost.ideaPoints);

        ProgrammingProgressBar.SetValue(have.Resources.programmingPoints, devCost.programmingPoints);
    }
}
