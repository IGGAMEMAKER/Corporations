using Assets.Core;

public class HideFlagshipLabelIfOnlyOneCompany : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return Companies.GetDaughtersAmount(MyCompany, Q) == 0;
    }
}
