using Assets.Classes;
using Assets.Utils.Humans;
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

        private static GameEntity CreateCompany(GameContext context, string name, CompanyType companyType)
        {
            int humanId = HumanUtils.GenerateHuman(context);

            return CreateCompany(context, name, companyType, new Dictionary<int, int>(), humanId);
        }

        private static GameEntity CreateCompany(GameContext context, string name, CompanyType companyType, Dictionary<int, int> founders, int CeoID)
        {
            var e = context.CreateEntity();

            int id = GenerateCompanyId(context);

            e.AddCompany(id, name, companyType);
            e.AddCompanyResource(new TeamResource());

            Dictionary<int, InvestorGoal> goals = new Dictionary<int, InvestorGoal>();

            e.AddShareholders(founders, goals);
            e.AddInvestmentProposals(new List<InvestmentProposal>());
            e.AddMetricsHistory(new List<MetricsInfo>());

            e.AddInvestmentRounds(false, InvestmentRound.Preseed, 0, false);
            e.isIndependentCompany = true;

            e.AddCEO(0, CeoID);

            return e;
        }

        public static void CopyShareholders(GameContext gameContext, int from, int to)
        {
            var source = GetCompanyById(gameContext, from).shareholders;

            GetCompanyById(gameContext, to).ReplaceShareholders(source.Shareholders, source.Goals);
        }



        public static GameEntity GenerateCompanyGroup(GameContext context, string name, int FormerProductCompany)
        {
            var c = GenerateCompanyGroup(context, name);

            CopyShareholders(context, FormerProductCompany, c.company.Id);

            return c;
        }


        public static GameEntity GenerateCompanyGroup(GameContext context, string name)
        {
            var c = CreateCompany(context, name, CompanyType.Group);

            BecomeInvestor(context, c, 0);

            return c;
        }

        public static GameEntity GenerateInvestmentFund(GameContext context, string name, long money)
        {
            var c = CreateCompany(context, name, CompanyType.FinancialGroup);

            BecomeInvestor(context, c, money);

            return c;
        }

        public static GameEntity GenerateHoldingCompany(GameContext context, string name)
        {
            var c = GenerateCompanyGroup(context, name);

            return TurnToHolding(context, c.company.Id);
        }

        public static GameEntity GenerateProductCompany(GameContext context, string name, NicheType niche)
        {
            var c = CreateCompany(context, name, CompanyType.ProductCompany);

            return GenerateProduct(context, c, name, niche);
        }

        public static GameEntity GenerateProduct(GameContext context, GameEntity company, string name, NicheType niche)
        {
            IndustryType industry = NicheUtils.GetIndustry(niche, context);

            var resources = new TeamResource(100, 100, 100, 100, 10000);

            uint clients = (uint)UnityEngine.Random.Range(15, 100);
            int brandPower = UnityEngine.Random.Range(0, 15);

            int productLevel = 0;
            int explorationLevel = productLevel;

            // product specific components
            company.AddProduct(company.company.Id, name, niche, productLevel);
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

        public static T RandomItem<T>(T[] items)
        {
            int index = UnityEngine.Random.Range(0, items.Length);

            return items[index];
        }

        public static string GenerateInvestmentCompanyName()
        {
            string[] names = new string[] { "Investments", "Capitals", "Funds", "and partners" };

            int length = UnityEngine.Random.Range(4, 8);

            return "The " + RandomString(length, true) + " " + RandomItem(names);
        }
    }
}
