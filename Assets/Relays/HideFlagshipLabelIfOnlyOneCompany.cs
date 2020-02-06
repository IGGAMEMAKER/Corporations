using Assets.Core;

public class HideFlagshipLabelIfOnlyOneCompany : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return Companies.GetDaughterCompaniesAmount(MyCompany, Q) < 2;
    }
}
