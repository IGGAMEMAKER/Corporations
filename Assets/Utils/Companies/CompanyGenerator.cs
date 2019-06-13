using Assets.Classes;
using System.Collections.Generic;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        private static GameEntity CreateCompany(GameContext context, string name, CompanyType companyType, Dictionary<int, BlockOfShares> founders, GameEntity CEO)
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


            TeamUtils.AttachToTeam(e, CeoID, WorkerRole.Universal);

            HumanUtils.SetSkills(CEO, WorkerRole.Business);

            HumanUtils.AttachToCompany(CEO, id, WorkerRole.Universal);

            return e;
        }

        public static GameEntity GenerateProduct(GameContext context, GameEntity company, string name, NicheType niche)
        {
            int brandPower = UnityEngine.Random.Range(0, 15);

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
            company.AddProduct(company.company.Id, name, niche, 0, SegmentsFeatures);
            company.AddDevelopmentFocus(DevelopmentFocus.Concept);
            company.AddFinance(0, 0, 0, 5f);
            company.AddMarketing(brandPower, Segments);

            InvestmentUtils.SetCompanyGoal(context, company, InvestorGoal.Prototype, 365);

            return company;
        }
    }
}
