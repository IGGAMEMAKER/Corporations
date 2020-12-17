namespace Assets.Core
{
    partial class Companies
    {
        // TODO move to separate file
        public static bool IsDirectlyRelatedToPlayer(GameContext gameContext, GameEntity company)
        {
            return company.isRelatedToPlayer;

            // var playerCompany = GetPlayerCompany(gameContext);
            //
            // if (playerCompany == null)
            //     return false;

            // return company.isControlledByPlayer || IsDaughterOf(playerCompany, company);
        }

        // TODO it is possible to buy a company if you have some amount of shares in it already!
        public static bool IsTheoreticallyPossibleToBuy(GameContext gameContext, GameEntity buyer, GameEntity target)
        {
            return !HasControlInCompany(buyer, target, gameContext);

            //foreach (var owning in Investments.GetOwnings(gameContext, buyer))
            //{

            //}
        }
    }
}
