using Assets.Core;

public class RenderGroupBalance : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue() {
        var profitable = Economy.IsProfitable(Q, MyCompany);
        //Colorize(profitable, Colors.COLOR_POSITIVE, Colors.COLOR_NEGATIVE);

        return "Cash\n" + Visuals.Colorize(Format.Minify(Economy.BalanceOf(MyCompany)), profitable);
    }
}
