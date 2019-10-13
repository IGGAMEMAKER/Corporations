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
            company.AddProduct(niche, 0); //  UnityEngine.Random.Range(0, 6)
            company.AddProductImprovements(0, 0, 0);
            company.AddExpertise(UnityEngine.Random.Range(1, 4)); //  UnityEngine.Random.Range(0, 6)


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
            e.isIndependentCompany = true;

            int id = GenerateCompanyId(context);

            e.AddCompany(id, name, companyType);
            e.AddCompanyResource(new TeamResource(100, 100, 100, 100, 10000));

            e.AddShareholders(founders);
            e.AddInvestmentProposals(new List<InvestmentProposal>());
            e.AddInvestmentRounds(InvestmentRound.Preseed);
            e.AddCompanyGoal(InvestorGoal.GrowCompanyCost, 1000000);


            e.AddBranding(0);

            int CeoID = CEO.human.Id;
            e.AddCEO(0, CeoID);
            e.AddTeam(100, new Dictionary<int, WorkerRole>(), TeamStatus.Solo);
            TeamUtils.AttachToTeam(e, CeoID, WorkerRole.Universal);
            HumanUtils.SetSkills(CEO, WorkerRole.Business);
            //HumanUtils.AttachToCompany(CEO, id, WorkerRole.Universal);


            e.AddCompanyFocus(new List<NicheType>(), new List<IndustryType>());

            e.AddMetricsHistory(new List<MetricsInfo>());
            e.AddCooldowns(new List<Cooldown>());

            return e;
        }
    }
}
