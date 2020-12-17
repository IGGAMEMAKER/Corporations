using Assets.Core;
using System.Linq;

public class FeatureCapView : UpgradedParameterView
{
    public override string RenderHint()
    {
        var product = Flagship;

        var cap = Teams.GetMaxFeatureRatingCap(product, Q);

        var bestTeam = product.team.Teams
                .OrderByDescending(t => Teams.GetFeatureRatingCap(product, t, Q).Sum())
                .First();

        return $"Our best team {bestTeam.Name} gives {cap.Sum()}lvl\n{cap.ToString()}";
    }

    public override string RenderValue()
    {
        var cap = Teams.GetMaxFeatureRatingCap(Flagship, Q).Sum();

        Colorize((int)(cap * 10), 0, 100);

        return cap.ToString("0.0") + "LV";
    }
}
