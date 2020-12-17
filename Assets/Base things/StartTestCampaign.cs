using Assets.Core;

public class StartTestCampaign : ButtonController
{
    public override void Execute()
    {
        State.StartNewCampaign(Q, NicheType.ECom_MoneyExchange, "IG Games");
    }
}
