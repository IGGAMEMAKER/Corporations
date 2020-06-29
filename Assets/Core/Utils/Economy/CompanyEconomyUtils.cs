using System.Linq;

namespace Assets.Core
{
    public static partial class Economy
    {
        public static long BalanceOf(GameEntity company) => company.companyResource.Resources.money;

        public static long GetCompanyIncome(GameContext context, int companyId) => GetCompanyIncome(context, Companies.Get(context, companyId));
        public static long GetCompanyIncome(GameContext context, GameEntity e)
        {
            if (Companies.IsProductCompany(e))
                return GetProductCompanyIncome(e, context);

            return GetGroupIncome(context, e);
        }

        // TODO move to raise investments
        public static bool IsCanTakeFastCash(GameContext gameContext, GameEntity company) => !IsHasCashOverflow(gameContext, company);
        public static bool IsHasCashOverflow(GameContext gameContext, GameEntity company)
        {
            var valuation = GetCompanyCost(gameContext, company);

            var balance = Economy.BalanceOf(company);
            var maxCashLimit = valuation * 7 / 100;

            bool hasCashOverflow = balance > maxCashLimit;

            return hasCashOverflow;
        }

        public static long GetProfit(GameContext context, int companyId) => GetProfit(context, Companies.Get(context, companyId));
        public static long GetProfit(GameContext context, GameEntity c)
        {
            return GetCompanyIncome(context, c) - GetCompanyMaintenance(context, c);
        }

        public static bool IsWillBecomeBankruptOnNextPeriod(GameContext gameContext, GameEntity c)
        {
            var profit = Economy.GetProfit(gameContext, c);
            var balance = Economy.BalanceOf(c);

            var willBecomeBankruptNextPeriod = balance + profit < 0;

            return willBecomeBankruptNextPeriod;
        }


        public static bool IsCanMaintainForAWhile(GameEntity company, GameContext gameContext, long money, int periods)
        {
            return BalanceOf(company) + (GetProfit(gameContext, company) - money) * periods >= 0;
        }
        public static bool IsCanMaintain(GameEntity company, GameContext gameContext, long money)
        {
            return GetProfit(gameContext, company) >= money;
        }


        public static bool IsProfitable(GameContext gameContext, int companyId) => IsProfitable(gameContext, Companies.Get(gameContext, companyId));
        public static bool IsProfitable(GameContext gameContext, GameEntity company)
        {
            return GetProfit(gameContext, company) > 0;
        }




        public static bool IsCompanyNeedsMoreMoneyOnMarket(GameContext gameContext, GameEntity product)
        {
            return !IsProfitable(gameContext, product.company.Id);
        }


        // TODO move to raise investments
        public static long GetFastCashAmount(GameContext gameContext, GameEntity company)
        {
            int fraction = C.FAST_CASH_COMPANY_SHARE;

            return GetCompanyCost(gameContext, company) * fraction / 100;
        }
        
        // TODO move to raise investments
        public static void RaiseFastCash(GameContext gameContext, GameEntity company)
        {
            var valuation = GetCompanyCost(gameContext, company);
            var offer = GetFastCashAmount(gameContext, company);

            var fund = Investments.GetRandomInvestmentFund(gameContext);

            bool hasShareholders = company.shareholders.Shareholders.Count > 1;
            if (!hasShareholders)
            {
                Companies.AddInvestmentProposal(gameContext, company.company.Id,
                    new InvestmentProposal
                    {
                        InvestorBonus = InvestorBonus.None,
                        Offer = offer,
                        ShareholderId = fund,
                        Valuation = valuation,
                        WasAccepted = false
                    });
                Companies.AcceptInvestmentProposal(gameContext, company.company.Id, fund);
            }
            else
            {
                for (var i = 0; i < company.shareholders.Shareholders.Count; i++)
                {
                    var investorId = company.shareholders.Shareholders.Keys.ToArray()[i];
                    var inv = Companies.GetInvestorById(gameContext, investorId);

                    bool isCeo = inv.shareholder.InvestorType == InvestorType.Founder;

                    if (!isCeo)
                    {
                        Companies.AddInvestmentProposal(gameContext, company.company.Id,
                            new InvestmentProposal
                            {
                                InvestorBonus = InvestorBonus.None,
                                Offer = offer,
                                ShareholderId = investorId,
                                Valuation = valuation,
                                WasAccepted = false
                            });
                        Companies.AcceptInvestmentProposal(gameContext, company.company.Id, investorId);
                    }
                }
            }
        }
    }
}
