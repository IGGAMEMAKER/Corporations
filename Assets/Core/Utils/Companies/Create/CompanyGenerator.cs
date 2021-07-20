using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    partial class Companies
    {
        public static GameEntity CreateProduct(GameContext context, GameEntity product, NicheType nicheType)
        {
            product.AddProduct(nicheType, 0);

            var niche = Markets.Get(context, nicheType);

            // market state
            product.AddNicheState(Markets.GetMarketState(niche), 100);

            Markets.GetMarketRequirementsForCompany(context, product);

            // product.AddNicheSegments(Markets.GetNichePositionings(nicheType, context));
            // product.AddNicheBaseProfile(Markets.Get(context, product).nicheBaseProfile.Profile);

            product.AddProductUpgrades(new Dictionary<ProductUpgrade, bool>
            {
                [ProductUpgrade.SimpleConcept] = true,
                [ProductUpgrade.PlatformWeb] = true,
            });

            // positioning
            var audiences = Marketing.GetAudienceInfos();
            var coreId = Random.Range(0, audiences.Count);

            product.AddProductPositioning(coreId);

            // development
            product.AddFeatures(new Dictionary<string, float>());
            product.AddSupportUpgrades(new Dictionary<string, int>());
            product.AddExpertise(Random.Range(1, 4));


            // var serverFeature = Products.GetHighloadFeatures(product)[0];
            // Teams.AddTeamTask(product, ScheduleUtils.GetCurrentDate(context), context, 0, new TeamTaskSupportFeature(serverFeature));

            // clients
            product.AddMarketing(new Dictionary<int, long>());
            product.AddSourceOfClients(new Dictionary<int, long>());
            product.AddCompanyMarketingActivities(new Dictionary<int, long>());

            // Markets.CopyChannelInfosToProductCompany(product, context);

            Marketing.AddClients(product, 50);

            // sphere of interest
            AddFocusNiche(product, nicheType, context);
            AddFocusIndustry(Markets.GetIndustry(nicheType, context), product);
            
            WrapProductWithAdditionalData(product, context);

            return product;
        }

        public static void WrapProductWithAdditionalData(GameEntity product, GameContext gameContext)
        {
            if (!product.hasNicheSegments)
                product.AddNicheSegments(Markets.GetNichePositionings(product.product.Niche, gameContext));
            
            if (!product.hasNicheBaseProfile)
                product.AddNicheBaseProfile(Markets.Get(gameContext, product).nicheBaseProfile.Profile);
            
            Markets.CopyChannelInfosToProductCompany(product, gameContext);
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

            var culture = GetRandomCorporateCulture();
            company.AddCorporateCulture(culture);


            SetIndependence(company, true);
            company.AddPartnerships(new List<int>());

            company.AddCompanyResourceHistory(new List<ResourceTransaction>());
            company.AddCompanyResource(new TeamResource());
            Companies.SetResources(company, new TeamResource(0, Teams.GetPromotionCost(TeamRank.Solo), 0, 0, 10000), "Initial capital");

            // investments
            company.AddShareholders(founders);
            company.AddInvestmentProposals(new List<InvestmentProposal>());
            company.AddInvestmentRounds(InvestmentRound.Preseed);
            company.AddCompanyGoal(new List<InvestmentGoal>()); // new InvestmentGoalGrowCost(1000000)
            company.AddCompletedGoals(new List<InvestorGoalType>());

            // Branding?
            company.AddBranding(0);

            // teams
            company.AddWorkerOffers(new List<ExpiringJobOffer>());
            company.AddTeam(
                100, 50,
                new Dictionary<int, WorkerRole>(),
                new Dictionary<WorkerRole, int> { [WorkerRole.Programmer] = 0 },
                new List<TeamInfo>(),
                0
                );

            // add team for CEO
            Teams.AddTeam(company, context, TeamType.CrossfunctionalTeam);
            company.team.Teams[0].Workers = Teams.GetMaxTeamSize(TeamRank.Solo);
            company.team.Teams[0].HiringProgress = 100;


            // CEO
            int CeoID = CEO.human.Id;

            CEO.humanSkills.Traits.RemoveAll(t => t == Trait.Greedy);
            CEO.humanSkills.Traits.Add(Trait.Shy);

            company.AddCEO(0, CeoID);
            company.AddEmployee(new Dictionary<int, WorkerRole>());

            company.AddTeamEfficiency(new TeamEfficiency());

            Teams.AttachToCompany(company, context, CEO, WorkerRole.CEO, 0);

            Teams.SetJobOffer(CEO, company, new JobOffer(0), 0, context);

            // uniqueness

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

        public static Dictionary<CorporatePolicy, int> GetPlayerCorporateCulture()
        {
            var max = C.CORPORATE_CULTURE_LEVEL_MAX;
            var half = max / 2;

            max--;

            return new Dictionary<CorporatePolicy, int>
            {
                [CorporatePolicy.CompetitionOrSupport] = half,
                [CorporatePolicy.SalariesLowOrHigh] = half,
                [CorporatePolicy.DoOrDelegate] = 1,
                [CorporatePolicy.DecisionsManagerOrTeam] = max,
                [CorporatePolicy.PeopleOrProcesses] = half,
                [CorporatePolicy.FocusingOrSpread] = 1, // doesn't matter
                [CorporatePolicy.Make] = 1,
                [CorporatePolicy.HardSkillsOrSoftSkills] = half
            };
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
