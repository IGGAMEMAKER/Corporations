public class HideSphereOfInfluenceTogglerForNiche : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var focus = MyCompany.companyFocus.Niches;

        return focus.Count > 6;
    }
}
