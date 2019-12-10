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
        public static Bonus<long> GetMonthlyBrandPowerChange(GameEntity product, GameContext gameContext)
        {
            var conceptStatus = ProductUtils.GetConceptStatus(product, gameContext);

            var isOutOfMarket = conceptStatus == ConceptStatus.Outdated;
            var isInnovator = conceptStatus == ConceptStatus.Leader;

            var percent = 5;
            var baseDecay = product.branding.BrandPower * percent / 100;


            var isMarketingAggressively = product.financing.Financing[Financing.Marketing] == 2;
            var isMarketingNormally     = product.financing.Financing[Financing.Marketing] == 1;
            var isPayingForMarketing    = product.financing.Financing[Financing.Marketing] > 0;

            var partnershipBonuses = GetPartnershipBonuses(product, gameContext);

            var BrandingChangeBonus = new Bonus<long>("Brand power change")
                .AppendAndHideIfZero(percent + "% Decay", -(int)baseDecay)
                .AppendAndHideIfZero("Is not paying for marketing", !isPayingForMarketing ? -7 : 0)
                .AppendAndHideIfZero("Outdated app", isOutOfMarket ? -4 : 0)

                .AppendAndHideIfZero("Capturing market", isMarketingAggressively ? 4 : 0)

                // multiply our marketing efforts if paying for them
                .MultiplyAndHideIfOne("Is Innovator", isInnovator && isPayingForMarketing ? 2 : 1)
                .AppendAndHideIfZero("Is Innovator", isInnovator && !isPayingForMarketing ? 1 : 0)
                .Append("Partnerships", (int)partnershipBonuses)
                ;

            return BrandingChangeBonus;
        }

        private static float GetPartnershipBonuses(GameEntity product, GameContext gameContext)
        {
            var partners = Companies.GetPartnerList(product, gameContext);

            var partnersInSameIndustry = partners
                .Where(p =>
                NicheUtils.GetIndustry(p.product.Niche, gameContext) ==
                NicheUtils.GetIndustry(product.product.Niche, gameContext)
                );


            float value = 0;
            foreach (var p in partnersInSameIndustry)
            {
                var marketShare = Companies.GetMarketShareOfCompanyMultipliedByHundred(product, gameContext);
                var marketSize = NicheUtils.GetMarketRating(gameContext, product.product.Niche);
                
                value += marketShare * marketSize / 100f;
            }

            return Mathf.Clamp(value, 0, 1) * 3f;
        }
    }
}
