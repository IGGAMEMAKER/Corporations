using Assets.Core;

public class RenderCompanyPositioning : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        var c = SelectedCompany;

        if (!c.hasProduct)
            return "-----";

        return Marketing.GetPositioningName(c);
    }
}
