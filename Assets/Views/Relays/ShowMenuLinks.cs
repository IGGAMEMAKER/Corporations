using Assets.Core;

public class ShowMenuLinks : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return Companies.GetDaughters(Q, MyCompany).Length == 0;
    }
}
