namespace Assets.Core
{
    static partial class Economy
    {
        public static TeamResource GetProductCompanyResourceChange(GameEntity company, GameContext gameContext)
        {
            long money = GetProfit(gameContext, company);

            int performance = Teams.GetPerformance(gameContext, company);

            return new TeamResource(
                Normalize(GetPP(company), performance),
                Normalize(GetMP(company), performance),
                Normalize(GetSP(company), performance),
                Normalize(GetIdeas(company, gameContext), performance),
                money
                );
        }


        static int Normalize(int value, int performance)
        {
            int period = Constants.PERIOD;

            return value * period * performance / 100;
        }


        static int GetPP(GameEntity productCompany)
        {
            var programmers = Teams.GetProgrammers(productCompany) * Constants.DEVELOPMENT_PRODUCTION_PROGRAMMER;
            var universals = Teams.GetUniversals(productCompany) * Constants.DEVELOPMENT_PRODUCTION_UNIVERSALS;

            return programmers + universals;
        }

        static int GetSP(GameEntity productCompany)
        {
            var marketers = Teams.GetMarketers(productCompany) * Constants.DEVELOPMENT_PRODUCTION_MARKETER;
            var universals = Teams.GetUniversals(productCompany) * Constants.DEVELOPMENT_PRODUCTION_UNIVERSALS;

            return marketers + universals;
        }

        static int GetMP(GameEntity productCompany)
        {
            var managers = Teams.GetManagers(productCompany) * Constants.DEVELOPMENT_PRODUCTION_MANAGER;
            var universals = Teams.GetUniversals(productCompany) * Constants.DEVELOPMENT_PRODUCTION_UNIVERSALS;

            return managers + universals;
        }

        public static int GetIdeas(GameEntity productCompany, GameContext gameContext)
        {
            var innovation = Products.GetInnovationChance(productCompany, gameContext);

            return innovation + 100;
        }
    }
}
