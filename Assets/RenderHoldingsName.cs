using Assets.Utils;

public class RenderHoldingsName : ParameterView
{
    public override string RenderValue()
    {
        bool isFinancialStructure = Companies.IsFinancialStructure(SelectedCompany);

        if (isFinancialStructure)
            return "They invest in";

        bool isRelatedToPlayer = Companies.IsRelatedToPlayer(GameContext, SelectedCompany);

        if (isRelatedToPlayer)
            return "Our holdings";
        else
            return "Their holdings";
    }
}
