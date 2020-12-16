using Assets.Core;

public class RenderPotentialMarketLeader : ParameterView
{
    public override string RenderValue()
    {
        if (!Markets.IsExploredMarket(Q, SelectedNiche))
            return "???";

        var potentialLeader = Markets.GetPotentialMarketLeader(Q, SelectedNiche);

        if (potentialLeader == null)
            return "";

        var chances = Products.GetInnovationChance(potentialLeader, Q);

        var isRelatedToPlayer = Companies.IsDirectlyRelatedToPlayer(Q, potentialLeader);

        var colorName = isRelatedToPlayer ? Colors.COLOR_CONTROL : Colors.COLOR_CONTROL_NO;

        Colorize(colorName);

        return $"<b>{potentialLeader.company.Name}</b>"; // "\n\nInnovation chances: {chances}%";
    }
}
