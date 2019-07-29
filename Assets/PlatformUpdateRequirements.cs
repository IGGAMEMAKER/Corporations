using Assets.Utils;

public class PlatformUpdateRequirements : View
    , IAnyDateListener
{
    public ProgressBar IdeaProgressBar;
    public ProgressBar ProgrammingProgressBar;

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }

    void OnEnable()
    {
        LazyUpdate(this);

        Render();
    }

    void Render()
    {
        var devCost = ProductUtils.GetDevelopmentCost(MyProductEntity, GameContext);
        var have = MyProductEntity.companyResource.Resources;

        IdeaProgressBar.SetValue(have.ideaPoints, devCost.ideaPoints);

        ProgrammingProgressBar.SetValue(have.programmingPoints, devCost.programmingPoints);
    }
}
