public class HideSphereOfInfluenceTogglerForNiche : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return false;
        var focus = MyCompany.companyFocus.Niches;

        return focus.Count > 6;
    }
}
