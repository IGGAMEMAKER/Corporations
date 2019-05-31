using Assets.Classes;
using System.Collections.Generic;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        private static GameEntity CreateCompany(GameContext context, string name, CompanyType companyType, Dictionary<int, BlockOfShares> founders, int CeoID)
        {
            var e = context.CreateEntity();

            int id = GenerateCompanyId(context);

            e.AddCompany(id, name, companyType);
            e.AddCompanyResource(new TeamResource(100, 100, 100, 100, 100000));

            e.AddShareholders(founders);
            e.AddInvestmentProposals(new List<InvestmentProposal>());
            e.AddMetricsHistory(new List<MetricsInfo>());

            e.AddInvestmentRounds(InvestmentRound.Preseed);
            e.isIndependentCompany = true;

            e.AddCompanyGoal(InvestorGoal.GrowCompanyCost, ScheduleUtils.GetCurrentDate(context) + 360, 1000000);

            e.AddCEO(0, CeoID);

            e.AddTeam(100, new Dictionary<int, WorkerRole>());

            TeamUtils.AttachToTeam(e, CeoID, WorkerRole.Universal);

            e.AddCooldowns(new Dictionary<CooldownType, Cooldown>());

            return e;
        }

        public static void AddCooldown(GameContext gameContext, GameEntity company, CooldownType cooldownType, int duration)
        {
            var c = company.cooldowns.Cooldowns;

            if (c.ContainsKey(cooldownType))
                return;

            c[cooldownType] = new Cooldown { EndDate = ScheduleUtils.GetCurrentDate(gameContext) + duration };

            company.ReplaceCooldowns(c);
        }

        public static GameEntity GenerateProduct(GameContext context, GameEntity company, string name, NicheType niche)
        {
            IndustryType industry = NicheUtils.GetIndustry(niche, context);

            long clients = UnityEngine.Random.Range(15, 100);
            int brandPower = UnityEngine.Random.Range(0, 15);

            int productLevel = 0;
            int improvements = 0;

            var Segments = new Dictionary<UserType, long>
            {
                [UserType.Core] = 0,
                [UserType.Regular] = 0,
            };

            var SegmentsFeatures = new Dictionary<UserType, int>
            {
                [UserType.Core] = 0,
                [UserType.Regular] = 0,
            };

            // product specific components
            company.AddProduct(company.company.Id, name, niche, productLevel, improvements, SegmentsFeatures);
            company.AddDevelopmentFocus(DevelopmentFocus.Concept);
            company.AddFinance(0, 0, 0, 5f);
            company.AddMarketing(brandPower, Segments);

            SetCompanyGoal(context, company, InvestorGoal.BecomeMarketFit, 365);

            LockCompanyGoal(context, company);

            return company;
        }
    }
}
