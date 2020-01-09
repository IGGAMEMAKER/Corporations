using System;
using UnityEngine;

namespace Assets.Core
{
    partial class Companies
    {
        public static GameEntity[] GetMyNeighbours(GameContext context)
        {
            GameEntity[] products = GetProductsNotControlledByPlayer(context);

            var myProduct = GetPlayerControlledProductCompany(context).product;

            // TODO GetMyNeighbours
            Debug.Log("GetMyNeighbours returns false data! it does not count products in same industry/niche?");

            return Array.FindAll(products, e => e.product.Niche != myProduct.Niche);
        }

        public static GameEntity[] GetMyCompetitors(GameContext context)
        {
            //return NicheUtils.GetPlayersOnMarket(GetPlayerControlledProductCompany(context), context);
            GameEntity[] products = GetProductsNotControlledByPlayer(context);

            GameEntity myProductEntity = GetPlayerControlledProductCompany(context);

            return Array.FindAll(products, e => e.product.Niche == myProductEntity.product.Niche);
        }

        // TODO move to separate file
        public static bool IsRelatedToPlayer(GameContext gameContext, int companyId) => IsRelatedToPlayer(gameContext, GetCompany(gameContext, companyId));
        public static bool IsRelatedToPlayer(GameContext gameContext, GameEntity company)
        {
            var playerCompany = GetPlayerCompany(gameContext);

            if (playerCompany == null)
                return false;

            return company.isControlledByPlayer || IsDaughterOfCompany(playerCompany, company);
        }
    }
}
