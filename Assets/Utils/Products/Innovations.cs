namespace Assets.Utils
{
    public static partial class ProductUtils
    {
        public static Bonus<long> GetInnovationChanceBonus(GameEntity product, GameContext gameContext)
        {
            var sphereOfInterestBonus = 0;

            var managingCompany = product.isIndependentCompany ? product : Companies.GetParentCompany(gameContext, product);
            var culture = managingCompany.corporateCulture.Culture;

            bool isPrimaryMarket = Companies.IsInSphereOfInterest(managingCompany, product.product.Niche);



            var niche = Markets.GetNiche(gameContext, product);
            var phase = Markets.GetMarketState(niche);
            var marketStage = Markets.GetMarketStageInnovationModifier(niche);

            var marketSpeedPenalty = GetNicheSpeedInnovationPenalty(niche);

            // culture bonuses
            var responsibility  = culture[CorporatePolicy.LeaderOrTeam];
            var mindset         = culture[CorporatePolicy.WorkerMindset];
            var createOrBuy     = culture[CorporatePolicy.CreateOrBuy];
            var focusing        = culture[CorporatePolicy.Focusing];

            // culture bonuses

            return new Bonus<long>("Innovation chance")
                .Append("Base", 5)
                .Append("Market stage " + Markets.GetMarketStateDescription(phase), marketStage)
                .Append("Market change speed", marketSpeedPenalty)
                
                .Append("CEO bonus", GetLeaderInnovationBonus(product) * (5 + (5 - responsibility)) / 10)
                .Append("Corporate Culture Mindset", 10 - mindset * 2)
                .Append("Corporate Culture Acquisitions", createOrBuy * 2)
                .AppendAndHideIfZero("Too many primary markets", Companies.GetPrimaryMarketsInnovationPenalty(managingCompany, gameContext))
                
                .AppendAndHideIfZero("Parent company focuses on this company market", sphereOfInterestBonus);
        }

        public static int GetFocusingBonus(GameEntity product)
        {

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
