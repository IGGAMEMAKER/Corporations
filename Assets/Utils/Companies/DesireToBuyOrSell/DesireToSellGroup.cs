namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        public static long GetDesireToSellGroup(GameEntity group, GameContext gameContext)
        {
            var shareholders = group.shareholders.Shareholders;

            long blocks = 0;
            long desireToSell = 0;

            foreach (var s in shareholders)
            {
                var invId = s.Key;
                var block = s.Value;

                desireToSell += GetDesireToSellGroupByInvestorType(group, block.InvestorType, invId, gameContext) * block.amount;
                blocks += block.amount;
            }

            if (blocks == 0)
                return 0;

            return desireToSell * 100 / blocks;
        }

        public static long GetDesireToSellGroupByInvestorType(GameEntity group, InvestorType investorType, int shareholderId, GameContext gameContext)
        {
            return group.isPublicCompany ? 1 : 0;
        }
    }
}
