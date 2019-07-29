using Assets.Classes;

namespace Assets.Utils
{
    public static partial class ProductUtils
    {
        public static void UpdateNicheSegmentInfo(GameEntity product, GameContext gameContext, UserType userType)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            UpdateNicheSegmentInfo(product, niche, userType);
        }

        public static void UpdateNicheSegmentInfo(GameEntity product, GameEntity niche, UserType userType)
        {
            var segments = niche.segment.Segments;

            if (IsWillInnovate(product, niche, userType))
            {
                segments[userType] = GetProductLevel(product) + 1;

                niche.ReplaceSegment(segments);
            }
        }



        // TODO DUPLICATE!! UpdateSegment Doesnot Use these functions
        public static void UpdateSegment(GameEntity product, GameContext gameContext, UserType userType1)
        {
            var userType = UserType.Core;

            var cooldown = new CooldownImproveSegment(userType);

            if (CooldownUtils.HasCooldown(product, cooldown))
                return;

            var costs = GetSegmentUpgradeCost(product, gameContext, userType);

            if (!CompanyUtils.IsEnoughResources(product, costs))
                return;

            UpdateNicheSegmentInfo(product, gameContext, userType);

            var p = product.product;

            product.ReplaceProduct(p.Id, p.Niche, GetProductLevel(product) + 1);


            var duration = GetSegmentImprovementDuration(gameContext, product);
            CooldownUtils.AddCooldownAndSpendResources(gameContext, product, cooldown, duration, costs);
        }

        public static int GetSegmentImprovementDuration(GameContext gameContext, GameEntity company)
        {
            var teamPerformance = TeamUtils.GetPerformance(gameContext, company);

            var innovationModifier = IsWillInnovate(company, gameContext, UserType.Core) ? 4 : 1;

            var random = UnityEngine.Random.Range(1, 1.3f);

            var niche = NicheUtils.GetNicheEntity(gameContext, company.product.Niche);
            var baseConceptTime = GetBaseConceptTime(niche);

            return (int)(baseConceptTime * innovationModifier * 100 * random / teamPerformance);
        }


        public static int GetBaseConceptTime(GameEntity niche)
        {
            return GetBaseConceptTime(niche.nicheLifecycle.NicheChangeSpeed);
        }

        public static int GetBaseConceptTime(NicheChangeSpeed nicheChangeSpeed)
        {
            switch (nicheChangeSpeed)
            {
                case NicheChangeSpeed.Month: return 5;
                case NicheChangeSpeed.Quarter: return 15;
                case NicheChangeSpeed.Year: return 60;
                case NicheChangeSpeed.ThreeYears: return 180;

                default: return 0;
            } 
        }

        public static bool HasEnoughResourcesForSegmentUpgrade(GameEntity product, GameContext gameContext, UserType userType)
        {
            var costs = GetSegmentUpgradeCost(product, gameContext, userType);

            return CompanyUtils.IsEnoughResources(product, costs);
        }
    }
}
