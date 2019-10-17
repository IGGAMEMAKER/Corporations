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
        var markets = NicheUtils.GetNiches(GameContext);
        var skipMonths = skipDays / 30;

        var date = ScheduleUtils.GetCurrentDate(GameContext);

        // simulate market development
        foreach (var m in markets)
        {
            var spawnTime = m.nicheLifecycle.OpenDate;

            var monthsOfWork = (date - spawnTime) / 30;
            var accumulator = monthsOfWork;

            // niche state promotion
            var profile = m.nicheBaseProfile.Profile;
            var time = (int)m.nicheLifecycle.Period;

            while (accumulator > 0)
            {
                NicheUtils.PromoteNicheState(m);
                accumulator -= NicheUtils.GetNicheDuration(m);
            }



            // filling with companies
            var amountOfCompanies = UnityEngine.Random.Range(0, monthsOfWork / 12);

            for (var i = 0; i < amountOfCompanies; i++)
                NicheUtils.FillMarket(m, GameContext);
        }

        // simulate products

        var products = CompanyUtils.GetProductCompanies(GameContext);
        foreach (var p in products)
        {
            var niche = NicheUtils.GetNicheEntity(GameContext, p.product.Niche);
            var spawnTime = niche.nicheLifecycle.OpenDate;

            var monthsOfWork = (date - spawnTime) / 30;
            //Debug.Log($"Market={p.product.Niche}  Date: " + date + " openDate " + spawnTime + "  monthsOfWork = " + monthsOfWork);

            if (monthsOfWork < 0)
                monthsOfWork = 0;

            // set concepts
            var iterationTime = ProductUtils.GetBaseIterationTime(niche);

            var concept = monthsOfWork * 30 / iterationTime;
            var randConcept = 1 + UnityEngine.Random.Range(0, concept);

            for (var i = 0; i < randConcept; i++)
                ProductUtils.UpdgradeProduct(p, GameContext, true);

            // set brands
            // commented, because UpgradeProduct already adds brand powers
            //var brand = Mathf.Clamp(UnityEngine.Random.Range(0, monthsOfWork / 2), 0, 35);
            //MarketingUtils.AddBrandPower(p, brand);

            // set clients
            var flow = (float) MarketingUtils.GetCurrentClientFlow(GameContext, p.product.Niche);
            var clients = monthsOfWork * flow * UnityEngine.Random.Range(0.5f, 1.5f);

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
