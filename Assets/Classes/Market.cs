using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes
{
    class Market
    {
        MarketSettings marketSettings;
        MarketInfo marketInfo;

        public Market (MarketInfo marketInfo, MarketSettings marketSettings)
        {
            this.marketInfo = marketInfo;
            this.marketSettings = marketSettings;
        }
    }

    // market dynamics
    public class MarketSettings
    {
        int ChangePeriod;

        public MarketSettings (int ChangePeriod)
        {
            this.ChangePeriod = ChangePeriod;
        }
    }

    // current market info
    public class MarketInfo
    {
        int baseTechCost;
        int baseEngagement;
        int basePaymentAbility;
        int baseMarketingCost;
        int amountOfFeatures;

        public MarketInfo(int baseTechCost, int baseEngagement, int basePaymentAbility, int baseMarketingCost, int amountOfFeatures)
        {
            this.baseTechCost = baseTechCost;
            this.baseEngagement = baseEngagement;
            this.basePaymentAbility = basePaymentAbility;
            this.baseMarketingCost = baseMarketingCost;
            this.amountOfFeatures = amountOfFeatures;
        }
    }
}
