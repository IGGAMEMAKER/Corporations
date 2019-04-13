using Assets.Utils;

public class GroupResources : ParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return ValueFormatter.Shorten(MyGroupEntity.companyResource.Resources.money);
    }
}
