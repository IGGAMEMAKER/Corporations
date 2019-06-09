using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    void BecomeMarketFit(GameEntity company)
    {
        ManageTeam(company);

        FocusOnIdeas(company);

        ImproveConcept(company);

        ImproveSegments(company);

        Crunch(company);
    }

    void ImproveConcept(GameEntity company)
    {

    }

    void Crunch(GameEntity product)
    {
        if (!product.isCrunching)
            TeamUtils.ToggleCrunching(gameContext, product.company.Id);
    }

    void FocusOnIdeas(GameEntity product)
    {
        ProductDevelopmentUtils.ToggleDevelopment(gameContext, product.company.Id, DevelopmentFocus.Concept);
    }
}
