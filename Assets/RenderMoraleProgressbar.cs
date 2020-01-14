public class RenderMoraleProgressbar : ParameterView
{
    public override string RenderValue()
    {
        var morale = SelectedCompany.team.Morale;

        return morale.ToString();
    }
}
