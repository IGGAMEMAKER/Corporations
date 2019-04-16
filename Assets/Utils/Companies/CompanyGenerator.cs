using Assets.Classes;
using Entitas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        // Create
        public static int GenerateCompanyId(GameContext context)
        {
            return context.GetEntities(GameMatcher.Company).Length;
        }

        public static int GenerateInvestorId(GameContext context)
        {
            return context.GetEntities(GameMatcher.Shareholder).Length;
        }

        public static GameEntity CreateCompany(GameContext context, string name, CompanyType companyType, Dictionary<int, int> founders = null)
        {
            var e = context.CreateEntity();

            int id = GenerateCompanyId(context);

            e.AddCompany(id, name, companyType);
            e.AddCompanyResource(new TeamResource());

            if (founders == null)
                founders = new Dictionary<int, int>();

            e.AddShareholders(founders);
            e.AddInvestmentProposals(new List<InvestmentProposal>());

            return e;
        }

        public static GameEntity GenerateCompanyGroup(GameContext context, string name, Dictionary<int, int> founders = null)
        {
            var c = CreateCompany(context, name, CompanyType.Group, founders);

            BecomeInvestor(context, c, 10000000);

            return c;
        }

        public static GameEntity GenerateInvestmentFund(GameContext context, string name, long money)
        {
            var c = CreateCompany(context, name, CompanyType.FinancialGroup, null);

            BecomeInvestor(context, c, money);

            return c;
        }

        public static GameEntity GenerateHoldingCompany(GameContext context, string name)
        {
            var c = GenerateCompanyGroup(context, name, null);

            return TurnToHolding(context, c.company.Id);
        }

        public static GameEntity GenerateProductCompany(GameContext context, string name, NicheType niche)
        {
            var c = CreateCompany(context, name, CompanyType.ProductCompany, null);

            return GenerateProduct(context, c, name, niche);
        }

        public static GameEntity GenerateProduct(GameContext context, GameEntity company, string name, NicheType niche)
        {
            IndustryType industry = NicheUtils.GetIndustry(niche, context);

            var resources = new TeamResource(100, 100, 100, 100, 10000);

            uint clients = (uint)UnityEngine.Random.Range(0, 10000);
            int brandPower = UnityEngine.Random.Range(0, 15);

            int productLevel = 0;
            int explorationLevel = productLevel;

            // product specific components
            company.AddProduct(company.company.Id, name, niche, industry, productLevel, explorationLevel, resources);
            company.AddFinance(0, 0, 0, 5f);
            company.AddTeam(1, 0, 0, 100);
            company.AddMarketing(clients, brandPower, false);

            return company;
        }

        // -------

        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            //System.Random random = new System.Random();

            char ch;
            for (int i = 0; i < size; i++)
            {
                //ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                ch = Convert.ToChar(Convert.ToInt32(65 + UnityEngine.Random.Range(0, 26)));
                builder.Append(ch);
            }

            if (lowerCase)
                return builder.ToString().ToLower();

            return builder.ToString();
        }

        public static string GenerateInvestmentCompanyName()
        {
            string[] names = new string[] { "Investments", "Capitals", "Funds", "and partners" };

            int index = UnityEngine.Random.Range(0, names.Length);

            int length = UnityEngine.Random.Range(4, 8);

            return "The " + RandomString(length, true) + " " + names[index];
        }
    }
}
