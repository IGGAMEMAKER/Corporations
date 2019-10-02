using Assets.Classes;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class ProductUtils
    {
        public static int GetBaseConceptTime(GameEntity niche)
        {
            return GetBaseConceptTime(niche.nicheLifecycle.NicheChangeSpeed);
        }

        public static int GetBaseConceptTime(NicheChangeSpeed nicheChangeSpeed)
        {
            switch (nicheChangeSpeed)
            {
                case NicheChangeSpeed.Month: return 30;
                case NicheChangeSpeed.Quarter: return 45;
                case NicheChangeSpeed.Year: return 60;
                case NicheChangeSpeed.ThreeYears: return 180;

                default: return 0;
            }
        }



        public static int GetProductUpgradeIterationTime(GameContext gameContext, GameEntity company)
        {
            var teamPerformance = TeamUtils.GetPerformance(gameContext, company);

            var niche = NicheUtils.GetNicheEntity(gameContext, company.product.Niche);
            var baseConceptTime = GetBaseConceptTime(niche);

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
