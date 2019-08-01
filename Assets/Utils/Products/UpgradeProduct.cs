using Assets.Classes;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class ProductUtils
    {
        public static void UpdateNicheSegmentInfo(GameEntity product, GameContext gameContext)
        {
            var niche = NicheUtils.GetNicheEntity(gameContext, product.product.Niche);

            UpdateNicheSegmentInfo(product, niche);
        }

        public static void UpdateNicheSegmentInfo(GameEntity product, GameEntity niche)
        {
            var segments = niche.segment.Segments;

            if (IsWillInnovate(product, niche))
            {
                segments[UserType.Core] = GetProductLevel(product) + 1;

                MarketingUtils.AddBrandPower(product, Random.Range(3, 10));

                niche.ReplaceSegment(segments);
            }
        }


        public static void UpgradeExpertise(GameEntity product, GameContext gameContext)
        {
            var nicheCosts = NicheUtils.GetNicheCosts(gameContext, product.product.Niche);
            var baseIdeaCost = nicheCosts.IdeaCost;

            var expertise = product.expertise.ExpertiseLevel;

            var required = new TeamResource(0, 0, 0, baseIdeaCost * expertise, 0);

            if (CompanyUtils.IsEnoughResources(product, required))
            {
                CompanyUtils.SpendResources(product, required);
                product.ReplaceExpertise(expertise + 1);
            }
        }

        // TODO DUPLICATE!! UpdateSegment Doesnot Use these functions
        public static void UpdateSegment(GameEntity product, GameContext gameContext)
        {
            var userType = UserType.Core;

            var cooldown = new CooldownImproveSegment(userType);

            if (CooldownUtils.HasCooldown(product, cooldown))
                return;

            var costs = GetProductUpgradeCost(product, gameContext);

            if (!CompanyUtils.IsEnoughResources(product, costs))
                return;

            UpdateNicheSegmentInfo(product, gameContext);

            var p = product.product;

            product.ReplaceProduct(p.Id, p.Niche, GetProductLevel(product) + 1);


            var duration = GetProductUpgradeIterationTime(gameContext, product);
            CooldownUtils.AddCooldownAndSpendResources(gameContext, product, cooldown, duration, costs);
        }



        public static int GetProductUpgradeIterationTime(GameContext gameContext, GameEntity company)
        {
            var teamPerformance = TeamUtils.GetPerformance(gameContext, company);

            var innovationModifier = IsWillInnovate(company, gameContext) ? 2 : 1;

            var random = 1; // Random.Range(1, 1.3f);

            var niche = NicheUtils.GetNicheEntity(gameContext, company.product.Niche);
            var baseConceptTime = GetBaseConceptTime(niche);

            var time = (int)(baseConceptTime * innovationModifier * 100 * random / teamPerformance);

            //Debug.Log($"Company {company.company.Name} iteration time: {time}");

            return time;
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

        public static bool HasEnoughResourcesForSegmentUpgrade(GameEntity product, GameContext gameContext)
        {
            var costs = GetProductUpgradeCost(product, gameContext);

            return CompanyUtils.IsEnoughResources(product, costs);
        }
    }
}
