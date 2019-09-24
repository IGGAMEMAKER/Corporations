using Assets.Classes;
using System.Collections.Generic;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static GameEntity CreateProduct(GameContext context, GameEntity company, NicheType niche)
        {
            var GOAL = InvestorGoal.Operationing;

            // product specific components
            company.AddProduct(company.company.Id, niche, 0); //  UnityEngine.Random.Range(0, 6)
            company.AddExpertise(UnityEngine.Random.Range(1, 4)); //  UnityEngine.Random.Range(0, 6)
            company.AddDevelopmentFocus(DevelopmentFocus.Concept);


            company.AddMarketing(0);

            AddFocusNiche(niche, company, context);

            var industry = NicheUtils.GetIndustry(niche, context);
            AddFocusIndustry(industry, company);


            InvestmentUtils.SetCompanyGoal(context, company, GOAL);


            int positionings = NicheUtils.GetNichePositionings(niche, context).Count;

            company.AddProductPositioning(UnityEngine.Random.Range(0, positionings));

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

            e.isAlive = true;

            int id = GenerateCompanyId(context);
            e.AddCooldowns(new List<Cooldown>());

            e.AddCompany(id, name, companyType);
            e.AddCompanyResource(new TeamResource(100, 100, 100, 100, 10000));

            e.AddShareholders(founders);
            e.AddInvestmentProposals(new List<InvestmentProposal>());
            e.AddMetricsHistory(new List<MetricsInfo>());

            e.AddInvestmentRounds(InvestmentRound.Preseed);
            e.isIndependentCompany = true;

            e.AddCompanyGoal(InvestorGoal.GrowCompanyCost, 1000000);

            e.AddBranding(0);

            e.AddTeamImprovements(new Dictionary<TeamUpgrade, int>());

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
