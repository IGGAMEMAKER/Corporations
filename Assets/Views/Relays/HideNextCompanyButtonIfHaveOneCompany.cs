using Assets.Core;

public class HideNextCompanyButtonIfHaveOneCompany : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var companies = Companies.GetDaughterProducts(Q, MyCompany);

        return companies.Length <= 1;
    }
}
