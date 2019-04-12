using System.Collections.Generic;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static int GetTotalShares(Dictionary<int, int> shareholders)
        {
            int totalShares = 0;
            foreach (var e in shareholders)
                totalShares += e.Value;

            return totalShares;
        }

        public static int BecomeInvestor(GameContext context, GameEntity e, long money)
        {
            int investorId = GenerateInvestorId(context); // GenerateInvestorId();

            string name = "Investor?";

            // company
            if (e.hasCompany)
                name = e.company.Name;

            // or human
            // TODO turn human to investor

            e.AddShareholder(investorId, name, money);

            return investorId;
        }

        public static void AddShareholder(GameContext context, int companyId, int investorId, int shares)
        {
            var c = GetCompanyById(context, companyId);

            Dictionary<int, int> shareholders;

            if (!c.hasShareholders)
            {
                shareholders = new Dictionary<int, int>();
                shareholders[investorId] = shares;

                c.AddShareholders(shareholders);
            }
            else
            {
                shareholders = c.shareholders.Shareholders;
                shareholders[investorId] = shares;

                c.ReplaceShareholders(shareholders);
            }
        }

        public static void TransferShares(GameContext context, int companyId, int buyerInvestorId, int sellerInvestorId, int amountOfShares)
        {
            var c = GetCompanyById(context, companyId);

            var shareholders = c.shareholders.Shareholders;

            if (shareholders.TryGetValue(buyerInvestorId, out int newBuyerShares))
                newBuyerShares += amountOfShares;

            if (shareholders.TryGetValue(buyerInvestorId, out int newSellerShares))
                newSellerShares -= amountOfShares;


            shareholders[sellerInvestorId] = newSellerShares;
            if (newSellerShares == 0)
                shareholders.Remove(sellerInvestorId);

            shareholders[buyerInvestorId] = newBuyerShares;

            c.ReplaceShareholders(shareholders);
        }

        //public static bool IsCanBuyShares(GameContext context, int )

        public static void BuyShares(GameContext context, int companyId, int buyerInvestorId, int sellerInvestorId, int amountOfShares, long bid)
        {
            TransferShares(context, companyId, buyerInvestorId, sellerInvestorId, amountOfShares);

            var buyer = GetInvestorById(context, buyerInvestorId);
            var seller = GetInvestorById(context, sellerInvestorId);

            var buyerResources = buyer.companyResource.Resources;
            var sellerResources = seller.companyResource.Resources;

            buyerResources.AddMoney(-bid);
            sellerResources.AddMoney(bid);

            buyer.ReplaceCompanyResource(buyerResources);
            seller.ReplaceCompanyResource(sellerResources);
        }
    }
}
