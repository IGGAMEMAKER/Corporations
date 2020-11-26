using Assets.Core;

public class ShowMenuLinks : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return Companies.GetDaughters(MyCompany, Q).Length == 0;
    }
}
