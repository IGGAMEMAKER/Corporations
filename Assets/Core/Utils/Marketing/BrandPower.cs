using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Marketing
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

            // base 10

            var percent = 25;

            if (Products.IsUpgradeEnabled(product, ProductUpgrade.QA3))
            {
                percent -= 15;
            } else if (Products.IsUpgradeEnabled(product, ProductUpgrade.QA2))
            {
                percent -= 10;
            } else if (Products.IsUpgradeEnabled(product, ProductUpgrade.QA))
            {
                percent -= 5;
            }

            if (Products.IsUpgradeEnabled(product, ProductUpgrade.Support3))
            {
                percent -= 3;
            } else if (Products.IsUpgradeEnabled(product, ProductUpgrade.Support2))
            {
                percent -= 2;
            } else if (Products.IsUpgradeEnabled(product, ProductUpgrade.Support))
            {
                percent -= 1;
            }



            var baseDecay = product.branding.BrandPower * percent / 100;

            var isReleased    = product.isRelease;

            var partnershipBonuses = GetPartnershipBonuses(product, gameContext);

            var isMonopolist = Markets.GetCompetitorsAmount(product, gameContext) == 1;

            var BrandingChangeBonus = new Bonus<long>("Brand power change")
                .AppendAndHideIfZero(percent + "% Decay", -(int)baseDecay)
                
                .AppendAndHideIfZero("Released", isReleased ? 1 : 0)
                .AppendAndHideIfZero("Branding Campaign",       Products.IsUpgradeEnabled(product, ProductUpgrade.BrandCampaign) ? 1 : 0)
                .AppendAndHideIfZero("Branding Campaign (II)",  Products.IsUpgradeEnabled(product, ProductUpgrade.BrandCampaign2) ? 1 : 0)
                .AppendAndHideIfZero("Branding Campaign (III)", Products.IsUpgradeEnabled(product, ProductUpgrade.BrandCampaign3) ? 2 : 0)

                .AppendAndHideIfZero("MONOPOLY", isMonopolist ? 10 : 0)
                
                .AppendAndHideIfZero("Outdated app", isOutOfMarket ? -7 : 0)

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
