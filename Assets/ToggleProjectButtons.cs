using Assets.Utils;

public class ToggleProjectButtons : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var exploredCompany = SelectedCompany.hasResearch || Companies.IsRelatedToPlayer(GameContext, SelectedCompany);

        return !exploredCompany;
    }
}
