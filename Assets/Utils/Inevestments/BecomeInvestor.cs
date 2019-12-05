namespace Assets.Utils
{
    public static partial class InvestmentUtils
    {
        public static int BecomeInvestor(GameContext context, GameEntity e, long money)
        {
            int investorId = GenerateInvestorId(context);

            string name = "Investor?";

            InvestorType investorType = InvestorType.VentureInvestor;

            // company
            if (e.hasCompany)
            {
                name = e.company.Name;

                if (e.company.CompanyType == CompanyType.FinancialGroup)
                {
                    investorType = InvestorType.VentureInvestor;

                }
                else
                {
                    investorType = InvestorType.Strategic;

                }
            }
            else if (e.hasHuman)
            {
                // or human
                // TODO turn human to investor

                name = e.human.Name + " " + e.human.Surname;
                investorType = InvestorType.Founder;

                if (!e.hasCompanyResource)
                    e.AddCompanyResource(new Classes.TeamResource(money));
            }

            e.AddShareholder(investorId, name, investorType);
            if (!e.hasCorporateCulture)
                e.AddCorporateCulture(CompanyUtils.GetFundCorporateCulture());
            AddMoneyToInvestor(context, investorId, money);

            return investorId;
        }
    }
}
