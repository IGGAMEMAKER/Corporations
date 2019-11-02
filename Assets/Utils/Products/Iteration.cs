using Assets.Classes;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class ProductUtils
    {
        public static int GetBaseIterationTime(GameEntity niche)
        {
            return GetBaseConceptTime(niche.nicheLifecycle.NicheChangeSpeed) / 4;
        }

        public static int GetBaseConceptTime(NicheSpeed nicheChangeSpeed)
        {
            switch (nicheChangeSpeed)
            {
                case NicheSpeed.Month: return 30;
                case NicheSpeed.Quarter: return 90;

                case NicheSpeed.HalfYear: return 180;
                case NicheSpeed.Year: return 360;

                case NicheSpeed.ThreeYears: return 360 * 3;

                default: return 0;
            }
        }



        public static int GetProductUpgradeIterationTime(GameContext gameContext, GameEntity company)
        {
            var teamPerformance = TeamUtils.GetPerformance(gameContext, company);

            var niche = NicheUtils.GetNicheEntity(gameContext, company.product.Niche);
            var baseConceptTime = GetBaseIterationTime(niche);

            var innovationTime = IsWillInnovate(company, gameContext) ? 50 : 0;

            var financing = company.financing.Financing;
            var devModifier = financing[Financing.Development];


            var culture = company.corporateCulture.Culture;
            var mindsetModifier = culture[CorporatePolicy.WorkerMindset];

            var modifiers = 100 + innovationTime - devModifier * Constants.FINANCING_ITERATION_SPEED_PER_LEVEL - mindsetModifier * Constants.CULTURE_ITERATION_SPEED_PER_LEVEL;
            var time = (int) (baseConceptTime * modifiers / 100f);

            Debug.Log($"GetProductUpgradeIterationTime: company={company.company.Name} dev={devModifier} culture={culture} ** result={time}");

            return time;
        }

        public static int GetProductUpgradeFinalIterationTime(GameContext gameContext, GameEntity company)
        {
            var baseTime = GetProductUpgradeIterationTime(gameContext, company);
            return baseTime;

            var random = Random.Range(10, 13);
            return baseTime * random / 10;
        }
    }
}
