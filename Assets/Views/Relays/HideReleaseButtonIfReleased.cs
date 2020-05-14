public class HideReleaseButtonIfReleased : HideOnSomeCondition
{
    public override bool HideIf() => Flagship.isRelease;
}
