using Assets.Classes;
using System;

namespace Assets.Utils
{
    static partial class CompanyEconomyUtils
    {
        // TODO move this to better place!
        public static int GetPeriodDuration()
        {
            return 30;
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
            //var productManager = TeamUtils.GetManagers
            //var universals = TeamUtils.GetUniversals(productCompany) * Constants.DEVELOPMENT_PRODUCTION_UNIVERSALS;

            var focusModifier = productCompany.developmentFocus.Focus == DevelopmentFocus.Concept
                ? Constants.DEVELOPMENT_FOCUS_IDEAS : 0;

            return Constants.DEVELOPMENT_PRODUCTION_IDEAS * (100 + focusModifier) / 100;
        }

        public static TeamResource GetResourceChange(GameEntity productCompany, GameContext gameContext)
        {
            int performance = TeamUtils.GetPerformance(gameContext, productCompany);

            long money = GetBalanceChange(productCompany, gameContext) * GetPeriodDuration() / 30;

            return new TeamResource(
                Normalize(GetPP(productCompany), performance),
                Normalize(GetMP(productCompany), performance),
                Normalize(GetSP(productCompany), performance),
                Normalize(GetIdeas(productCompany), performance),
                money
                );
        }
    }
}
