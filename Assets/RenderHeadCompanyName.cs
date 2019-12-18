using Assets.Utils;

public class RenderHeadCompanyName : ParameterView
{
    public override string RenderValue()
    {
        return "Head company: " + Visuals.Link(MyCompany.company.Name);
    }
}
