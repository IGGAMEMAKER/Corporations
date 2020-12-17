using Assets.Core;

public class AppQualityView : UpgradedParameterView
{
    public override string RenderHint()
    {
        return Marketing.GetPositioningQuality(Flagship).ToString();
    }

    public override string RenderValue()
    {
        return Visuals.Positive(Marketing.GetAppQuality(Flagship).ToString("+0.0"));
    }
}
