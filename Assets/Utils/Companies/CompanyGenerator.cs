using Assets.Classes;
using System.Collections.Generic;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static GameEntity CreateProduct(GameContext context, GameEntity company, NicheType niche)
        {
            var GOAL = InvestorGoal.GrowCompanyCost;


            int brandPower = 0;

            var Segments = new Dictionary<UserType, long>
            {
                [UserType.Core] = 0,
                [UserType.Regular] = 0,
                [UserType.Mass] = 0,
            };

            var SegmentsFeatures = new Dictionary<UserType, int>
            {
                [UserType.Core] = 0,
                [UserType.Regular] = 0,
                [UserType.Mass] = 0,
            };

            // product specific components
            company.AddProduct(company.company.Id, niche, SegmentsFeatures);
            company.AddDevelopmentFocus(DevelopmentFocus.Concept);
            company.AddFinance(0, 0, 0, 5f);
            company.AddMarketing(brandPower, Segments);

            AddFocusNiche(niche, company);

            var industry = NicheUtils.GetIndustry(niche, context);
            AddFocusIndustry(industry, company);
            InvestmentUtils.SetCompanyGoal(context, company, GOAL, 365);

            return company;
        }

        public static void AddFocusNiche(NicheType nicheType, GameEntity company)
        {
            var niches = company.companyFocus.Niches;

            if (niches.Contains(nicheType))
                return;

            niches.Add(nicheType);

            company.ReplaceCompanyFocus(niches, company.companyFocus.Industries);
        }

        public static void AddFocusIndustry(IndustryType industryType, GameEntity company)
        {
            var industries = company.companyFocus.Industries;

            if (industries.Contains(industryType))
                return;

            industries.Add(industryType);

            company.ReplaceCompanyFocus(company.companyFocus.Niches, industries);
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
            e.AddCooldowns(new List<Cooldown>());

            e.AddCompany(id, name, companyType);
            e.AddCompanyResource(new TeamResource(100, 100, 100, 100, 100000));

            e.AddShareholders(founders);
            e.AddInvestmentProposals(new List<InvestmentProposal>());
            e.AddMetricsHistory(new List<MetricsInfo>());

            e.AddInvestmentRounds(InvestmentRound.Preseed);
            e.isIndependentCompany = true;

            e.AddCompanyGoal(InvestorGoal.GrowCompanyCost, ScheduleUtils.GetCurrentDate(context) + 360, 1000000);

            int CeoID = CEO.human.Id;
            e.AddCEO(0, CeoID);

            e.AddTeam(100, new Dictionary<int, WorkerRole>(), TeamStatus.Solo);

            e.AddCompanyFocus(new List<NicheType>(), new List<IndustryType>());

            TeamUtils.AttachToTeam(e, CeoID, WorkerRole.Universal);

            HumanUtils.SetSkills(CEO, WorkerRole.Business);

            HumanUtils.AttachToCompany(CEO, id, WorkerRole.Universal);

            return e;
        }
    }
}
