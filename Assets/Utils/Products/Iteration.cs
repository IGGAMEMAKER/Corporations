using Assets.Classes;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class ProductUtils
    {
        public static int GetBaseIterationTime(GameEntity niche)
        {
            return GetBaseConceptTime(niche.nicheLifecycle.NicheChangeSpeed);
        }

        public static int GetBaseConceptTime(NicheSpeed nicheChangeSpeed)
        {
            switch (nicheChangeSpeed)
            {
                case NicheSpeed.Month: return 30;
                case NicheSpeed.Quarter: return 45;
                case NicheSpeed.Year: return 60;
                case NicheSpeed.ThreeYears: return 180;

                default: return 0;
            }
        }



        public static int GetProductUpgradeIterationTime(GameContext gameContext, GameEntity company)
        {
            var teamPerformance = TeamUtils.GetPerformance(gameContext, company);

            var niche = NicheUtils.GetNicheEntity(gameContext, company.product.Niche);
            var baseConceptTime = GetBaseIterationTime(niche);

            var innovationTime = IsWillInnovate(company, gameContext) ? 15 : 10;

            //return baseConceptTime * innovationTime * 100 / 10 / teamPerformance;
            return baseConceptTime * innovationTime / 10;
        }

        public static int GetProductUpgradeFinalIterationTime(GameContext gameContext, GameEntity company)
        {
            var random = Random.Range(10, 13);

            var baseTime = GetProductUpgradeIterationTime(gameContext, company);
            return baseTime;

            return baseTime * random / 10;
        }
    }
}
