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

            var innovationModifier = IsWillInnovate(company, gameContext) ? 1.5f : 1;

            var random = 1; // Random.Range(1, 1.3f);

            var niche = NicheUtils.GetNicheEntity(gameContext, company.product.Niche);
            var baseConceptTime = GetBaseConceptTime(niche);

            var time = (int)(baseConceptTime * innovationModifier * 100 * random / teamPerformance);

            //Debug.Log($"Company {company.company.Name} iteration time: {time}");

            return time;
        }
    }
}
