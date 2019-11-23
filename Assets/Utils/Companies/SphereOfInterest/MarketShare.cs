using Entitas;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        internal static long GetMarketShareOfCompanyMultipliedByHundred(GameEntity product, GameContext gameContext)
        {
            long clients = 0;

            foreach (var p in NicheUtils.GetProductsOnMarket(gameContext, product))
                clients += MarketingUtils.GetClients(p);

            if (clients == 0)
                return 0;

            var share = 100 * MarketingUtils.GetClients(product) / clients;

            //Debug.Log("GetMarketShareOf " + product.company.Name + " : " + share);

            return share;
        }

        internal static long GetControlInMarket(GameEntity group, NicheType nicheType, GameContext gameContext)
        {
            var products = NicheUtils.GetProductsOnMarket(gameContext, nicheType);

            long share = 0;

            long clients = 0;

            foreach (var p in products)
            {
                var cli = MarketingUtils.GetClients(p);
                share += GetControlInCompany(group, p, gameContext) * cli;

                clients += cli;
            }

            if (clients == 0)
                return 0;

            return share / clients;

            //return HasCompanyOnMarket(myCompany, nicheType, gameContext) ? 100 / players : 0;
        }

        public static int GetControlInCompany(GameEntity controlling, GameEntity holding, GameContext gameContext)
        {
            int shares = 0;

            foreach (var daughter in GetDaughterCompanies(gameContext, controlling.company.Id))
            {
                if (daughter.company.Id == holding.company.Id)
                    shares += GetShareSize(gameContext, holding.company.Id, controlling.shareholder.Id);

                if (!daughter.hasProduct)
                    shares += GetControlInCompany(daughter, holding, gameContext);
            }

            return shares;
        }
    }
}
