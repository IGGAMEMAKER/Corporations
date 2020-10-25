using Assets.Core;

public class GroupResources : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        return Format.Minify(Economy.BalanceOf(MyGroupEntity));
    }
}
