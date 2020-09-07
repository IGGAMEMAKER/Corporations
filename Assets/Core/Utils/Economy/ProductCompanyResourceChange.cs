namespace Assets.Core
{
    static partial class Economy
    {
        public static TeamResource GetProductCompanyResourceChange(GameEntity company, GameContext gameContext)
        {
            long money = GetProfit(gameContext, company);

            return new TeamResource(
                0,
                0,
                0,
                0,
                money
                );
        }
    }
}
