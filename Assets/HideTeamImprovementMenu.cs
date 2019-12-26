using Assets.Core;

public class HideTeamImprovementMenu : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return !(SelectedCompany.hasProduct && Companies.IsDaughterOfCompany(MyCompany, SelectedCompany));
    }
}
