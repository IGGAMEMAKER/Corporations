using Assets.Core;
using System.Linq;

public class ShowTheInnovatorOnMarket : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var players = Markets.GetProductsOnMarket(Q, SelectedNiche);

        var productCompany = players.FirstOrDefault(p => p.isTechnologyLeader);

        if (productCompany == null)
            return "Noone has the decisive advantage in concept";

        return $"{productCompany.company.Name} ({Products.GetProductLevel(productCompany)}LVL) \nThis gives them +1 Brand each month";
    }
}
