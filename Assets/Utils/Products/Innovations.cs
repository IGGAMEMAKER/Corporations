namespace Assets.Utils
{
    public static partial class ProductUtils
    {
        public static Bonus<long> GetInnovationChanceBonus(GameEntity company, GameContext gameContext)
        {
            var culture = company.corporateCulture.Culture;
            var sphereOfInterestBonus = 0;
            if (!company.isIndependentCompany)
            {
                var parent = Companies.GetParentCompany(gameContext, company);


                if (parent != null)
                {
                    culture = parent.corporateCulture.Culture;
                    if (Companies.IsInSphereOfInterest(parent, company.product.Niche))
                        sphereOfInterestBonus = 5;
                }
            }

            var niche = Markets.GetNiche(gameContext, company.product.Niche);
            var phase = Markets.GetMarketState(niche);
            var marketStage = Markets.GetMarketStageInnovationModifier(niche);

            var marketSpeedPenalty = GetNicheSpeedInnovationPenalty(niche);

            // culture bonuses
            var responsibility = culture[CorporatePolicy.Responsibility];
            var mindset = culture[CorporatePolicy.WorkerMindset];
            var createOrBuy = culture[CorporatePolicy.CreateOrBuy];

            // culture bonuses

            return new Bonus<long>("Innovation chance")
                .Append("Base", 5)
                .Append("Market stage " + Markets.GetMarketStateDescription(phase), marketStage)
                .Append("Market change speed", marketSpeedPenalty)
                
                .Append("CEO bonus", GetLeaderInnovationBonus(company) * (5 + (5 - responsibility)) / 10)
                .Append("Corporate Culture Mindset", 10 - mindset * 2)
                .Append("Corporate Culture Acquisitions", createOrBuy * 2)
                
                .AppendAndHideIfZero("Is independent company", company.isIndependentCompany ? 5 : 0)
                .AppendAndHideIfZero("Parent company focuses on this company market", sphereOfInterestBonus);
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

        public static int GetInnovationChance(GameEntity company, GameContext gameContext)
        {
            var chance = GetInnovationChanceBonus(company, gameContext);

            return (int)chance.Sum();
        }

        public static int GetLeaderInnovationBonus(GameEntity company)
        {
            //var CEOId = 
            int companyId = company.company.Id;
            int CEOId = Companies.GetCEOId(company);

            //var accumulated = GetAccumulatedExpertise(company);

            return (int)(15 * Companies.GetHashedRandom2(companyId, CEOId));
            //return 35 + (int)(30 * GetHashedRandom2(companyId, CEOId) + accumulated);
        }
    }
}
