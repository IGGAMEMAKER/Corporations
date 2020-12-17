using Assets.Core;

public class RenderInnovationChance : UpgradedParameterView
{
    public override string RenderHint()
    {
        if (!SelectedCompany.hasProduct)
            return "";

        return Products.GetInnovationChanceBonus(SelectedCompany, Q).ToString();
    }

    public override string RenderValue()
    {
        if (!SelectedCompany.hasProduct)
            return "";

        var chance = Products.GetInnovationChance(SelectedCompany, Q);

        return chance + "%";
    }
}
