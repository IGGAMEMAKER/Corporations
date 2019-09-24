using Assets.Utils;
using System.Collections.Generic;
using UnityEngine;

public partial class AIProductSystems : OnDateChange
{
    void Survive(GameEntity product)
    {
        // we cannot earn money fast, we need to reduce company maintenance!

        // shrink team
        ShrinkTeam(product);

        // crunch
        Crunch(product);

        // sell more shares if possible
    }

    void ShrinkTeam(GameEntity product)
    {
        Debug.Log("TRY TO SURVIVE: " + product.company.Name);
        Debug.Log("TRY TO SURVIVE: " + product.company.Name);
        Debug.Log("TRY TO SURVIVE: " + product.company.Name);
        Debug.Log("TRY TO SURVIVE: " + product.company.Name);
        Debug.Log("TRY TO SURVIVE: " + product.company.Name);
        Debug.LogError("TRY TO SURVIVE: " + product.company.Name);

        if (FireWorkerByRole(product, WorkerRole.Marketer))
            return;

        FireWorkerByRole(product, WorkerRole.Programmer);
    }
}
