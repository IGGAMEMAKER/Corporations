using Assets.Core;

public class HideMainLinkButton : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return !MyCompany.isWantsToExpand; // || !Companies.IsHasDaughters(GameContext, MyCompany) || CurrentIntDate < 180;
    }
}
