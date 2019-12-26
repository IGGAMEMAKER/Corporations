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
            var mindset         = culture[CorporatePolicy.WorkerMindset];
            var createOrBuy     = culture[CorporatePolicy.BuyOrCreate];
            var focusing        = culture[CorporatePolicy.Focusing];

            // penalties
            var tooManyPrimaryMarketsPenalty = Companies.GetPrimaryMarketsInnovationPenalty(managingCompany, gameContext);

            return new Bonus<long>("Innovation chance")
                // market
                .Append("Market stage " + Markets.GetMarketStateDescription(phase), marketStage)
                .Append("Market change speed", marketSpeedPenalty)

                // corp culture
                .Append("CEO bonus", GetLeaderInnovationBonus(product) * (10 - responsibility) / 10)
                .Append("Corporate Culture Mindset", 5 - mindset)
                .Append("Corporate Culture Acquisitions", createOrBuy * 2)

                // focusing / sphere of interest
                .AppendAndHideIfZero("Primary Market", isPrimaryMarket ? GetFocusingBonus(managingCompany) : 0)
                .AppendAndHideIfZero("Is part of holding", GetHoldingBonus(product, managingCompany))
                .AppendAndHideIfZero("Too many primary markets", -tooManyPrimaryMarketsPenalty)
                ;
        }

        public static int GetHoldingBonus(GameEntity product, GameEntity holding)
        {
            return 0;
        }

        public static int GetFocusingBonus(GameEntity product)
        {
            var focusing = Companies.GetPolicyValue(product, CorporatePolicy.Focusing);

            return 5 * (5 - focusing);
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
