using System.Linq;
using UnityEngine;

namespace Assets.Utils
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

            var percent = 7;
            var baseDecay = product.branding.BrandPower * percent / 100;


            var isMarketingAggressively = product.financing.Financing[Financing.Marketing] == 2;
            var isPayingForMarketing    = product.isRelease;

            var partnershipBonuses = GetPartnershipBonuses(product, gameContext);

            var BrandingChangeBonus = new Bonus<long>("Brand power change")
                .AppendAndHideIfZero("Is not released", !isPayingForMarketing ? -7 : 0)
                .AppendAndHideIfZero("Released", isPayingForMarketing ? 1 : 0)
                .AppendAndHideIfZero("Outdated app", isOutOfMarket ? -4 : 0)

                .AppendAndHideIfZero("Capturing market", isMarketingAggressively ? 4 : 0)

                // multiply our marketing efforts if paying for them
                .MultiplyAndHideIfOne("Is Innovator", isInnovator && isPayingForMarketing ? 2 : 1)
                .AppendAndHideIfZero("Is Innovator", isInnovator && !isPayingForMarketing ? 1 : 0)
                .Append("Partnerships", (int)partnershipBonuses)
                .AppendAndHideIfZero(percent + "% Decay", -(int)baseDecay)
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
