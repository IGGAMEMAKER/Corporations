using Assets.Core;

public class HideNextCompanyButtonIfHaveOneCompany : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var companies = Companies.GetDaughterProductCompanies(GameContext, MyCompany);

        return companies.Length <= 1;
    }
}
