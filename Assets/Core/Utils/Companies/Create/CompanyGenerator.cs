using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    partial class Companies
    {
        public static GameEntity CreateProduct(GameContext context, GameEntity product, NicheType niche)
        {
            product.AddProduct(niche, 0);
            product.AddProductUpgrades(new Dictionary<ProductUpgrade, bool>
            {
                [ProductUpgrade.SimpleConcept] = true,
                [ProductUpgrade.PlatformWeb] = true,
            });

            // positioning
            int positionings = Markets.GetNichePositionings(niche, context).Count;
            product.AddProductPositioning(Random.Range(0, positionings));

            // development
            product.AddFeatures(
                new Dictionary<string, float> { },
                0);
            product.AddSupportUpgrades(new Dictionary<string, int>());
            product.AddExpertise(Random.Range(1, 4));

            // clients
            var flow = Marketing.GetBaseClientsForNewCompanies(context, niche);
            var baseClients = Random.Range(0.15f, 0.35f) * flow;


            var audiences = Marketing.GetAudienceInfos();
            var coreId = Random.Range(0, audiences.Count);
            product.AddProductTargetAudience(coreId);

            var clientList = new Dictionary<int, long>
            {
                [coreId] = 50
            };

            product.AddMarketing((long)baseClients, clientList);

            product.AddCompanyMarketingActivities(new Dictionary<int, long>());
            product.AddSourceOfClients(new Dictionary<int, long>());

            // sphere of interest
            var industry = Markets.GetIndustry(niche, context);

            AddFocusNiche(niche, product, context);
            AddFocusIndustry(industry, product);


            Investments.SetCompanyGoal(context, product, InvestorGoal.Prototype);

            return product;
        }




        private static GameEntity CreateCompany(
            GameContext context,
            string name,
            CompanyType companyType,
            Dictionary<int, BlockOfShares> founders,
            GameEntity CEO)
        {
            var company = context.CreateEntity();

            int id = GenerateCompanyId(context);

            company.AddCompany(id, name, companyType);
            company.isAlive = true;
            company.isIndependentCompany = true;
            company.AddPartnerships(new List<int>());
            company.AddCompanyStrategies(RandomEnum<CompanySettingGrowthType>.GenerateValue(), RandomEnum<CompanySettingAttitudeToWorkers>.GenerateValue(), RandomEnum<CompanySettingControlDesire>.GenerateValue());

            company.AddCompanyResource(new TeamResource(100, 100, 100, 100, 10000));

            // investments
            company.AddShareholders(founders);
            company.AddInvestmentProposals(new List<InvestmentProposal>());
            company.AddInvestmentRounds(InvestmentRound.Preseed);
            company.AddCompanyGoal(InvestorGoal.GrowCompanyCost, 1000000);


            company.AddBranding(0);

            // team
            int CeoID = CEO.human.Id;
            company.AddCEO(0, CeoID);
            company.AddTeam(
                100, 50,
                new Dictionary<int, WorkerRole>(),
                new Dictionary<WorkerRole, int> { [WorkerRole.Programmer] = 0 },
                new List<TeamInfo>()
                {
                    
                }
                );
            company.AddEmployee(new Dictionary<int, WorkerRole>());
            Teams.AddTeam(company, TeamType.CrossfunctionalTeam);
            Teams.AttachToTeam(company, CEO, WorkerRole.CEO, 0);

            // culture
            var culture = GetRandomCorporateCulture();
            company.AddCorporateCulture(culture);


            company.AddCompanyFocus(new List<NicheType>(), new List<IndustryType>());

            company.AddMetricsHistory(new List<MetricsInfo>());
            //e.AddCooldowns(new List<Cooldown>());

            return company;
        }

        public static Dictionary<CorporatePolicy, int> GetRandomWorkerCorporateCulture()
        {
            var max = C.CORPORATE_CULTURE_LEVEL_MAX + 1;
            var half = max / 2;


            var culture = GetRandomCorporateCulture();

            culture[CorporatePolicy.SalariesLowOrHigh] = Random.Range(half, max);
            culture[CorporatePolicy.Make] = Random.Range(half - 1, max);

            return culture;
        }

        public static Dictionary<CorporatePolicy, int> GetRandomCorporateCulture()
        {
            var max = C.CORPORATE_CULTURE_LEVEL_MAX + 1;
            var min = C.CORPORATE_CULTURE_LEVEL_MIN;

            var dict = new Dictionary<CorporatePolicy, int>();

            foreach (var e in (CorporatePolicy[])System.Enum.GetValues(typeof(CorporatePolicy)))
                dict[e] = Random.Range(min, max);

            return dict;
        }

        public static Dictionary<CorporatePolicy, int> GetFundCorporateCulture()
        {
            var max = C.CORPORATE_CULTURE_LEVEL_MAX + 1;
            var min = C.CORPORATE_CULTURE_LEVEL_MIN;
            var half = max / 2;

            return GetRandomCorporateCulture();
            //new Dictionary<CorporatePolicy, int>()
            //{
            //    [CorporatePolicy.CompetitionOrSupport]  = Random.Range(min, max),
            //    [CorporatePolicy.BuyOrCreate]           = Random.Range(min, max),
            //    [CorporatePolicy.SalariesLowOrHigh]     = Random.Range(min, max),
            //};
        }
    }
}
