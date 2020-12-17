using Assets.Core;

public class RenderHumanAmbitions : ParameterView
{
    public override string RenderValue()
    {
        var ambition = Humans.GetAmbition(Q, SelectedHuman.human.Id);

        switch (ambition)
        {
            case Ambition.EarnMoney:
                return "Wants to make money";

            //case Ambition.RuleProductCompany:
            //    return "Make innovative products";

            case Ambition.RuleCorporation:
                return "Wants to rule the corporation";

            default:
                return ambition.ToString();
        }
    }
}
