namespace Assets.Core
{
    public static partial class Teams
    {
        // ------------------- Management cost
        
        public static Bonus<float> GetManagerPointChange(GameEntity company, GameContext gameContext)
        {
            var teams = company.team.Teams;
        
            var bonus = new Bonus<float>("Point gain");

            bool teamsOnly = teams.Count > 3;

            foreach (var team in teams)
            {
                var b = GetTeamManagementBonus(team, company, gameContext);

                if (teamsOnly)
                {
                    bonus.Append(team.Name, b.Sum());
                }
                else
                {
                    bonus.Append(b);
                }
            }

            return bonus;
        }

        static float GetTeamManagementGain(TeamInfo team, GameEntity company, GameContext gameContext)
        {
            var role = GetMainManagerRole(team);
            var manager = GetWorkerByRole(role, team, gameContext);
            var coreTeamBonus = 1; // team.isCoreTeam ? 3 : 1;

            var rating = GetEffectiveManagerRating(gameContext, company, manager);
        
            return coreTeamBonus * rating / 10f;
        }

        public static Bonus<float> GetTeamManagementBonus(TeamInfo team, GameEntity company, GameContext gameContext)
        {
            var bonus = new Bonus<float>("points");
        
            var role = GetMainManagerRole(team);

            var flatness  = Companies.GetPolicyValue(company, CorporatePolicy.DoOrDelegate);
            var processes = Companies.GetPolicyValue(company, CorporatePolicy.PeopleOrProcesses);

            var minMultiplier = 0.1f;
            var maxMultiplier = 1f;

            var minDiscount = 0.5f;
            var maxDiscount = 2f;

            var multiplier = 1.5f; // minMultiplier + (maxMultiplier - minMultiplier) * flatness / 10;
            var discount = 1f; // minDiscount + (maxDiscount - minDiscount) * processes / 10;

            var center = 5;

            // vertical structure
            if (flatness < center)
                multiplier = 2;

            // horizontal
            if (flatness > center)
                multiplier = 1f;

            if (processes < center)
                discount = 2f;

            if (processes > center)
                discount = 0.5f;

            var gain = GetTeamManagementGain(team, company, gameContext) * multiplier;
            var maintenance = GetManagementCostOfTeam(team) * discount;
        
            bonus.AppendAndHideIfZero(Humans.GetFormattedRole(role), gain);
            bonus.Append("Maintenance", -maintenance);

            return bonus;
        }

        public static int GetManagementCostOfTeam(TeamInfo team)
        {
            switch (team.Rank)
            {
                case TeamRank.Solo: return team.isCoreTeam ? 4 : C.MANAGEMENT_COST_SOLO;
                case TeamRank.SmallTeam: return C.MANAGEMENT_COST_SMALL_TEAM;
                case TeamRank.BigTeam: return C.MANAGEMENT_COST_BIG_TEAM;
                case TeamRank.Department: return C.MANAGEMENT_COST_DEPARTMENT;

                default: return 0;
            }
        }
    }
}
