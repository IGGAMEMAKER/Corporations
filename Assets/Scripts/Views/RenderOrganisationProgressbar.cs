public class RenderOrganisationProgressbar : ParameterView
{
    public override string RenderValue()
    {
        var organisation = SelectedCompany.team.Organisation;

        return organisation.ToString();
    }
}
