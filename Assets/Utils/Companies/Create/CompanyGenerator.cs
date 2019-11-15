using Assets.Classes;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static GameEntity CreateProduct(GameContext context, GameEntity company, NicheType niche)
        {
            var GOAL = InvestorGoal.Operationing;

            // product specific components
            company.AddProduct(niche, 0); //  UnityEngine.Random.Range(0, 6)

            company.AddProductImprovements(new Dictionary<ProductImprovement, int> { [ProductImprovement.Acquisition] = 0, [ProductImprovement.Monetisation] = 0, [ProductImprovement.Retention] = 0 }, 0);
            company.AddExpertise(Random.Range(1, 4)); //  UnityEngine.Random.Range(0, 6)

            var flow = MarketingUtils.GetClientFlow(context, niche);
            var baseClients = Random.Range(flow / 2, flow * 2);
            company.AddMarketing((long)baseClients);
            company.AddFinancing(new Dictionary<Financing, int>
            {
                [Financing.Development] = 0,
                [Financing.Marketing] = 0,
                [Financing.Team] = 0
            });

            AddFocusNiche(niche, company, context);

            var industry = NicheUtils.GetIndustry(niche, context);
            AddFocusIndustry(industry, company);


            InvestmentUtils.SetCompanyGoal(context, company, GOAL);

            int positionings = NicheUtils.GetNichePositionings(niche, context).Count;

            company.AddProductPositioning(Random.Range(0, positionings));

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

            var culture = new Dictionary<CorporatePolicy, int>()
            {
                [CorporatePolicy.Responsibility] = Random.Range(1, 6),
                [CorporatePolicy.WorkerMindset] = Random.Range(1, 6),
                [CorporatePolicy.Focusing] = Random.Range(1, 6),
                [CorporatePolicy.CreateOrBuy] = Random.Range(1, 6),
            };
            e.AddCorporateCulture(culture);

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
            HumanUtils.AttachToCompany(CEO, id, WorkerRole.Universal);


            e.AddCompanyFocus(new List<NicheType>(), new List<IndustryType>());

            e.AddMetricsHistory(new List<MetricsInfo>());
            e.AddCooldowns(new List<Cooldown>());

            return e;
        }
    }
}
