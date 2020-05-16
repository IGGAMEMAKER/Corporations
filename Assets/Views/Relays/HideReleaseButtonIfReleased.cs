using Assets.Core;

public class HideReleaseButtonIfReleased : HideOnSomeCondition
{
    public override bool HideIf() => !Companies.IsReleaseableApp(Flagship, Q);
}
