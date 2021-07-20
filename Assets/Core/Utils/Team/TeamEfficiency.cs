using System.Linq;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static void UpdateTeamEfficiency(GameEntity product, GameContext gameContext)
        {
            // competition
            var competitors = Companies.GetDirectCompetitors(product, gameContext, true);

            bool isUniqueCompany = competitors.Count() == 1;
            // -------------------------------------------------

            product.ReplaceTeamEfficiency(new TeamEfficiency
            {
                DevelopmentEfficiency = GetDevelopmentTeamEfficiency(gameContext, product),
                MarketingEfficiency = GetMarketingTeamEfficiency(gameContext, product, isUniqueCompany),

                FeatureCap = GetMaxFeatureRatingCap(product, gameContext).Sum(), // Products.GetFeatureRatingCap(company, gameContext),
                FeatureGain = GetFeatureRatingGain(),

                isUniqueCompany = isUniqueCompany,
                Competitiveness = 0
            });
        }

        public static int GetDevelopmentEfficiency(GameEntity company)
        {
            return company.teamEfficiency.Efficiency.DevelopmentEfficiency;
        }

        public static int GetMarketingEfficiency(GameEntity company)
        {
            return company.teamEfficiency.Efficiency.MarketingEfficiency;
        }

        // -------------- DEV -----------------------
        public static int GetDevelopmentTeamEfficiency(GameContext gameContext, GameEntity product, TeamInfo teamInfo)
        {
            var efficiency = GetEffectiveManagerRating(gameContext, product, WorkerRole.TeamLead, teamInfo);

            efficiency += teamInfo.TeamType == TeamType.DevelopmentTeam ? 30 : 0;

            //efficiency += Teams.GetManagerFocusBonus(gameContext, product, teamInfo, ManagerTask.ViralSpread) * 10;

            return (10 + efficiency) * GetTeamEfficiency(product, teamInfo);
        }

        private static int GetDevelopmentTeamEfficiency(GameContext gameContext, GameEntity product)
        {
            var viableTeams = product.team.Teams

            // dev teams only
            .Where(t => IsUniversalTeam(t.TeamType) || t.TeamType == TeamType.DevelopmentTeam)

            .Select(t => GetDevelopmentTeamEfficiency(gameContext, product, t) / 100)
            ;

            if (!viableTeams.Any())
                return 50;

            return (int)viableTeams.Average();
        }

        // -------------- MARKETING --------------------------------
        public static int GetMarketingTeamEfficiency(GameContext gameContext, GameEntity company, bool isUnique)
        {
            var viableTeams = company.team.Teams
                
                // marketing teams only
                .Where(t => IsTaskSuitsTeam(t.TeamType, GetMarketingTaskMockup())) //  IsUniversalTeam(t.TeamType) || t.TeamType == TeamType.MarketingTeam

                .Select(t => GetMarketingTeamEfficiency(gameContext, company, t) / 100)
                ;

            if (!viableTeams.Any())
                return 30;

            return (int)viableTeams.Average() * (isUnique ? 2 : 1);
        }


        public static int GetMarketingTeamEfficiency(GameContext gameContext, GameEntity product, TeamInfo teamInfo)
        {
            var efficiency = GetEffectiveManagerRating(gameContext, product, WorkerRole.MarketingLead, teamInfo);

            efficiency *= teamInfo.TeamType == TeamType.MarketingTeam ? 2 : 1;

            //marketingEffeciency += Teams.GetManagerFocusBonus(gameContext, company, teamInfo, ManagerTask.ViralSpread) * 10;

            return (50 + efficiency) * GetTeamEfficiency(product, teamInfo);
        }


        // --------------------------------

        public static int GetManagerFocusBonus(GameContext gameContext, GameEntity company, TeamInfo teamInfo, ManagerTask managerTask)
        {
            bool hasMainManager = Teams.HasMainManagerInTeam(teamInfo);

            if (hasMainManager)
            {
                return teamInfo.ManagerTasks.Count(t => t == managerTask);
            }

            return 0;
        }


        // --------------------- FEATURES --------------------------
        public static float GetFeatureRatingGain() => 1f;

        public static Bonus<float> GetMaxFeatureRatingCap(GameEntity product, GameContext gameContext)
        {
            return product.team.Teams
                .Select(t => GetFeatureRatingCap(product, t, gameContext))
                .OrderByDescending(b => b.Sum())
                .First();
        }

        public static Bonus<float> GetFeatureRatingCap(GameEntity product, TeamInfo team, GameContext gameContext)
        {
            var productManager = Teams.GetWorkerInRole(team, WorkerRole.ProductManager, gameContext);

            var bonus = new Bonus<float>("Max feature lvl");

            bonus.Append("Base", 3f);
            bonus.AppendAndHideIfZero("Development Team", team.TeamType == TeamType.DevelopmentTeam ? 1f : 0);

            if (productManager != null)
            {
                // ... 5f
                var addedCap = 6 * Humans.GetRating(productManager) / 100f;

                bonus.AppendAndHideIfZero("Product Manager", addedCap);
            }

            // var culture = Companies.GetOwnCulture(Companies.GetManagingCompanyOf(product, gameContext));
            //
            // var cultureBonus = (10 - culture[CorporatePolicy.DoOrDelegate]) * 0.2f;
            // var cultureBonus2 = (culture[CorporatePolicy.DecisionsManagerOrTeam]) * 0.4f;
            //
            // bonus.Append("Corporate culture Do or Delegate", cultureBonus);
            // bonus.Append("Corporate culture Structure", cultureBonus2);


            //bool hasMainManager = Teams.HasMainManagerInTeam(team, gameContext, product);
            //if (hasMainManager)
            //{
            //    var focus = team.ManagerTasks.Count(t => t == ManagerTask.Polishing);
            //    cap += focus * 0.4f;
            //}

            bonus.Cap(0, 10);

            return bonus;

            //return Mathf.Clamp(cap, 0, 10);
        }

        // ------------------------------------------------------------------

        public static int GetTeamEfficiency(GameEntity company, TeamInfo teamInfo)
        {
            var maxWorkers = Teams.GetMaxTeamSize(teamInfo); // 8;

            var organisationBonus = (int)teamInfo.Organisation;

            // 0... 130
            return teamInfo.Workers * (100 + 30 * organisationBonus / 100) / maxWorkers + 1;
        }

        public static int GetWorkerEfficiency(GameEntity worker, GameEntity company)
        {
            if (worker == null)
                return 0;

            var expertise = 0;

            if (company.hasProduct && worker.humanSkills.Expertise.ContainsKey(company.product.Niche))
                expertise = worker.humanSkills.Expertise[company.product.Niche];

            var adaptability = worker.humanCompanyRelationship.Adapted == 100 ? 100 : 30;

            return (70 * adaptability + 30 * expertise) / 100;
        }
    }
}
