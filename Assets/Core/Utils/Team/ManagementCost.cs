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

        public static Bonus<float> GetTeamManagementBonus(TeamInfo team, GameEntity company, GameContext gameContext, bool shortDescription = false)
        {
            var bonus = new Bonus<float>("points");
        
            var role = GetMainManagerRole(team);

            var flatness  = Companies.GetPolicyValue(company, CorporatePolicy.DoOrDelegate);
            var processes = Companies.GetPolicyValue(company, CorporatePolicy.PeopleOrProcesses);


            var center = 5;

            // ---- structure
            var multiplier = 1.5f;
            if (flatness < center)
                multiplier = 2;

            if (flatness > center)
                multiplier = 1f;

            // ---- processes
            var discount = 1f;
            if (processes < center)
                discount = 2f;

            if (processes > center)
                discount = 0.5f;


            
            var gain = GetTeamManagementGain(team, company, gameContext) * multiplier;
            var directMaintenance = GetManagementCostOfTeam(team);
            var indirectMaintenance = GetIndirectManagementCostOfTeam(team);

            var gainNameFormatted = shortDescription
                ? Humans.GetFormattedRole(role)
                : $"{Humans.GetFormattedRole(role)} in {team.Name}";

            var maintenanceFormatted = shortDescription ? "Direct management" : team.Name;
            
            bool noManager = !HasMainManagerInTeam(team);
            bool isDirectManagement = noManager || team.isCoreTeam;
            
            
            // if not managed properly
            // spend points from CEO

            var notEnoughPoints = directMaintenance > gain;

            bool applyDirectManagementCost = isDirectManagement || notEnoughPoints;
            bool applyIndirectManagementCost = !applyDirectManagementCost;

            if (applyDirectManagementCost)
            {
                bonus.AppendAndHideIfZero(maintenanceFormatted, -directMaintenance);
                bonus.AppendAndHideIfZero(gainNameFormatted, gain);
            }
            // bonus.Append($"Maintenance of {team.Rank}#{team.ID}", -maintenance);


            if (applyIndirectManagementCost)
            {
                bonus.Append("Indirect management", -indirectMaintenance);
            }
            
            return bonus;
        }

        static float GetTeamManagementGain(TeamInfo team, GameEntity company, GameContext gameContext)
        {
            var role = GetMainManagerRole(team);
            var manager = GetWorkerByRole(role, team, gameContext);

            var rating = GetEffectiveManagerRating(company, manager);
        
            return rating / 10f;
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

        public static int GetIndirectManagementCostOfTeam(TeamInfo team)
        {
            switch (team.Rank)
            {
                case TeamRank.Solo: return 1;
                case TeamRank.SmallTeam: return 2;
                case TeamRank.BigTeam: return 3;
                case TeamRank.Department: return 4;

                default: return 0;
            }
        }
    }
}
