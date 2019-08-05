using Assets.Utils;
using Entitas;
using System.Collections.Generic;
using UnityEngine;

class SpawnFundsSystem : OnMonthChange
{
    public SpawnFundsSystem(Contexts contexts) : base(contexts)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        GameEntity[] niches = NicheUtils.GetPlayableNiches(gameContext);
            //contexts.game.GetEntities(GameMatcher.AllOf(GameMatcher.Niche));

        foreach (var e in niches)
        {
            var nicheRating = NicheUtils.GetMarketRating(e);

            var potential = NicheUtils.GetMarketPotential(e);

            var profileInvestors = NicheUtils.GetInstitutionalInvestors(gameContext, e);

            var playersOnMarket = profileInvestors.Length;

            var potentialRating = Mathf.Log10(potential) - 5;
            //                              1...5 = 25  *               1...4 = 10           
            var spawnChance = Mathf.Pow(nicheRating, 2) * Mathf.Pow(potentialRating, 1.7f) / (Mathf.Pow((playersOnMarket + 1), 2));

            if (spawnChance > Random.Range(0, 1000))
                SpawnInvestmentFunds(1, 100000, 5000000);
        }
    }

    int GenerateInvestmentFund(string name, long money)
    {
        return CompanyUtils.GenerateInvestmentFund(gameContext, name, money).shareholder.Id;
    }

    long GetRandomFundSize(int min, int max)
    {
        int value = UnityEngine.Random.Range(min, max);

        return System.Convert.ToInt64(value);
    }

    void SpawnInvestmentFunds(int amountOfFunds, int investmentMin, int investmentMax)
    {
        for (var i = 0; i < amountOfFunds; i++)
            GenerateInvestmentFund(RandomUtils.GenerateInvestmentCompanyName(), GetRandomFundSize(investmentMin, investmentMax));
    }

    void SpawnInvestors(int amountOfInvestors, int investmentMin, int investmentMax)
    {
        for (var i = 0; i < amountOfInvestors; i++)
            InvestmentUtils.GenerateAngel(gameContext);
    }
}
