using Assets.Core;

public class RenderGroupBalance : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue() {
        //return "Cash\n" + Visuals.Colorize(Format.Money(Economy.BalanceOf(MyCompany)), profitable);
        return Format.Minify(Economy.BalanceOf(MyCompany));
        //return "Cash\n" + Format.Minify(Economy.BalanceOf(MyCompany));
    }
}
