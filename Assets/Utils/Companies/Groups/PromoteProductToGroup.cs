using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static int PromoteProductCompanyToGroup(GameContext context, int companyId)
        {
            var c = GetCompanyById(context, companyId);

            if (!c.isIndependentCompany)
                return -1;

            var niche = c.product.Niche;

            var industry = NicheUtils.GetIndustry(niche, context);

            var name = c.company.Name;

            int companyGroupId = GenerateCompanyGroup(context, name + " Group", companyId).company.Id;

            AttachToGroup(context, companyGroupId, companyId);
            c.isIndependentCompany = false;

            var groupCo = GetCompanyById(context, companyGroupId);
            AddFocusIndustry(industry, groupCo);
            AddFocusNiche(niche, groupCo, context);
            groupCo.isManagingCompany = true;

            NotifyAboutCompanyPromotion(context, companyGroupId, name);

            return companyGroupId;
        }

        public static void NotifyAboutCompanyPromotion(GameContext gameContext, int companyId, string previousName)
        {
            NotificationUtils.AddNotification(gameContext, new NotificationMessageCompanyTypeChange(companyId, previousName));
        }

        public static void AttachToGroup(GameContext context, int parent, int subsidiary)
        {
            // TODO only possible if independent!

            var p = GetCompanyById(context, parent);

            if (!IsCompanyGroupLike(p))
                return;

            var s = GetCompanyById(context, subsidiary);

            Debug.Log("Attach " + s.company.Name + " to " + p.company.Name);

            var shareholders = new Dictionary<int, BlockOfShares>
            {
                {
                    p.shareholder.Id,
                    new BlockOfShares
                    {
                        amount = 100,
                        InvestorType = InvestorType.Strategic,
                        shareholderLoyalty = 100
                    }
                }
            };

            if (s.hasShareholders)
                s.ReplaceShareholders(shareholders);
            else
                s.AddShareholders(shareholders);

            p.isIndependentCompany = false;
        }

        public static GameEntity TurnToHolding(GameContext context, int companyId)
        {
            var c = GetCompanyById(context, companyId);

            c.ReplaceCompany(c.company.Id, c.company.Name, CompanyType.Holding);
            c.isManagingCompany = true;

            return c;
        }
    }
}
