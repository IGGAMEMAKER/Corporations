namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        internal static BonusContainer GetProductCompetitivenessBonus(GameEntity company, GameContext gameContext)
        {
            int marketLevel = GetMarketDemand(gameContext, company.product.Niche);

            int productLevel = company.product.ProductLevel;

            int techLeadershipBonus = company.isTechnologyLeader ? 15 : 0;

            return new BonusContainer("Product Competitiveness")
                .RenderTitle()
                .Append("Product Level", productLevel)
                .Append("Market Requirements", -marketLevel)
                .AppendAndHideIfZero("Is Setting Trends", techLeadershipBonus);
        }

        public static int GetMarketDemand(GameContext gameContext, NicheType nicheType)
        {
            var nicheState = GetNicheEntity(gameContext, nicheType).nicheState;

            return nicheState.Level;
        }

        internal static long GetProductCompetitiveness(GameEntity company, GameContext gameContext)
        {
            return GetProductCompetitivenessBonus(company, gameContext).Sum();
        }
    }
}
