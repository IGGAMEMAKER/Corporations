using Assets.Core;

public class ChooseTargetAudienceButtonController : ButtonController
{
    public override void Execute()
    {
        var company = Flagship;

        var positioning = FindObjectOfType<PositioningManagerView>().Positioning;

        Marketing.ChangePositioning(company, Q, positioning.ID);
    }
}
