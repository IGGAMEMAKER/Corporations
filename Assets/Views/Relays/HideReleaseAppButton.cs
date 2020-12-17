using Assets.Core;

public class HideReleaseAppButton : HideOnSomeCondition
{
    public override bool HideIf()
    {
        //var p = SelectedCompany;
        var p = Flagship;
        return !Companies.IsReleaseableApp(p);
    }
}
