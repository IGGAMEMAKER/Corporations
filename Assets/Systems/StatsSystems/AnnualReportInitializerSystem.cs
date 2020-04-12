using Entitas;
using System.Collections.Generic;

class AnnualReportInitializerSystem : IInitializeSystem
{
    public readonly GameContext gameContext;

    public AnnualReportInitializerSystem(Contexts contexts)
    {
        gameContext = contexts.game;
    }

    void IInitializeSystem.Initialize()
    {
        var e = gameContext.CreateEntity();

        e.AddReports(new List<AnnualReport>());
    }

}
