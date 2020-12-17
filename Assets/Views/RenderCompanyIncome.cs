using Assets.Core;

public class RenderCompanyIncome : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        return Format.Money(Economy.GetIncome(Q, SelectedCompany));
    }
}
