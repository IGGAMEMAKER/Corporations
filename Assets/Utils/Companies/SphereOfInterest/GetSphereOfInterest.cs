using Entitas;
using System;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        internal static bool IsInSphereOfInterest(GameEntity company, NicheType niche)
        {
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

            if (niches.Contains(nicheType))
                return;

            niches.Add(nicheType);
            NotifyAboutCompanyFocusChange(gameContext, company.company.Id, true, nicheType);

            company.ReplaceCompanyFocus(niches, company.companyFocus.Industries);
        }

        public static void NotifyAboutCompanyFocusChange(GameContext gameContext, int companyId, bool addedOrRemoved, NicheType nicheType)
        {
            NotificationUtils.AddNotification(gameContext, new NotificationMessageCompanyFocusChange(companyId, addedOrRemoved, nicheType));
        }

        public static void AddFocusIndustry(IndustryType industryType, GameEntity company)
        {
            var industries = company.companyFocus.Industries;

            if (industries.Contains(industryType))
                return;

            industries.Add(industryType);

            company.ReplaceCompanyFocus(company.companyFocus.Niches, industries);
        }
    }
}
