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
            var speed = TeamUtils.GetPerformance(gameContext, company);

            var innovation = IsWillInnovate(company, gameContext, UserType.Core) ? 4 : 1;

            var random = UnityEngine.Random.Range(1, 1.3f);

            return (int)(Constants.COOLDOWN_CONCEPT * random * innovation * 100 / speed);
        }



        public static bool HasEnoughResourcesForSegmentUpgrade(GameEntity product, GameContext gameContext, UserType userType)
        {
            var costs = GetSegmentUpgradeCost(product, gameContext, userType);

            return CompanyUtils.IsEnoughResources(product, costs);
        }
    }
}
