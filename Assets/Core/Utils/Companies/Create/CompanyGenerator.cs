using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    partial class Companies
    {
        public static GameEntity CreateProduct(GameContext context, GameEntity product, NicheType niche)
        {
            product.AddProduct(niche, 0);

            // market state
            product.AddNicheState(Markets.GetMarketState(context, niche), 100);
            product.AddNicheSegments(Markets.GetNichePositionings(niche, context));
            product.AddNicheBaseProfile(Markets.Get(context, product).nicheBaseProfile.Profile);

            product.AddProductUpgrades(new Dictionary<ProductUpgrade, bool>
            {
                [ProductUpgrade.SimpleConcept] = true,
                [ProductUpgrade.PlatformWeb] = true,
            });

            // positioning
            //int positionings = Markets.GetNichePositionings(niche, context).Count;

            var audiences = Marketing.GetAudienceInfos();
            var coreId = Random.Range(0, audiences.Count);

            product.AddProductPositioning(coreId);

            // development
            product.AddFeatures(
                new Dictionary<string, float> { },
                0);
            product.AddSupportUpgrades(new Dictionary<string, int>());
            product.AddExpertise(Random.Range(1, 4));


            var serverFeature = Products.GetHighloadFeatures(product)[0];
            Teams.AddTeamTask(product, context, 0, new TeamTaskSupportFeature(serverFeature));

            // clients
            product.AddMarketing(new Dictionary<int, long>());
            product.AddSourceOfClients(new Dictionary<int, long>());
            product.AddCompanyMarketingActivities(new Dictionary<int, long>());

            Marketing.AddClients(product, 50, coreId);

            // sphere of interest
            var industry = Markets.GetIndustry(niche, context);

            AddFocusNiche(niche, product, context);
            AddFocusIndustry(industry, product);

            Investments.AddCompanyGoal(product, new InvestmentGoalMakePrototype());

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

            SetIndependence(company, true);
            company.AddPartnerships(new List<int>());

            company.AddCompanyResourceHistory(new List<ResourceTransaction>());
            company.AddCompanyResource(new TeamResource());
            Companies.SetResources(company, new TeamResource(100, 100, 100, 100, 10000), "Initial capital");

            // investments
            company.AddShareholders(founders);
            company.AddInvestmentProposals(new List<InvestmentProposal>());
            company.AddInvestmentRounds(InvestmentRound.Preseed);
            company.AddCompanyGoal(new List<InvestmentGoal> { new InvestmentGoalGrowCost(1000000) });
            company.AddCompletedGoals(new List<InvestorGoalType>());

            // Branding?
            company.AddBranding(0);

            // teams
            company.AddWorkerOffers(new List<ExpiringJobOffer>());
            company.AddTeam(
                100, 50,
                new Dictionary<int, WorkerRole>(),
                new Dictionary<WorkerRole, int> { [WorkerRole.Programmer] = 0 },
                new List<TeamInfo>() {}
                );

            // add team for CEO
            Teams.AddTeam(company, TeamType.SmallCrossfunctionalTeam);

            // CEO
            int CeoID = CEO.human.Id;

            CEO.humanSkills.Traits.RemoveAll(t => t == Trait.Greedy);
            CEO.humanSkills.Traits.Add(Trait.Shy);

            company.AddCEO(0, CeoID);
            company.AddEmployee(new Dictionary<int, WorkerRole>());

            company.AddTeamEfficiency(new TeamEfficiency());

            Teams.AttachToCompany(company, CEO, WorkerRole.CEO, 0);

            Teams.SetJobOffer(CEO, company, new JobOffer(0), 0);

            // uniqueness
            var culture = GetRandomCorporateCulture();
            company.AddCorporateCulture(culture);
            company.AddCompanyStrategies(RandomEnum<CompanySettingGrowthType>.GenerateValue(), RandomEnum<CompanySettingAttitudeToWorkers>.GenerateValue(), RandomEnum<CompanySettingControlDesire>.GenerateValue());


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
