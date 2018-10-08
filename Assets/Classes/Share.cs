using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes
{
    class ShareInfo
    {
        int Share;
        Investor Shareholder;
        int Investments;

        public ShareInfo (int share, Investor investor, int investments)
        {
            Share = share;
            Shareholder = investor;
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
