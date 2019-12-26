using Assets.Core;

public class RenderInnovationChanceDescription : ParameterView
{
    public override string RenderValue()
    {
        if (!SelectedCompany.hasProduct)
            return "";

        if (Companies.IsExploredCompany(GameContext, SelectedCompany))
            return Products.GetInnovationChanceBonus(SelectedCompany, GameContext).ToString();

        return "???";
    }
}
