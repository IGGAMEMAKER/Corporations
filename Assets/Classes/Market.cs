using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes
{
    class Market
    {
        int BaseTechCost;
        int BaseEngagement;
        int BasePaymentAbility;
        int BaseMarketingCost;
        int ChangePeriod; // 
        int AmountOfFeatures;

        public Market (int baseTechCost, int baseEngagement, int basePaymentAbility, int baseMarketingCost, int changePeriod, int amountOfFeatures)
        {
            BaseTechCost = baseTechCost;
            BaseEngagement = baseEngagement;
            BasePaymentAbility = basePaymentAbility;
            BaseMarketingCost = baseMarketingCost;
            ChangePeriod = changePeriod;
            AmountOfFeatures = amountOfFeatures;
        }


    }
}
