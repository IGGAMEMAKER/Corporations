using Assets.Core;

public class HideMainLinkButton : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return false;
        return !MyCompany.isWantsToExpand; // || !Companies.IsHasDaughters(GameContext, MyCompany) || CurrentIntDate < 180;
    }
}
