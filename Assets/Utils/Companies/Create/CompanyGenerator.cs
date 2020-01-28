using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    partial class Companies
    {
        public static GameEntity CreateProduct(GameContext context, GameEntity company, NicheType niche)
        {
            company.AddProduct(niche, 0);

            // positioning
            int positionings = Markets.GetNichePositionings(niche, context).Count;
            company.AddProductPositioning(Random.Range(0, positionings));

            // development
            company.AddFeatures(new Dictionary<ProductFeature, int> { [ProductFeature.Acquisition] = 0, [ProductFeature.Monetisation] = 0, [ProductFeature.Retention] = 0 }, 0);
            company.AddExpertise(Random.Range(1, 4));

            company.AddFinancing(new Dictionary<Financing, int> { [Financing.Development] = 0, [Financing.Marketing] = 0, [Financing.Team] = 0 });

            // clients
            var flow = MarketingUtils.GetBaseClientsForNewCompanies(context, niche);
            var baseClients = Random.Range(0.15f, 0.35f) * flow;
            company.AddMarketing((long)baseClients);

            // sphere of interest
            var industry = Markets.GetIndustry(niche, context);

            AddFocusNiche(niche, company, context);
            AddFocusIndustry(industry, company);


            Investments.SetCompanyGoal(context, company, InvestorGoal.Operationing);

            return company;
        }




        private static GameEntity CreateCompany(
            GameContext context,
            string name,
            CompanyType companyType,
            Dictionary<int, BlockOfShares> founders,
            GameEntity CEO)
        {
            var e = context.CreateEntity();

            int id = GenerateCompanyId(context);

            e.AddCompany(id, name, companyType);
            e.isAlive = true;
            e.isIndependentCompany = true;
            e.AddPartnerships(new List<int>());


            e.AddCompanyResource(new TeamResource(100, 100, 100, 100, 10000));

            // investments
            e.AddShareholders(founders);
            e.AddInvestmentProposals(new List<InvestmentProposal>());
            e.AddInvestmentRounds(InvestmentRound.Preseed);
            e.AddCompanyGoal(InvestorGoal.GrowCompanyCost, 1000000);


            e.AddBranding(0);

            // team
            int CeoID = CEO.human.Id;
            e.AddCEO(0, CeoID);
            e.AddTeam(100, 50, new Dictionary<int, WorkerRole>(), new Dictionary<WorkerRole, int> { [WorkerRole.Programmer] = 0 }, TeamStatus.Solo);
            e.AddEmployee(new Dictionary<int, WorkerRole>());

            Teams.AttachToTeam(e, CEO, WorkerRole.CEO);

            // culture
            var culture = GetRandomCorporateCulture();
            e.AddCorporateCulture(culture);


            e.AddCompanyFocus(new List<NicheType>(), new List<IndustryType>());

            e.AddMetricsHistory(new List<MetricsInfo>());
            e.AddCooldowns(new List<Cooldown>());

            return e;
        }

        public static Dictionary<CorporatePolicy, int> GetRandomCorporateCulture()
        {
            var max = Balance.CORPORATE_CULTURE_LEVEL_MAX + 1;
            var min = Balance.CORPORATE_CULTURE_LEVEL_MIN;

            return new Dictionary<CorporatePolicy, int>()
            {
                [CorporatePolicy.LeaderOrTeam]   = Random.Range(min, max),
                [CorporatePolicy.WorkerMindset]  = Random.Range(min, max),
                [CorporatePolicy.Focusing]       = Random.Range(min, max),
                [CorporatePolicy.BuyOrCreate]    = Random.Range(min, max),
            };
        }

        public static Dictionary<CorporatePolicy, int> GetFundCorporateCulture()
        {
            var max = Balance.CORPORATE_CULTURE_LEVEL_MAX + 1;
            var min = Balance.CORPORATE_CULTURE_LEVEL_MIN;
            var half = max / 2;

            return new Dictionary<CorporatePolicy, int>()
            {
                [CorporatePolicy.LeaderOrTeam]   = Random.Range(min, max),
                [CorporatePolicy.WorkerMindset]  = Random.Range(min, half),
                [CorporatePolicy.Focusing]       = Random.Range(min, max),
                [CorporatePolicy.BuyOrCreate]    = Random.Range(min, half),
            };
        }
    }
}
