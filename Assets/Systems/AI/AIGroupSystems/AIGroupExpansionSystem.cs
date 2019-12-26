using System.Collections.Generic;
using Assets.Core;


public partial class AIGroupExpansionSystem : OnQuarterChange
{
    public AIGroupExpansionSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var c in Companies.GetAIManagingCompanies(gameContext))
            ManageGroup(c);
    }

    void ManageGroup(GameEntity group)
    {
        ExpandSphereOfInfluence(group);
        FillUnoccupiedMarkets(group);
    }
}
