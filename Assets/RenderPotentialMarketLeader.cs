using Assets.Core;

public class RenderPotentialMarketLeader : ParameterView
{
    public override string RenderValue()
    {
        if (!Markets.IsExploredMarket(GameContext, SelectedNiche))
            return "???";

        var potentialLeader = Markets.GetPotentialMarketLeader(GameContext, SelectedNiche);

        if (potentialLeader == null)
            return "";

        var chances = Products.GetInnovationChance(potentialLeader, GameContext);

        var isRelatedToPlayer = Companies.IsRelatedToPlayer(GameContext, potentialLeader);

        var colorName = isRelatedToPlayer ? Colors.COLOR_CONTROL : Colors.COLOR_CONTROL_NO;

        Colorize(colorName);

        return $"<b>{potentialLeader.company.Name}</b>"; // "\n\nInnovation chances: {chances}%";
    }
}
