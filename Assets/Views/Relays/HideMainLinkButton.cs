using Assets.Core;

public class HideMainLinkButton : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return !Companies.IsHasDaughters(Q, MyCompany);
        //return !MyCompany.isWantsToExpand; //  || CurrentIntDate < 180;
    }
}
