using Assets.Classes;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static int PromoteProductCompanyToGroup(GameContext context, int companyId)
        {
            var c = GetCompanyById(context, companyId);

            Dictionary<int, int> founders = c.shareholders.Shareholders;

            int companyGroupId = GenerateCompanyGroup(context, c.company.Name + " Group", founders).company.Id;
            AttachToGroup(context, companyGroupId, companyId);

            return companyGroupId;
        }

        public static void RedistributeMoneyBetweenCompaniesIfPossible(GameContext context, int giverId, int acceptorId, int amount)
        {
            var giver = GetCompanyById(context, giverId);
            var acceptor = GetCompanyById(context, acceptorId);

            var transfer = new TeamResource(amount);
            var receiving = new TeamResource(-amount);

            if (!TeamResource.IsEnoughResources(transfer, giver.companyResource.Resources))
                return;

            var newGiverResources = TeamResource.Difference(giver.companyResource.Resources, transfer);
            var newAcceptorResources = TeamResource.Difference(acceptor.companyResource.Resources, receiving);

            giver.ReplaceCompanyResource(newGiverResources);
            acceptor.ReplaceCompanyResource(newAcceptorResources);
        }

        public static void AttachToGroup(GameContext context, int parent, int subsidiary)
        {
            // TODO only possible if independent!

            var p = GetCompanyById(context, parent);

            if (!IsCompanyGroupLike(p))
                return;

            var s = GetCompanyById(context, subsidiary);

            Debug.Log("Attach " + s.company.Name + " to " + p.company.Name);

            Dictionary<int, int> shareholders = new Dictionary<int, int>
            {
                { p.shareholder.Id, 100 }
            };

            Dictionary<int, InvestorGoal> goals = new Dictionary<int, InvestorGoal>
            {
                { p.shareholder.Id, InvestorGoal.GrowCompanyCost }
            };

            if (s.hasShareholders)
                s.ReplaceShareholders(shareholders, s.shareholders.Goals);
            else
                s.AddShareholders(shareholders, goals);

            p.isIndependentCompany = false;
        }

        public static GameEntity TurnToHolding(GameContext context, int companyId)
        {
            var c = GetCompanyById(context, companyId);

            c.ReplaceCompany(c.company.Id, c.company.Name, CompanyType.Holding);

            return c;
        }
    }
}
