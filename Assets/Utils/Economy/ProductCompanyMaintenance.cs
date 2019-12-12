using Assets.Utils.Formatting;
using UnityEngine;

namespace Assets.Utils
{
    partial class EconomyUtils
    {
        // resulting costs
        internal static long GetProductCompanyMaintenance(GameEntity e, GameContext gameContext)
        {
            var devFinancing = GetProductDevelopmentCost(e, gameContext);
            var marketingFinancing = GetProductMarketingCost(e, gameContext);

            return devFinancing + marketingFinancing;
        }

        public static long GetProductMarketingCost(GameEntity e, GameContext gameContext, float financing)
        {
            var gainedClients = MarketingUtils.GetAudienceGrowth(e, gameContext);
            var acquisitionCost = Markets.GetClientAcquisitionCost(e.product.Niche, gameContext);

            var result = gainedClients * acquisitionCost * financing;

            //if (result < 0)
            //{
            //    var niche = EnumUtils.GetFormattedNicheName(e.product.Niche);
            //    Debug.Log($"{niche} gained clients = {gainedClients}, acquisition cost = {acquisitionCost}, financing = {financing}");
            //}

            return (long)result;
        }
        public static long GetProductMarketingCost(GameEntity e, GameContext gameContext)
        {
            var financing = GetMarketingFinancingCostMultiplier(e);

            return GetProductMarketingCost(e, gameContext, financing);
        }

        public static long GetProductDevelopmentCost(GameEntity e, GameContext gameContext)
        {
            var baseCost = Markets.GetBaseDevelopmentCost(e.product.Niche, gameContext);

            var concept = Products.GetProductLevel(e);
            var niche = Markets.GetNiche(gameContext, e);
            var complexity = (int)niche.nicheBaseProfile.Profile.AppComplexity;

            var development = Mathf.Pow(1f + complexity / 100f, concept);

            return (long)(baseCost * development);
        }
    }
}
