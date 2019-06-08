using Assets.Classes;
using Assets.Utils;
using UnityEngine;

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

    TeamResource GetConceptCost(GameEntity company)
    {
        return ProductDevelopmentUtils.GetDevelopmentCost(company, gameContext);
    }



    void FocusOnIdeas(GameEntity product)
    {
        
        ProductDevelopmentUtils.ToggleDevelopment(gameContext, product.company.Id, DevelopmentFocus.Concept);
    }

    void Print(string action, GameEntity company)
    {
        if (company.isControlledByPlayer)
            Debug.Log($"{action} : {company.company.Name}");
    }
}
