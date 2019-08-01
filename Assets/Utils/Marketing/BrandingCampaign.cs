using Assets.Classes;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static bool HasBrandingCooldown(GameEntity company)
        {
            return CooldownUtils.HasCooldown(company, CooldownType.BrandingCampaign);
        }

        public static void StartBrandingCampaign(GameContext gameContext, GameEntity company)
        {
            var resources = GetBrandingCost(gameContext, company);

            if (!CompanyUtils.IsEnoughResources(company, resources) || HasBrandingCooldown(company))
                return;

            AddBrandPower(company, GetBrandingPowerGain(gameContext, company));
            //AddMassUsersWhileBrandingCampaign(company, gameContext);

            var duration = GetBrandingCampaignCooldownDuration();


            CooldownUtils.AddCooldownAndSpendResources(gameContext, company, CooldownType.BrandingCampaign, duration, resources);
        }

        public static int GetBrandingCampaignCooldownDuration()
        {
            return Constants.COOLDOWN_BRANDING;
        }

        public static void AddBrandPower(GameEntity company, int power)
        {
            var brandPower = (int)Mathf.Clamp(company.branding.BrandPower + power, 0, 100);

            company.ReplaceBranding(brandPower);
        }

        public static int GetBrandingPowerGain(GameContext gameContext, GameEntity company)
        {
            int techLeadershipBonus = company.isTechnologyLeader ? 2 : 1;

            int marketingDirectorBonus = 1;

            var financing = GetMarketingFinancingBrandPowerGainModifier(company.finance.marketingFinancing);

            return financing * techLeadershipBonus * marketingDirectorBonus;
        }

        public static int GetMarketingFinancingBrandPowerGainModifier(GameEntity company)
        {
            return GetMarketingFinancingBrandPowerGainModifier(company.finance.marketingFinancing);
        }

        public static int GetMarketingFinancingBrandPowerGainModifier(MarketingFinancing financing)
        {
            switch (financing)
            {
                case MarketingFinancing.Zero: return 0;
                case MarketingFinancing.Low: return 1;
                case MarketingFinancing.Medium: return 2;
                case MarketingFinancing.High: return 5;

                default: return 0;
            }
        }
    }
}
