using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Classes
{
    class ShareholderInfo
    {
        List<ShareInfo> Shareholders;

        public ShareholderInfo (List<ShareInfo> shareholders)
        {
            Shareholders = shareholders;
        }

        public void AddShareholder (int investorId)
        {
            // buy free shares
            Shareholders.Add(new ShareInfo(0, investorId, 0));
        }

        public void EditShare (int share, int sellerId, int buyerId, int price)
        {
            Shareholders[sellerId].SellShare(share, price);
            Shareholders[buyerId].BuyShare(share, price);
        }

        internal void PrintAllShareholders()
        {
            Debug.Log("------------- SHAREHOLDERS ------------");

            for (var i = 0; i < Shareholders.Count; i++)
            {
                float share = Shareholders[i].Share;
                int benefit = -Shareholders[i].Investments;
                Debug.Log(String.Format("Shareholder {0} - {1}% (benefit: {2})", i, share, benefit));
            }
        }
    }
}
