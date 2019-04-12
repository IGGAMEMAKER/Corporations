using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class CompanyUtils
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
    }
}
