public class ProjectMoraleView : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return SelectedCompany.team.Morale.ToString();
    }
}
