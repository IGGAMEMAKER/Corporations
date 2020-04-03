public class RenderOwnersOfCompanyTitle : ParameterView
{
    public override string RenderValue()
    {
        return "Owners of\n" + SelectedCompany.company.Name;
    }
}
