using Assets.Utils;
using System.Collections.Generic;

public partial class AIProductSystems : OnDateChange
{
    void Survive(GameEntity product)
    {
        // we cannot earn money fast, we need to reduce company maintenance!

        // shrink team
        ShrinkTeam(product);

        // increase prices
        IncreasePrices(product);

        // crunch
        Crunch(product);

        // sell more shares if possible
    }

    void FindMostExpensiveWorker(Dictionary<int, WorkerRole> workers)
    {
        
    }

    void ShrinkTeam(GameEntity product)
    {
        if (FireWorkerByRole(product, WorkerRole.Marketer))
            return;

        FireWorkerByRole(product, WorkerRole.Programmer);
    }
}
