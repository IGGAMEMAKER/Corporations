using Assets.Classes;
using Assets.Utils;
using UnityEngine;

public partial class AIProductSystems : OnDateChange
{
    void BecomeMarketFit(GameEntity company)
    {
        ManageTeam(company);

        FocusOnIdeas(company);

        ImproveConcept(company);

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


    TeamResource GetResourceChange(GameEntity company)
    {
        return CompanyEconomyUtils.GetResourceChange(company, gameContext);
    }

    TeamResource GetConceptCost(GameEntity company)
    {
        return ProductDevelopmentUtils.GetDevelopmentCost(company, gameContext);
    }

    void Print(string action, GameEntity company)
    {
        if (!company.isControlledByPlayer)
            Debug.Log($"{action} : {company.company.Name}");
    }
}
