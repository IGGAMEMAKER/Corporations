using Assets.Utils;

public class HideTeamImprovementMenu : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return !(SelectedCompany.hasProduct && CompanyUtils.IsDaughterOfCompany(MyCompany, SelectedCompany));
    }
}
