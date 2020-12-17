using Assets.Core;

public class DrawConceptProgressLinear : View
{
    public ProgressBar ProgressBar;

    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship;

        var p = getProgress(company);

        ProgressBar.SetValue(p);
        ProgressBar.SetDescription($"Upgrading to {Products.GetProductLevel(company)}LVL");
    }

    float getProgress(GameEntity company)
    {
        return 0;
    }
}
