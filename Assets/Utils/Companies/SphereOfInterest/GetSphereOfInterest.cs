using Entitas;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        internal static bool IsInSphereOfInterest(GameEntity company, GameContext gameContext)
        {
            var player = GetPlayerCompany(gameContext);
            if (player == null)
                return false;

            return IsInSphereOfInterest(player, company);
        }

        internal static bool IsInSphereOfInterest(GameEntity company, NicheType niche)
        {
            if (!company.hasCompanyFocus)
                return false;

            return company.companyFocus.Niches.Contains(niche);
        }

        internal static bool IsInSphereOfInterest(GameEntity company, GameEntity interestingCompany)
        {
            if (!interestingCompany.hasProduct)
                return false;

            return IsInSphereOfInterest(company, interestingCompany.product.Niche);
        }




        public static void RemoveFromSphereOfInfluence(NicheType nicheType, GameEntity company, GameContext gameContext)
        {
            var focus = company.companyFocus;
            var niches = focus.Niches.Remove(nicheType);

            NotifyAboutCompanyFocusChange(gameContext, company.company.Id, false, nicheType);

            company.ReplaceCompanyFocus(focus.Niches, focus.Industries);
        }

        public static void AddFocusNiche(NicheType nicheType, GameEntity company, GameContext gameContext)
        {
            var niches = company.companyFocus.Niches;

            if (IsInSphereOfInterest(company, nicheType))
                return;

            niches.Add(nicheType);
            NotifyAboutCompanyFocusChange(gameContext, company.company.Id, true, nicheType);

            company.ReplaceCompanyFocus(niches, company.companyFocus.Industries);
        }

        public static void NotifyAboutCompanyFocusChange(GameContext gameContext, int companyId, bool addedOrRemoved, NicheType nicheType)
        {
            //NotificationUtils.AddNotification(gameContext, new NotificationMessageCompanyFocusChange(companyId, addedOrRemoved, nicheType));
        }

        public static void AddFocusIndustry(IndustryType industryType, GameEntity company)
        {
            var industries = company.companyFocus.Industries;

            if (industries.Contains(industryType))
                return;

            industries.Add(industryType);

            company.ReplaceCompanyFocus(company.companyFocus.Niches, industries);
        }

        internal static bool HasCompanyOnMarket(GameEntity group, NicheType nicheType, GameContext gameContext)
        {
            return GetDaughterCompanies(gameContext, group.company.Id).Count(c => c.hasProduct && c.product.Niche == nicheType) > 0;
        }



        internal static long GetMarketShareOfCompanyMultipliedByHundred(GameEntity product, GameContext gameContext)
        {
            long clients = 0;

            foreach (var p in NicheUtils.GetProductsOnMarket(gameContext, product))
                clients += MarketingUtils.GetClients(p);

            if (clients == 0)
                return 0;

            var share = 100 * MarketingUtils.GetClients(product) / clients;

            //Debug.Log("GetMarketShareOf " + product.company.Name + " : " + share);

            return share;
        }


        
        internal static long GetMarketImportanceForCompany(GameContext gameContext, GameEntity company, NicheType n)
        {
            return NicheUtils.GetMarketSize(gameContext, n) * GetControlInMarket(company, n, gameContext) / 100;
        }



        internal static long GetControlInMarket(GameEntity group, NicheType nicheType, GameContext gameContext)
        {
            var products = NicheUtils.GetProductsOnMarket(gameContext, nicheType);

            long share = 0;

            long clients = 0;

            foreach (var p in products)
            {
                var cli = MarketingUtils.GetClients(p);
                share += GetControlInCompany(group, p, gameContext) * cli;

                clients += cli;
            }

            if (clients == 0)
                return 0;

            return share / clients;

            //return HasCompanyOnMarket(myCompany, nicheType, gameContext) ? 100 / players : 0;
        }

        public static int GetControlInCompany(GameEntity controlling, GameEntity holding, GameContext gameContext)
        {
            int shares = 0;

            foreach (var daughter in GetDaughterCompanies(gameContext, controlling.company.Id))
            {
                if (daughter.company.Id == holding.company.Id)
                    shares += GetShareSize(gameContext, holding.company.Id, controlling.shareholder.Id);

                if (!daughter.hasProduct)
                    shares += GetControlInCompany(daughter, holding, gameContext);
            }

            return shares;
        }
    }
}
