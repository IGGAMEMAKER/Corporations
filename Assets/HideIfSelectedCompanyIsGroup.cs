public class HideIfSelectedCompanyIsGroup : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return !SelectedCompany.hasProduct;
    }
}
