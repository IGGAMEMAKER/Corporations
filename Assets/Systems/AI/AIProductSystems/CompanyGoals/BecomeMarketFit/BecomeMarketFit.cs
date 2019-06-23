using Assets.Utils;
using Assets.Utils.Tutorial;

public partial class AIProductSystems : OnDateChange
{
    void BecomeMarketFit(GameEntity company)
    {
        ManageTeam(company);

        Crunch(company);

        ImproveSegments(company);

        GrabTestClients(company);
    }

    void GrabTestClients(GameEntity company)
    {
        if (company.isControlledByPlayer && !TutorialUtils.IsOpenedFunctionality(gameContext, TutorialFunctionality.FirstAdCampaign))
            return;

        Print("Start test campaign", company);

        MarketingUtils.StartTestCampaign(gameContext, company);
    }
}
