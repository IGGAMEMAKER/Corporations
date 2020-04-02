public class HideInterestsIfNotProductCompany : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return !SelectedCompany.hasCompanyFocus || SelectedCompany.hasProduct;
    }
}
