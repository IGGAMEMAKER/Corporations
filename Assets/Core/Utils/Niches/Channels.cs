using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Markets
    {
        public static GameEntity GetMarketingChannel(GameContext gameContext, int channelId)
        {
            return GetAllMarketingChannels(gameContext).First(c => c.marketingChannel.ChannelInfo.ID == channelId);
        }

        public static GameEntity[] GetAllMarketingChannels(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.MarketingChannel);
        }

        public static IEnumerable<ChannelInfo> GetAllMarketingChannels(GameEntity product)
        {
            return product.channelInfos.ChannelInfos.Values.ToList();
        }



        public static IEnumerable<ChannelInfo> GetTheoreticallyPossibleMarketingChannels(GameEntity product)
        {
            return
                GetAllMarketingChannels(product)
                .Where(channel => !Marketing.IsActiveInChannel(product, channel.ID))
                .Where(c => !product.team.Teams[0].Tasks.Any(t => t.IsPending && t.AreSameTasks(new TeamTaskChannelActivity(c.ID, 0))))
                ;
        }

        public static IEnumerable<ChannelInfo> GetAffordableMarketingChannels(GameEntity product, GameContext gameContext)
        {
            var payer = product.isFlagship ? Companies.GetPlayerCompany(gameContext) : product;
            int periods = product.isFlagship ? 4 : 1;

            var spareBudget = Economy.GetSpareBudget(payer, gameContext, periods);

            return GetTheoreticallyPossibleMarketingChannels(product)
                .Where(IsCanAffordChannel(product, spareBudget));
        }

        public static IEnumerable<ChannelInfo> GetMaintainableMarketingChannels(GameEntity product, GameContext gameContext)
        {
            var payer = product.isFlagship ? Companies.GetPlayerCompany(gameContext) : product;
            int periods = product.isFlagship ? 10 * 4 : 1;

            var spareBudget = Economy.GetProfit(gameContext, payer);

            var channels = new List<ChannelInfo>();

            var allChannels = GetAllMarketingChannels(product).OrderBy(c => Marketing.GetChannelCost(product, c.ID)).ToList();
            var spareBudget2 = spareBudget + Economy.BalanceOf(payer);
            for (var i = 0; i < allChannels.Count(); i++)
            {
                var c = allChannels[i];
                var adCost = Marketing.GetChannelCost(product, c.ID);

                if (Marketing.IsActiveInChannel(product, c.ID) || adCost == 0)
                {
                    channels.Add(c);
                    continue;
                }

                // 5, cause some channels take too much time to recover
                if (adCost > spareBudget2)
                {
                    if (product.isFlagship)
                    {
                        Debug.Log("not enough money for " + c.ID + " " + Format.Money(adCost * 3) + " spare=" + Format.Money(spareBudget2));
                    }
                    break;
                }

                channels.Add(c);
                spareBudget2 -= adCost;
            }

            return channels;
            
            return allChannels
                .Where(IsCanMaintainChannel(product, spareBudget))
                ;
        }



        public static Func<ChannelInfo, bool> IsCanAffordChannel(GameEntity company, long spareBudget) => c =>
        {
            var adCost = Marketing.GetChannelCost(company, c.ID);

            if (adCost == 0)
                return true;

            return spareBudget > adCost;
        };

        public static Func<ChannelInfo, bool> IsCanMaintainChannel(GameEntity company, long spareBudget) => c =>
        {
            var adCost = Marketing.GetChannelCost(company, c.ID);

            if (adCost == 0)
                return true;

            return spareBudget > adCost || Marketing.IsActiveInChannel(company, c.ID);
        };
    }
}
