public class HideIfNotGroupCompany : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var companyType = SelectedCompany.company.CompanyType;
        return companyType == CompanyType.FinancialGroup || companyType == CompanyType.ProductCompany;
    }
}
