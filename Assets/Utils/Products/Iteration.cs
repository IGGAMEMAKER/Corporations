namespace Assets.Utils
{
    public static partial class ProductUtils
    {
        public static int GetBaseIterationTime(GameEntity niche) => GetBaseIterationTime(niche.nicheLifecycle.NicheChangeSpeed);
        public static int GetBaseIterationTime(NichePeriod nicheChangeSpeed)
        {
            var modifier = 4;
            switch (nicheChangeSpeed)
            {
                case NichePeriod.Quarter: return 90 / modifier;

                case NichePeriod.HalfYear: return 180 / modifier;
                case NichePeriod.Year: return 360 / modifier;

                case NichePeriod.ThreeYears: return 360 * 3 / modifier;

                default: return 0;
            }
        }

        public static int GetProductUpgradeIterationTime(GameContext gameContext, GameEntity company)
        {
            var niche = NicheUtils.GetNiche(gameContext, company.product.Niche);
            var baseConceptTime = GetBaseIterationTime(niche);

            var innovationTime = IsWillInnovate(company, gameContext) ? 50 : 0;
            var financing = company.financing.Financing;

            var culture = company.corporateCulture.Culture;
            var mindsetModifier = culture[CorporatePolicy.WorkerMindset];

            var modifiers = 100 + innovationTime
                - financing[Financing.Team] * Constants.FINANCING_ITERATION_SPEED_PER_LEVEL
                - mindsetModifier * Constants.CULTURE_ITERATION_SPEED_PER_LEVEL;

            var time = (int) (baseConceptTime * modifiers / 100f);
            //Debug.Log($"GetProductUpgradeIterationTime: company={company.company.Name} dev={devModifier} culture={culture} ** result={time}");

            return time;
        }
    }
}
