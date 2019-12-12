using Assets.Utils;
using Entitas;
using System;
using UnityEngine;

public partial class WorldFillerSystem : IInitializeSystem
{
    readonly GameContext GameContext;
    bool simulated = false;

    public WorldFillerSystem(Contexts contexts)
    {
        GameContext = contexts.game;
    }

    void IInitializeSystem.Initialize()
    {
        Simulate();
    }

    void MockySimulation(int skipDays)
    {
        var markets = Markets.GetNiches(GameContext);
        var skipMonths = skipDays / 30;

        var date = ScheduleUtils.GetCurrentDate(GameContext);

        Debug.Log("Mocky simulation: simulate market development");

        // simulate market development
        foreach (var m in markets)
        {
            var spawnTime = m.nicheLifecycle.OpenDate;

            var monthsOfWork = (date - spawnTime) / 30;
            var accumulator = monthsOfWork;

            // niche state promotion
            var profile = m.nicheBaseProfile.Profile;

            while (accumulator > 0)
            {
                Debug.Log("while");
                Markets.PromoteNicheState(m);
                accumulator -= Markets.GetNicheDuration(m);
            }



            // filling with companies
            var amountOfCompanies = UnityEngine.Random.Range(0, monthsOfWork / 12);

            for (var i = 0; i < amountOfCompanies; i++)
                Markets.FillMarket(m, GameContext);
        }

        Debug.Log("Mocky simulation: simulate product development");
        // simulate products

        var products = Companies.GetProductCompanies(GameContext);
        foreach (var p in products)
        {
            var niche = Markets.GetNiche(GameContext, p.product.Niche);
            var spawnTime = niche.nicheLifecycle.OpenDate;

            var monthsOfWork = (date - spawnTime) / 30;
            //Debug.Log($"Market={p.product.Niche}  Date: " + date + " openDate " + spawnTime + "  monthsOfWork = " + monthsOfWork);

            if (monthsOfWork < 0)
                monthsOfWork = 0;

            // set concepts
            var iterationTime = Products.GetBaseIterationTime(niche);

            var concept = monthsOfWork * 30 / iterationTime;
            var randConcept = 1 + UnityEngine.Random.Range(0, concept);

            for (var i = 0; i < randConcept; i++)
                Products.UpdgradeProduct(p, GameContext, true);

            // set brands
            // commented, because UpgradeProduct already adds brand powers
            //var brand = Mathf.Clamp(UnityEngine.Random.Range(0, monthsOfWork / 2), 0, 35);
            //MarketingUtils.AddBrandPower(p, brand);

            // set clients
            var flow = (float) MarketingUtils.GetClientFlow(GameContext, p.product.Niche);
            //var clients = monthsOfWork * flow * UnityEngine.Random.Range(0.5f, 1.5f);
            var growth = 1.03f;
            var clients = MarketingUtils.GetClients(p) * Mathf.Pow(growth, monthsOfWork);

            MarketingUtils.AddClients(p, (long)clients);
        }
    }

    void Simulate()
    {
        if (!simulated)
            SimulateDevelopment();
        simulated = true;
    }

    void SimulateDevelopment()
    {
        Debug.Log("Simulate Development");
        var skipDays = (Constants.START_YEAR - 1991) * 360;

        Debug.Log("Skip days = " + skipDays);
        //ScheduleUtils.ResumeGame(GameContext, skipDays, 50000);


        //var incr = 1;

        //for (var i = 0; i < skipDays; i += incr)
        //    ScheduleUtils.IncreaseDate(GameContext, incr);


        MockySimulation(skipDays);

        Debug.Log("Simulation done");
    }
}
