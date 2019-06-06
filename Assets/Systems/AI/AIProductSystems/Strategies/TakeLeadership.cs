using Assets.Utils;

public partial class AIProductSystems : OnDateChange
{
    void TakeLeadership(GameEntity product)
    {
        // focus on ideas
        FocusOnIdeas(product);

        // crunch
        Crunch(product);
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
