using Assets.Classes;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class MarketingUtils
    {
        public static void StartBrandingCampaign(GameContext gameContext, GameEntity company)
        {
            var resources = GetBrandingCost(gameContext, company);

            if (!CompanyUtils.IsEnoughResources(company, resources) || CooldownUtils.HasCooldown(company, CooldownType.BrandingCampaign))
                return;

            AddBrandPower(company, GetBrandingPowerGain(gameContext, company));
            AddMassUsersWhileBrandingCampaign(company, gameContext);

            var duration = GetBrandingCampaignCooldownDuration(gameContext, company);


            CooldownUtils.AddCooldownAndSpendResources(gameContext, company, CooldownType.BrandingCampaign, duration, resources);
        }

        public static int GetBrandingCampaignCooldownDuration(GameContext gameContext, GameEntity company)
        {
            return Constants.COOLDOWN_BRANDING;
        }

        public static void AddBrandPower(GameEntity company, int power)
        {
            var brandPower = (int)Mathf.Clamp(company.branding.BrandPower + power, 0, 100);

            company.ReplaceBranding(brandPower);
        }

        public static void AddMassUsersWhileBrandingCampaign(GameEntity company, GameContext gameContext)
        {
            Debug.Log("AddMassUsersWhileBrandingCampaign " + company.company.Name);

            var costs = GetNicheCosts(gameContext, company);
            var batch = GetCompanyClientBatch(gameContext, company);

            var clients = batch * 10 * GetMarketingFinancingBrandPowerGainModifier(company) * Random.Range(0.15f, 1.5f);

            AddClients(company, UserType.Mass, (long)clients);
        }

        public static TeamResource GetBrandingCost(GameContext gameContext, GameEntity company)
        {
            var financing = GetMarketingFinancingBrandPowerGainModifier(company.finance.marketingFinancing);

            var costs = NicheUtils.GetNicheEntity(gameContext, company.product.Niche).nicheCosts;

            var marketingCost = costs.MarketingCost * 3 * financing;
            var moneyCost = costs.AdCost * 10 * financing;

            return new TeamResource(0, 0, marketingCost, 0, moneyCost);
        }

        public static int GetBrandingPowerGain(GameContext gameContext, GameEntity company)
        {
            int techLeadershipBonus = company.isTechnologyLeader ? 2 : 1;

            int marketingDirectorBonus = 1;

            return GetMarketingFinancingBrandPowerGainModifier(company.finance.marketingFinancing) * techLeadershipBonus * marketingDirectorBonus;
        }



        public static int GetMarketingFinancingBrandPowerGainModifier(GameEntity company)
        {
            return GetMarketingFinancingBrandPowerGainModifier(company.finance.marketingFinancing);
        }

        public static int GetMarketingFinancingBrandPowerGainModifier(MarketingFinancing financing)
        {
            switch (financing)
            {
                case MarketingFinancing.Low: return 1;
                case MarketingFinancing.Medium: return 2;
                case MarketingFinancing.High: return 5;

                default: return 0;
            }
        }
    }
}
