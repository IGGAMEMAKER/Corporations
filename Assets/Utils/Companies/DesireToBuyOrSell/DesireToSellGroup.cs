namespace Assets.Core
{
    public static partial class Companies
    {
        public static long GetDesireToSellGroupByInvestorType(GameEntity group, InvestorType investorType, int shareholderId, GameContext gameContext)
        {
            return group.isPublicCompany ? 1 : 0;
        }
    }
}
