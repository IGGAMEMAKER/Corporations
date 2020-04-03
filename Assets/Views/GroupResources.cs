using Assets.Core;

public class GroupResources : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return Format.Minify(MyGroupEntity.companyResource.Resources.money);
    }
}
