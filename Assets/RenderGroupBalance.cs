using Assets.Core;

public class RenderGroupBalance : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue() => Format.Minify(Economy.BalanceOf(MyCompany));
}
