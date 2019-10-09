using Assets.Classes;
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

        public static BonusContainer GetMonthlyBrandPowerChange(GameEntity product, GameContext gameContext)
        {
            var conceptStatus = ProductUtils.GetConceptStatus(product, gameContext);
            var isOutOfMarket = conceptStatus == ConceptStatus.Outdated;
            var isInnovator = conceptStatus == ConceptStatus.Leader;

            var percent = 4;
            var baseDecay = -product.branding.BrandPower * percent / 100;

            var BrandingChangeBonus = new BonusContainer("Brand power change")
                .AppendAndHideIfZero(percent + "% Decay", (int)baseDecay)
                .AppendAndHideIfZero("Outdated app", isOutOfMarket ? -1 : 0)
                .AppendAndHideIfZero("Is Innovator", isInnovator ? 5 : 0);

            return BrandingChangeBonus;
        }
    }
}
