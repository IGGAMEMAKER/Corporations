using System.Linq;

namespace Assets.Core
{
    partial class Economy
    {
        // resulting costs
        public static Bonus<long> GetProductCompanyMaintenance(GameEntity e, bool isBonus)
        {
            var bonus = new Bonus<long>("Maintenance")
                .Append("Team", GetManagersCost(e))
                ;

            // team tasks
            foreach (var t in e.team.Teams[0].Tasks)
            {
                if (!t.IsPending)
                    bonus.AppendAndHideIfZero(t.GetPrettyName(), GetTeamTaskCost(e, t));
            }

            return bonus;
        }

        public static long GetTeamTaskCost(GameEntity product, TeamTask teamTask)
        {
            return 0;
            if (teamTask.IsFeatureUpgrade)
                return 0;

            if (teamTask.IsMarketingTask)
                return (teamTask as TeamTaskChannelActivity).ChannelCost;
                //return Marketing.GetMarketingActivityCost(product, gameContext, (teamTask as TeamTaskChannelActivity).ChannelId);

            return 0;
        }

        public static long GetProductMaintenance(GameEntity e, GameContext gameContext)
        {
            var bonus = GetProductCompanyMaintenance(e, true);

            return bonus.Sum();
        }



        // team cost

        public static Bonus<long> GetSalaries(GameEntity e, GameContext gameContext)
        {
            Bonus<long> salaries = new Bonus<long>("Manager salaries");

            foreach (var t in e.team.Teams)
            {
                foreach (var humanId in t.Managers)
                {
                    var human = Humans.Get(gameContext, humanId);
                    var salary = Humans.GetSalary(human);

                    salaries.Append(Humans.GetFullName(human), salary);
                }
            }

            return salaries;
        }

        public static void UpdateSalaries(GameEntity company, GameContext gameContext)
        {
            company.team.Salaries = GetSalaries(company, gameContext).Sum();
        }

        public static long GetManagersCost(GameEntity e)
        {
            return e.team.Salaries;
        }
    }
}
