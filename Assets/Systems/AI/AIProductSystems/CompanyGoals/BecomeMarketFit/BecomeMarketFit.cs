using Assets.Utils;
using Assets.Utils.Tutorial;

public partial class AIProductSystems : OnDateChange
{
    void BecomeMarketFit(GameEntity company)
    {
        ManageTeam(company);

        Crunch(company);

        FocusOnIdeas(company);

        ImproveConcept(company);

        if (GetMarketDifference(company) == 0)
            ImproveSegments(company);

        GrabTestClients(company);
    }

    void ImproveConcept(GameEntity company)
    {

    }

    void GrabTestClients(GameEntity company)
    {
        if (company.isControlledByPlayer && !TutorialUtils.IsOpenedFunctionality(gameContext, TutorialFunctionality.FirstAdCampaign))
            return;

        Print("Start test campaign", company);

        MarketingUtils.StartTestCampaign(gameContext, company);
    }

    int GetDate()
    {
        return ScheduleUtils.GetCurrentDate(gameContext);
    }

    void Crunch(GameEntity product)
    {
        //if (!product.isCrunching)
        //    TeamUtils.ToggleCrunching(gameContext, product.company.Id);
    }

    void FocusOnIdeas(GameEntity product)
    {
        ProductDevelopmentUtils.ToggleDevelopment(gameContext, product.company.Id, DevelopmentFocus.Concept);

        // Steal ideas
    }
}
