using Assets.Core;

public class RenderInnovationChanceDescription : ParameterView
{
    public override string RenderValue()
    {
        if (!SelectedCompany.hasProduct)
            return "";

        return "???";
    }
}
