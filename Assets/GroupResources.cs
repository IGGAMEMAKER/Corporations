using Assets.Utils;

public class GroupResources : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return Format.Shorten(MyGroupEntity.companyResource.Resources.money);
    }
}
