using Assets.Classes;
using System.Collections.Generic;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        private static GameEntity CreateCompany(GameContext context, string name, CompanyType companyType, Dictionary<int, int> founders, int CeoID)
        {
            var e = context.CreateEntity();

            int id = GenerateCompanyId(context);

            e.AddCompany(id, name, companyType);
            e.AddCompanyResource(new TeamResource(100, 100, 100, 100, 0));

            Dictionary<int, InvestorGoal> goals = new Dictionary<int, InvestorGoal>();

            e.AddShareholders(founders, goals);
            e.AddInvestmentProposals(new List<InvestmentProposal>());
            e.AddMetricsHistory(new List<MetricsInfo>());

            e.AddInvestmentRounds(InvestmentRound.Preseed);
            e.isIndependentCompany = true;

            e.AddCEO(0, CeoID);

            return e;
        }

        public static GameEntity GenerateProduct(GameContext context, GameEntity company, string name, NicheType niche)
        {
            IndustryType industry = NicheUtils.GetIndustry(niche, context);

            var resources = new TeamResource(100, 100, 100, 100, 10000);

            long clients = UnityEngine.Random.Range(15, 100);
            int brandPower = UnityEngine.Random.Range(0, 15);

            int productLevel = 0;
            int improvements = 0;

            Dictionary<NicheType, long> Segments = new Dictionary<NicheType, long>
            {
                [niche] = clients
            };

            Dictionary<NicheType, int> SegmentsFeatures = new Dictionary<NicheType, int>
            {
                [niche] = productLevel
            };

            // product specific components
            company.AddProduct(company.company.Id, name, niche, productLevel, improvements, SegmentsFeatures);
            company.AddFinance(0, 0, 0, 5f);
            company.AddTeam(1, 0, 0, 100);
            company.AddMarketing(clients, brandPower, false, Segments);

            return company;
        }
    }
}
