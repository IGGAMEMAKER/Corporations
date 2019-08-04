public class HideNextMarketButtonIfUnnecessary : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var focus = MyCompany.companyFocus.Niches;

        return focus.Count <= 1 || !focus.Contains(SelectedNiche);
    }
}
