using Assets.Classes;
using System;

namespace Assets.Utils
{
    static partial class EconomyUtils
    {
        // TODO move this to better place!
        public static int GetPeriodDuration()
        {
            return 30;
        }

        public static TeamResource GetProductCompanyResourceChange(GameEntity company, GameContext gameContext)
        {
            var period = GetPeriodDuration();

            long money = GetProfit(company, gameContext) * period / 30;

            int performance = TeamUtils.GetPerformance(gameContext, company);

            return new TeamResource(
                Normalize(GetPP(company), performance),
                Normalize(GetMP(company), performance),
                Normalize(GetSP(company), performance),
                Normalize(GetIdeas(company), performance),
                money
                );
        }


        static int Normalize (int value, int performance)
        {
            int period = GetPeriodDuration();

            return value * period * performance / 100;
        }

        static long Normalize (long value, int performance)
        {
            int period = GetPeriodDuration();

            return value * period * performance / 100;
        }


        static int GetPP(GameEntity productCompany)
        {
            var programmers = TeamUtils.GetProgrammers(productCompany) * Constants.DEVELOPMENT_PRODUCTION_PROGRAMMER;
            var universals = TeamUtils.GetUniversals(productCompany) * Constants.DEVELOPMENT_PRODUCTION_UNIVERSALS;

            return programmers + universals;
        }

        static int GetSP(GameEntity productCompany)
        {
            var marketers = TeamUtils.GetMarketers(productCompany) * Constants.DEVELOPMENT_PRODUCTION_MARKETER;
            var universals = TeamUtils.GetUniversals(productCompany) * Constants.DEVELOPMENT_PRODUCTION_UNIVERSALS;

            return marketers + universals;
        }

        static int GetMP(GameEntity productCompany)
        {
            var managers = TeamUtils.GetManagers(productCompany) * Constants.DEVELOPMENT_PRODUCTION_MANAGER;
            var universals = TeamUtils.GetUniversals(productCompany) * Constants.DEVELOPMENT_PRODUCTION_UNIVERSALS;

            return managers + universals;
        }

        static int GetIdeas(GameEntity productCompany)
        {
            var focusModifier = Constants.DEVELOPMENT_FOCUS_IDEAS;

            var expertiseModifier = Companies.GetCompanyExpertise(productCompany);

            return Constants.DEVELOPMENT_PRODUCTION_IDEAS * (expertiseModifier + focusModifier) / 100;
        }
    }
}
