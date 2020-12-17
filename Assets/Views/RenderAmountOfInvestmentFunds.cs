using Assets.Core;
using System.Linq;

public class RenderAmountOfInvestmentFunds : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        var funds = Companies.GetInvestmentFundsWhoAreInterestedInMarket(Q, SelectedNiche);

        return $"Funds ({funds.Count()})";
    }
}
