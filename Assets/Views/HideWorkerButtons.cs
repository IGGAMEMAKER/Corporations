using Assets.Core;

public class HideWorkerButtons : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return false;
    }
}
