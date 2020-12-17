using Assets.Core;

public class RenderStatsOfCompany : ParameterView
{
    public override string RenderValue()
    {
        return Visuals.Link($"Stats of company {Flagship.company.Name}");
    }
}
