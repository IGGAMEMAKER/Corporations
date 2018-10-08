using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes
{
    class ShareholderInfo
    {
        List<ShareInfo> Shareholders;

        public ShareholderInfo (List<ShareInfo> shareholders)
        {
            Shareholders = shareholders;
        }

        public void AddShareholder (int share, Investor investor, int investments = 0)
        {
            // buy free shares
            Shareholders.Add(new ShareInfo(share, investor, investments));
        }

        public void EditShare (int share, int sellerId, int buyerId, int price)
        {
            Shareholders[sellerId].SellShare(share, price);
            Shareholders[buyerId].BuyShare(share, price);
        }
    }
}
