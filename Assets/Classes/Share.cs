using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes
{
    class ShareInfo
    {
        public int Share { get; set; }
        public int Investments { get; set; }
        public int Benefit { get; set; }
        int Shareholder;

        public ShareInfo (int share, int investorId, int investments)
        {
            Share = share;
            Shareholder = investorId;
            Investments = investments;
        }

        public void BuyShare (int share, int spend)
        {
            Share += share;
            Investments += spend;
        }

        public void SellShare (int share, int benefit)
        {
            Share -= share;
            Investments -= benefit;
        }
    }
}
