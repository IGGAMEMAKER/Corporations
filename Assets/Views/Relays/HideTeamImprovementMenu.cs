using Assets.Core;

public class HideTeamImprovementMenu : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return !(SelectedCompany.hasProduct && Companies.IsDaughterOf(MyCompany, SelectedCompany));
    }
}
