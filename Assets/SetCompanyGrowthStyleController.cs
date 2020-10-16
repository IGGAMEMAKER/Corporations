using Assets.Core;

public class SetCompanyGrowthStyleController : ButtonController
{
    public CompanyGrowthStyle CompanyGrowthStyle;

    public override void Execute()
    {
        Investments.SetGrowthStyle(MyCompany, CompanyGrowthStyle);
    }
}
