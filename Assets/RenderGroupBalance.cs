using Assets.Core;

public class RenderGroupBalance : ParameterView
{
    public override string RenderValue() => Format.Minify(Economy.BalanceOf(MyCompany));
}
