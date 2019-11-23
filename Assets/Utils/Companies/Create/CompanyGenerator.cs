﻿using Assets.Classes;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static GameEntity CreateProduct(GameContext context, GameEntity company, NicheType niche)
        {
            // positioning
            company.AddProduct(niche, 0);
            int positionings = NicheUtils.GetNichePositionings(niche, context).Count;
            company.AddProductPositioning(Random.Range(0, positionings));

            // development
            company.AddProductImprovements(new Dictionary<ProductImprovement, int> { [ProductImprovement.Acquisition] = 0, [ProductImprovement.Monetisation] = 0, [ProductImprovement.Retention] = 0 }, 0);
            company.AddExpertise(Random.Range(1, 4));
            company.AddFinancing(new Dictionary<Financing, int> { [Financing.Development] = 0, [Financing.Marketing] = 0, [Financing.Team] = 0 });

            // clients
            var flow = MarketingUtils.GetClientFlow(context, niche);
            var baseClients = Random.Range(flow / 2, flow);
            company.AddMarketing((long)baseClients);

            // sphere of interest
            var industry = NicheUtils.GetIndustry(niche, context);

            AddFocusNiche(niche, company, context);
            AddFocusIndustry(industry, company);


            InvestmentUtils.SetCompanyGoal(context, company, InvestorGoal.Operationing);

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

            int id = GenerateCompanyId(context);

            e.AddCompany(id, name, companyType);
            e.isAlive = true;
            e.isIndependentCompany = true;
            e.AddPartnerships(new List<int>());


            e.AddCompanyResource(new TeamResource(100, 100, 100, 100, 10000));

            // investments
            e.AddShareholders(founders);
            e.AddInvestmentProposals(new List<InvestmentProposal>());
            e.AddInvestmentRounds(InvestmentRound.Preseed);
            e.AddCompanyGoal(InvestorGoal.GrowCompanyCost, 1000000);


            e.AddBranding(0);

            // team
            int CeoID = CEO.human.Id;
            e.AddCEO(0, CeoID);
            e.AddTeam(100, new Dictionary<int, WorkerRole>(), TeamStatus.Solo);
            TeamUtils.AttachToTeam(e, CeoID, WorkerRole.Universal);
            HumanUtils.SetSkills(CEO, WorkerRole.Business);
            HumanUtils.AttachToCompany(CEO, id, WorkerRole.Universal);

            // culture
            var culture = new Dictionary<CorporatePolicy, int>()
            {
                [CorporatePolicy.Responsibility] = Random.Range(1, 6),
                [CorporatePolicy.WorkerMindset] = Random.Range(1, 6),
                [CorporatePolicy.Focusing] = Random.Range(1, 6),
                [CorporatePolicy.CreateOrBuy] = Random.Range(1, 6),
            };
            e.AddCorporateCulture(culture);


            e.AddCompanyFocus(new List<NicheType>(), new List<IndustryType>());

            e.AddMetricsHistory(new List<MetricsInfo>());
            e.AddCooldowns(new List<Cooldown>());

            return e;
        }
    }
}
