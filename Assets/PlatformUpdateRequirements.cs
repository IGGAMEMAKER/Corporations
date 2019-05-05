using Assets.Utils;

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
        var have = MyProductEntity.companyResource.Resources;

        IdeaProgressBar.SetValue(have.ideaPoints, devCost.ideaPoints);

        ProgrammingProgressBar.SetValue(have.programmingPoints, devCost.programmingPoints);
    }
}
