using Assets.Utils;

public class ShowMenuLinks : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return Companies.GetDaughterCompanies(GameContext, MyCompany.company.Id).Length == 0;
    }
}
