namespace Assets.Utils
{
    public static class CompanyEconomyUtils
    {
        public static long GetIncome(GameEntity e, GameContext context)
        {
            if (e.company.CompanyType == CompanyType.ProductCompany)
                return ProductEconomicsUtils.GetIncome(e);

            return 1000000;
        }

        public static GameEntity[] GetDaughterCompanies(GameEntity e, GameContext context)
        {
            return new GameEntity[] { e };
        }
    }
}
