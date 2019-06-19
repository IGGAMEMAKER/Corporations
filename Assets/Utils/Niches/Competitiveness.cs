namespace Assets.Utils
{
    public static partial class NicheUtils
    {
        internal static BonusContainer GetProductCompetitivenessBonus(GameEntity company, GameContext gameContext)
        {
            int techLeadershipBonus = company.isTechnologyLeader ? 15 : 0;

            return new BonusContainer("Product Competitiveness")
                .RenderTitle()
                .Append("Just some value", 5)
                .AppendAndHideIfZero("Is Setting Trends", techLeadershipBonus);
        }

        internal static long GetProductCompetitiveness(GameEntity company, GameContext gameContext)
        {
            return GetProductCompetitivenessBonus(company, gameContext).Sum();
        }
    }
}
