using UnityEngine;

namespace Assets.Core
{
    partial class Cooldowns
    {
        // concept upgrade cooldown
        public static void AddConceptUpgradeCooldown(GameContext gameContext, GameEntity product)
        {
            AddCooldown(gameContext, new CompanyTaskUpgradeConcept(product.company.Id), Products.GetConceptUpgradeTime(gameContext, product));
        }

        public static bool HasConceptUpgradeCooldown(GameContext gameContext, GameEntity product)
        {
            return HasCooldown(gameContext, new CompanyTaskUpgradeConcept(product.company.Id));
        }


        // culture upgrade cooldown
        public static void AddCorporateCultureUpgradeCooldown(GameContext gameContext, GameEntity company, int duration)
        {
            AddCooldown(gameContext, new CompanyTaskUpgradeCulture(company.company.Id), duration);
        }

        public static bool HasCorporateCultureUpgradeCooldown(GameContext gameContext, GameEntity company)
        {
            return HasCooldown(gameContext, new CompanyTaskUpgradeCulture(company.company.Id));
        }


        // branding campaign
        public static void AddBrandingCooldown(GameContext gameContext, GameEntity company)
        {
            var effeciency = Products.GetTeamEffeciency(gameContext, company);

            AddCooldown(gameContext, new CompanyTaskBrandingCampaign(company.company.Id), 90 * 100 / effeciency);
        }

        public static bool HasBrandingCampaignCooldown(GameContext gameContext, GameEntity company)
        {
            return HasCooldown(gameContext, new CompanyTaskBrandingCampaign(company.company.Id));
        }

        // regular campaign
        public static void AddRegularCampaignCooldown(GameContext gameContext, GameEntity company)
        {
            var effeciency = Products.GetTeamEffeciency(gameContext, company);

            Debug.Log("Regular Campaign: " + company.company.Name + " " + effeciency);

            AddCooldown(gameContext, new CompanyTaskMarketingRegularCampaign(company.company.Id), 8 * 100 / effeciency);
        }

        public static bool HasRegularCampaignCooldown(GameContext gameContext, GameEntity company)
        {
            return HasCooldown(gameContext, new CompanyTaskMarketingRegularCampaign(company.company.Id));
        }
    }
}
