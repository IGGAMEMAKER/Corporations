using System;

namespace Assets.Utils
{
    static partial class CompanyEconomyUtils
    {
        private static long GetProductCompanyIncome(GameEntity e, GameContext context)
        {
            float income = 0;

            foreach (var pair in e.marketing.Segments)
                income += GetIncomeBySegment(context, e.company.Id, pair.Key);

            return Convert.ToInt64(income);
        }

        internal static float GetIncomeBySegment(GameContext gameContext, int companyId, UserType userType)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            long clients = c.marketing.Segments[userType];

            float price = GetSegmentPrice(gameContext, companyId, userType);

            return clients * price;
        }

        internal static float GetSegmentPrice(GameContext gameContext, int companyId, UserType userType)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            return GetUserTypePriceModifier(userType) * GetProductPrice(c, gameContext);
        }

        public static float GetProductPrice(GameEntity e, GameContext context)
        {
            if (e.finance.price == Pricing.Free) return 0;

            float price = (float)e.finance.price;

            return GetBaseProductPrice(e, context) * price / 100;
        }

        public static float GetBaseProductPrice(GameEntity e, GameContext context)
        {
            return e.finance.basePrice;
        }

        private static float GetUserTypePriceModifier(UserType userType)
        {
            switch (userType)
            {
                case UserType.Regular: return 0.6f;
                case UserType.Core: return 2f;

                default: return 0.1f;
            }
        }
    }
}
