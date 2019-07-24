namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        static BonusContainer GetCompanyRiskBonus(GameContext gameContext, int companyId)
        {
            int marketDemand = GetMarketDemandRisk(gameContext, companyId);
            int monetisation = GetMonetisationRisk(gameContext, companyId);
            int competitors = GetCompetitionRisk(gameContext, companyId);

            return new BonusContainer("Total risk")
                .SetDimension("%")
                .Append("Niche demand risk", marketDemand)
                //.Append("Competition risk", competitors)
                .AppendAndHideIfZero("Is not profitable", monetisation);
        }

        internal static int GetCompetitionRisk (GameContext gameContext, int companyId)
        {
            var company = CompanyUtils.GetCompanyById(gameContext, companyId);

            return GetCompetitorsAmount(company, gameContext) * 5;
        }

        internal static long GetCompanyRisk(GameContext gameContext, int companyId)
        {
            return GetCompanyRiskBonus(gameContext, companyId).Sum();
        }

        public static string GetCompanyRiskDescription(GameContext gameContext, int companyId)
        {
            return GetCompanyRiskBonus(gameContext, companyId).ToString(true);
        }

        public static int GetMonetisationRisk(GameContext gameContext, int companyId)
        {
            int num = Constants.RISKS_MONETISATION_MAX;

            if (CompanyEconomyUtils.IsCompanyProfitable(gameContext, companyId))
                num -= Constants.RISKS_MONETISATION_IS_PROFITABLE;

            return num;
        }


        public static int GetMarketDemandRisk(GameContext gameContext, int companyId)
        {
            var c = CompanyUtils.GetCompanyById(gameContext, companyId);

            return GetMarketDemandRisk(gameContext, c.product.Niche);
        }

        public static int GetMarketDemandRisk(GameContext gameContext, NicheType nicheType)
        {
            var niche = GetNicheEntity(gameContext, nicheType);

            switch (niche.nicheState.Phase)
            {
                case NicheLifecyclePhase.Idle:
                    return Constants.RISKS_DEMAND_MAX;

                case NicheLifecyclePhase.Innovation:
                    return Constants.RISKS_DEMAND_MAX / 2;

                case NicheLifecyclePhase.Trending:
                    return Constants.RISKS_DEMAND_MAX / 5;

                case NicheLifecyclePhase.MassUse:
                    return Constants.RISKS_DEMAND_MAX / 10;

                case NicheLifecyclePhase.Decay:
                    return Constants.RISKS_DEMAND_MAX / 2;

                case NicheLifecyclePhase.Death:
                default:
                    return 100;
            }
        }
    }
}
