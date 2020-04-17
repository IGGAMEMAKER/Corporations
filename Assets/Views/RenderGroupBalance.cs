using Assets.Core;

public class RenderGroupBalance : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue() {
        //return "Cash\n" + Visuals.Colorize(Format.Money(Economy.BalanceOf(MyCompany)), profitable);
        return "Cash\n" + Format.Money(Economy.BalanceOf(MyCompany));
    }
}
