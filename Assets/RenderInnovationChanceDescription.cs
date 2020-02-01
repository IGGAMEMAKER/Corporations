using Assets.Core;

public class RenderInnovationChanceDescription : ParameterView
{
    public override string RenderValue()
    {
        if (!SelectedCompany.hasProduct)
            return "";

        if (Companies.IsExploredCompany(Q, SelectedCompany))
            return Products.GetInnovationChanceBonus(SelectedCompany, Q).ToString();

        return "???";
    }
}
