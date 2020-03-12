namespace Assets.Core
{
    public static partial class Products
    {
        public static Bonus<long> GetInnovationChanceBonus(GameEntity product, GameContext gameContext)
        {
            var managingCompany = Companies.GetManagingCompanyOf(product, gameContext);

            bool isPrimaryMarket = Companies.IsInSphereOfInterest(managingCompany, product.product.Niche);

            // market state
            var niche = Markets.GetNiche(gameContext, product);
            var phase = Markets.GetMarketState(niche);
            var marketStage = Markets.GetMarketStageInnovationModifier(niche);

            var marketSpeedPenalty = GetNicheSpeedInnovationPenalty(niche);

            // culture bonuses
            var culture = Companies.GetActualCorporateCulture(product, gameContext);
            var responsibility  = culture[CorporatePolicy.LeaderOrTeam];
            var mindset         = culture[CorporatePolicy.InnovationOrStability];
            var createOrBuy     = culture[CorporatePolicy.BuyOrCreate];
            var focusing        = culture[CorporatePolicy.FocusingOrSpread];

            var maxCorpLevel = Balance.CORPORATE_CULTURE_LEVEL_MAX;

            // managers
            var productManager = Teams.GetWorkerByRole(product, WorkerRole.ProductManager, gameContext);
            var productManagerRating = productManager == null ? 0 : Humans.GetRating(productManager, WorkerRole.ProductManager);
            var productManagerBonus = Teams.GetWorkerEffeciency(productManager, product) * productManagerRating * 20 / 100 / 100;

            var CEOBonus = GetLeaderInnovationBonus(product) * (maxCorpLevel - responsibility) / maxCorpLevel;

            return new Bonus<long>("Innovation chance")
                // market
                .Append("Market stage " + Markets.GetMarketStateDescription(phase), marketStage)
                .Append("Market change speed", marketSpeedPenalty)

                // corp culture
                .Append("Corporate Culture Mindset", maxCorpLevel - mindset)
                .Append("Corporate Culture Acquisitions", createOrBuy)

                .Append("CEO bonus", CEOBonus)
                .AppendAndHideIfZero("Product manager", productManagerBonus)

                // focusing / sphere of interest
                //.AppendAndHideIfZero("Primary Market", isPrimaryMarket ? GetFocusingBonus(managingCompany) : 0)
                ;
        }

        public static int GetFocusingBonus(GameEntity product)
        {
            var focusing = Companies.GetPolicyValue(product, CorporatePolicy.FocusingOrSpread);

            return 5 * (Balance.CORPORATE_CULTURE_LEVEL_MAX - focusing);
        }

        public static int GetNicheSpeedInnovationPenalty(GameEntity niche)
        {
            var speed = niche.nicheBaseProfile.Profile.NicheSpeed;
            switch (speed)
            {
                case NicheSpeed.ThreeYears:
                    return -20;
                case NicheSpeed.Year:
                    return -12;
                case NicheSpeed.HalfYear:
                    return -5;
                case NicheSpeed.Quarter:
                default:
                    return 0;
            }
        }

        public static int GetInnovationChance(GameEntity product, GameContext gameContext)
        {
            var chance = GetInnovationChanceBonus(product, gameContext);

            return (int)chance.Sum();
        }

        public static int GetLeaderInnovationBonus(GameEntity product)
        {
            //var CEOId = 
            int companyId = product.company.Id;
            int CEOId = Companies.GetCEOId(product);

            //var accumulated = GetAccumulatedExpertise(company);

            return (int)(15 * Companies.GetHashedRandom2(companyId, CEOId));
            //return 35 + (int)(30 * GetHashedRandom2(companyId, CEOId) + accumulated);
        }
    }
}
