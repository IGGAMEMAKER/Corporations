namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        public static long GetDesireToSellGroupByInvestorType(GameEntity group, InvestorType investorType, int shareholderId, GameContext gameContext)
        {
            return group.isPublicCompany ? 1 : 0;
        }
    }
}
