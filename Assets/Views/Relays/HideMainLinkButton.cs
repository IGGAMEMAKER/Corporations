using Assets.Core;

public class HideMainLinkButton : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return !Companies.IsHasDaughters(MyCompany);
        //return !MyCompany.isWantsToExpand; //  || CurrentIntDate < 180;
    }
}
