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
            e.AddCompanyResource(new TeamResource(100, 100, 100, 100, 0));

            e.AddShareholders(founders);
            e.AddInvestmentProposals(new List<InvestmentProposal>());
            e.AddMetricsHistory(new List<MetricsInfo>());

            e.AddInvestmentRounds(InvestmentRound.Preseed);
            e.isIndependentCompany = true;

            e.AddCompanyGoal(InvestorGoal.GrowProfit);

            e.AddCEO(0, CeoID);


            Dictionary<CooldownType, Cooldown> cooldowns = new Dictionary<CooldownType, Cooldown>();

            cooldowns[CooldownType.TargetingActivity] = new Cooldown { EndDate = 10 };
            e.AddCooldowns(cooldowns);

            return e;
        }

        public static GameEntity GenerateProduct(GameContext context, GameEntity company, string name, NicheType niche)
        {
            IndustryType industry = NicheUtils.GetIndustry(niche, context);

            var resources = new TeamResource(100, 100, 100, 10, 10000);

            long clients = UnityEngine.Random.Range(15, 100);
            int brandPower = UnityEngine.Random.Range(0, 15);

            int productLevel = 0;
            int improvements = 0;

            var Segments = new Dictionary<UserType, long>
            {
                [UserType.Newbie] = clients,
                [UserType.Regular] = 0,
                [UserType.Core] = 0,
            };

            var SegmentsFeatures = new Dictionary<UserType, int>
            {
                [UserType.Newbie] = productLevel,
                [UserType.Regular] = productLevel,
                [UserType.Core] = productLevel,
            };

            // product specific components
            company.AddProduct(company.company.Id, name, niche, productLevel, improvements, SegmentsFeatures);
            company.AddDevelopmentFocus(DevelopmentFocus.Concept);
            company.AddFinance(0, 0, 0, 5f);
            company.AddTeam(1, 0, 0, 100);
            company.AddMarketing(brandPower, Segments);
            company.AddTargetUserType(UserType.Core);

            company.ReplaceCompanyGoal(InvestorGoal.BecomeMarketFit);

            return company;
        }
    }
}
