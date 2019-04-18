using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Utils
{
    public static partial class CompanyEconomyUtils
    {
        public static void IncreaseCompanyBalance(GameContext context, int companyId, long sum)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            long balance = c.companyResource.Resources.money + sum;

            c.ReplaceCompanyResource(c.companyResource.Resources.SetMoney(balance));
        }

        public static void RestructureFinances(GameContext context, int percent, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(context, companyId);

            var balance = c.companyResource.Resources.money;
            var investments = c.shareholder.Money;

            var total = balance + investments;

            investments = total * percent / 100;
            balance = total - investments;

            c.ReplaceCompanyResource(c.companyResource.Resources.SetMoney(balance));
            c.ReplaceShareholder(c.shareholder.Id, c.shareholder.Name, investments);
        }
    }
}
