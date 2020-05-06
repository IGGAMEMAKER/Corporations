namespace Assets.Core
{
    public static partial class Products
    {
        public static int GetInnovationChance(GameEntity product, GameContext gameContext)
        {
            var chance = GetInnovationChanceBonus(product, gameContext);

            return (int)chance.Sum();
        }

        public static Bonus<long> GetInnovationChanceBonus(GameEntity product, GameContext gameContext)
        {
            // market state
            var niche = Markets.Get(gameContext, product);
            var phase = Markets.GetMarketState(niche);
            var marketStage = Markets.GetMarketStageInnovationModifier(niche);

            var marketSpeedPenalty = GetNicheSpeedInnovationPenalty(niche);

            // culture bonuses
            var culture = Companies.GetActualCorporateCulture(product, gameContext);
            var createOrBuy     = culture[CorporatePolicy.Make];

            // managers
            var productManagerBonus = Teams.GetProductManagerBonus(product, gameContext);

            var CEOBonus = Teams.GetCEOInnovationBonus(product, gameContext);

            return new Bonus<long>("Innovation chance")
                // market
                .Append("Market stage " + Markets.GetMarketStateDescription(phase), marketStage)
                .Append("Market change speed", marketSpeedPenalty)

                // corp culture
                //.Append("Corporate Culture Mindset", maxCorpLevel - mindset)
                .Append("Corporate Culture Expansion Policy", createOrBuy)

                // managers
                .Append("CEO bonus", CEOBonus)
                .AppendAndHideIfZero("Product manager", productManagerBonus)
                ;
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
    }
}
