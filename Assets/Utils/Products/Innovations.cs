namespace Assets.Utils
{
    public static partial class ProductUtils
    {
        public static BonusContainer GetInnovationChanceBonus(GameEntity company, GameContext gameContext)
        {
            var culture = company.corporateCulture.Culture;
            var sphereOfInterestBonus = 0;
            if (!company.isIndependentCompany)
            {
                var parent = CompanyUtils.GetParentCompany(gameContext, company);


                if (parent != null)
                {
                    culture = parent.corporateCulture.Culture;
                    if (CompanyUtils.IsInSphereOfInterest(parent, company.product.Niche))
                        sphereOfInterestBonus = 5;
                }
            }

            var niche = NicheUtils.GetNiche(gameContext, company.product.Niche);
            var phase = NicheUtils.GetMarketState(niche);
            var marketStage = CompanyUtils.GetMarketStageInnovationModifier(niche);


            // culture bonuses
            var responsibility = culture[CorporatePolicy.Responsibility];
            var mindset = culture[CorporatePolicy.WorkerMindset];
            var createOrBuy = culture[CorporatePolicy.CreateOrBuy];

            // culture bonuses

            return new BonusContainer("Innovation chance")
                .Append("Base", 5)
                .Append("Market stage " + CompanyUtils.GetMarketStateDescription(phase), marketStage)
                
                .Append("CEO bonus", GetLeaderInnovationBonus(company) * (5 + (5 - responsibility)) / 10)
                .Append("Corporate Culture Mindset", 10 - mindset * 2)
                .Append("Corporate Culture Acquisitions", createOrBuy * 2)
                
                .AppendAndHideIfZero("Is independent company", company.isIndependentCompany ? 5 : 0)
                .AppendAndHideIfZero("Parent company focuses on this company market", sphereOfInterestBonus);
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
            int CEOId = CompanyUtils.GetCEOId(company);

            //var accumulated = GetAccumulatedExpertise(company);

            return (int)(15 * CompanyUtils.GetHashedRandom2(companyId, CEOId));
            //return 35 + (int)(30 * GetHashedRandom2(companyId, CEOId) + accumulated);
        }
    }
}
