using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class MarketingUtils
    {
        public static void AddBrandPower(GameEntity company, float power)
        {
            var brandPower = Mathf.Clamp(company.branding.BrandPower + power, 0, 100);

            company.ReplaceBranding(brandPower);
        }

        // float
        public static Bonus<long> GetBrandChange(GameEntity product, GameContext gameContext)
        {
            var conceptStatus = Products.GetConceptStatus(product, gameContext);

            var isOutOfMarket = conceptStatus == ConceptStatus.Outdated;
            var isInnovator = conceptStatus == ConceptStatus.Leader;

            var percent = 10;
            var baseDecay = product.branding.BrandPower * percent / 100;


            var isMarketingAggressively = Economy.GetMarketingFinancing(product) == Products.GetMaxFinancing;
            var isReleased    = product.isRelease;

            var partnershipBonuses = GetPartnershipBonuses(product, gameContext);

            var isMonopolist = Markets.GetCompetitorsAmount(product, gameContext) == 1;

            var BrandingChangeBonus = new Bonus<long>("Brand power change")
                .AppendAndHideIfZero(percent + "% Decay", -(int)baseDecay)
                
                .AppendAndHideIfZero("Released", isReleased ? 1 : 0)
                .AppendAndHideIfZero("MONOPOLY", isMonopolist ? 10 : 0)
                
                .AppendAndHideIfZero("Outdated app", isOutOfMarket ? -4 : 0)

                .AppendAndHideIfZero("Capturing market", isMarketingAggressively ? 6 : 0)

                .Append("Partnerships", (int)partnershipBonuses)
                
                ;

            return BrandingChangeBonus;
        }

        private static float GetPartnershipBonuses(GameEntity product, GameContext gameContext)
        {
            var partners = Companies.GetPartnerList(product, gameContext);

            var industry = Markets.GetIndustry(product.product.Niche, gameContext);


            float value = 0;
            foreach (var p in partners)
                value += Companies.GetBrandProjectionOnIndustry(p, gameContext, industry);

            return Mathf.Clamp(value, 0, 3);
        }
    }
}
