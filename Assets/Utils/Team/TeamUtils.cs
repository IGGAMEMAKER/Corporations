using Entitas;
using System;

namespace Assets.Utils
{
    public static partial class TeamUtils
    {
        private static void ReplaceTeam(GameEntity gameEntity, TeamComponent t)
        {
            gameEntity.ReplaceTeam(t.Programmers, t.Managers, t.Marketers, t.Morale);
        }

        public static int GetTeamMaxSize(GameEntity company)
        {
            return company.team.Managers * 7;
        }

        public static int GetTeamSize(GameEntity company) {
            return company.team.Managers + company.team.Marketers + company.team.Programmers;
        }

        public static bool IsWillNotOverextendTeam(GameEntity company)
        {
            return GetTeamSize(company) + 1 < GetTeamMaxSize(company);
        }

        public static void HireManager(GameEntity company)
        {
            var t = company.team;

            t.Managers++;

            ReplaceTeam(company, t);
        }

        public static void HireProgrammer(GameEntity company)
        {
            var t = company.team;
            t.Programmers++;

            ReplaceTeam(company, t);
        }

        public static void HireMarketer(GameEntity company)
        {
            var t = company.team;
            t.Marketers++;

            ReplaceTeam(company, t);
        }

        public static void FireManager(GameEntity company)
        {
            var t = company.team;

            if (t.Managers > 0)
                t.Managers--;

            ReplaceTeam(company, t);
        }

        public static void FireProgrammer(GameEntity company)
        {
            var t = company.team;

            if (t.Programmers > 0)
                t.Programmers--;

            ReplaceTeam(company, t);
        }

        public static void FireMarketer(GameEntity company)
        {
            var t = company.team;

            if (t.Marketers > 0)
                t.Marketers--;

            ReplaceTeam(company, t);
        }

    }
}
