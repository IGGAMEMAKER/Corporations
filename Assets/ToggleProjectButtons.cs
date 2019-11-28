using Assets.Utils;

public class ToggleProjectButtons : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var exploredCompany = SelectedCompany.hasResearch || CompanyUtils.IsCompanyRelatedToPlayer(GameContext, SelectedCompany);

        return !exploredCompany;
    }
}
