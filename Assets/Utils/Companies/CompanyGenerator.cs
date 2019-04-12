using Assets.Classes;
using Entitas;
using System.Collections.Generic;

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

        public static GameEntity CreateCompanyEntity(GameContext context, string name, CompanyType companyType, Dictionary<int, int> founders = null)
        {
            var e = context.CreateEntity();

            int id = GenerateCompanyId(context);

            e.AddCompany(id, name, companyType);
            e.AddCompanyResource(new TeamResource());

            if (founders == null)
                founders = new Dictionary<int, int>();

            e.AddShareholders(founders);

            return e;
        }

        public static int GenerateInvestmentFund(GameContext context, string name, long money)
        {
            var e = context.CreateEntity();

            int id = GenerateCompanyId(context); // GenerateId();
            int investorId = GenerateInvestorId(context);

            e.AddCompany(id, name, CompanyType.FinancialGroup);
            BecomeInvestor(context, e, money);

            return investorId;
        }

        public static int GenerateHoldingCompany(GameContext context, string name)
        {
            var e = context.CreateEntity();

            int id = GenerateCompanyId(context); // GenerateId();

            e.AddCompany(id, name, CompanyType.Holding);
            BecomeInvestor(context, e, 0);

            return id;
        }

        public static int GenerateCompanyGroup(GameContext context, string name, Dictionary<int, int> founders = null)
        {
            var e = CreateCompanyEntity(context, name, CompanyType.Group, founders);

            BecomeInvestor(context, e, 0);

            return e.company.Id;
        }

        public static int GenerateProductCompany(GameContext context, string name, NicheType niche)
        {
            int id = GenerateCompanyId(context); // GenerateId();

            GenerateProduct(context, name, niche, id);

            return id;
        }

        public static void GenerateProduct(GameContext context, string name, NicheType niche, int id)
        {
            IndustryType industry = NicheUtils.GetIndustry(niche, context);

            var resources = new TeamResource(100, 100, 100, 100, 10000);

            uint clients = (uint)UnityEngine.Random.Range(0, 10000);
            int brandPower = UnityEngine.Random.Range(0, 15);

            int productLevel = 0;
            int explorationLevel = productLevel;

            var e = context.CreateEntity();
            e.AddCompany(id, name, CompanyType.ProductCompany);

            // product specific components
            e.AddProduct(id, name, niche, industry, productLevel, explorationLevel, resources);
            e.AddFinance(0, 0, 0, 5f);
            e.AddTeam(1, 0, 0, 100);
            e.AddMarketing(clients, brandPower, false);
            e.AddCompanyResource(new TeamResource());
        }
    }
}
