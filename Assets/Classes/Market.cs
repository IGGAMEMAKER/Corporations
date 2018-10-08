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
        int AmountOfFeatures;

        int ChangePeriod; // 

        public Market (int baseTechCost, int baseEngagement, int basePaymentAbility, int baseMarketingCost, int amountOfFeatures, int changePeriod)
        {
            BaseTechCost = baseTechCost;
            BaseEngagement = baseEngagement;
            BasePaymentAbility = basePaymentAbility;
            BaseMarketingCost = baseMarketingCost;
            AmountOfFeatures = amountOfFeatures;

            ChangePeriod = changePeriod;
        }


    }
}
