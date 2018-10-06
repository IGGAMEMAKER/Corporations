using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes
{
    enum InvestorType
    {
        Speculant,
        WantsDividends
    }

    class Investor
    {
        int Money;
        InvestorType Type;

        public Investor (int money, InvestorType type)
        {
            Money = money;
            Type = type;
        }

        public void SpendMoney (int amount)
        {
            Money -= amount;
        }

        public void GainMoney (int amount)
        {
            Money += amount;
        }
    }
}
