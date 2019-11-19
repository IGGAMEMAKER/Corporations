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

        public static long GetProductMarketingCost(GameEntity e, GameContext gameContext)
        {
            var multiplier = GetMarketingFinancingCostMultiplier(e);

            var gainedClients = MarketingUtils.GetAudienceGrowth(e, gameContext);
            
            var brandDiscount = MarketingUtils.GetAudienceReachBrandMultiplier(e);
            var innovationDiscount = MarketingUtils.GetAudienceReachInnovationLeaderMultiplier(e);

            var acquisitionCost = NicheUtils.GetClientAcquisitionCost(e.product.Niche, gameContext);

            var result = (long)((gainedClients * acquisitionCost * multiplier) / brandDiscount / innovationDiscount);

            if (result == 0)
            {
                Debug.Log($"{EnumUtils.GetFormattedNicheName(e.product.Niche)} gained clients = {gainedClients}, brand discount = {brandDiscount}, acquisition cost = {acquisitionCost}, multiplier = {multiplier}");

            }

            return result;
        }

        public static long GetProductDevelopmentCost(GameEntity e, GameContext gameContext)
        {
            var stage = GetStageFinancingMultiplier(e);
            var team = GetTeamFinancingCostMultiplier(e);

            var baseCost = NicheUtils.GetBaseDevelopmentCost(e.product.Niche, gameContext);

            return (long)(baseCost * stage * team);
        }
    }
}
