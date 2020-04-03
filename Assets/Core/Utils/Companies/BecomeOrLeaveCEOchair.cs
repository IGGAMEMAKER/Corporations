namespace Assets.Core
{
    public static partial class Companies
    {
        public static void RemovePlayerControlledCompany(GameContext context, int id)
        {
            Get(context, id).isControlledByPlayer = false;
        }

        internal static void BecomeCEO(GameContext gameContext, int companyID)
        {
            SetPlayerControlledCompany(gameContext, companyID);
        }

        public static void PlayAs(GameEntity company, GameContext GameContext)
        {
            var human = Humans.GetHuman(GameContext, company.cEO.HumanId);

            SetPlayerControlledCompany(GameContext, company.company.Id);

            human.isPlayer = true;
        }

        public static void SetPlayerControlledCompany(GameContext context, int id)
        {
            var c = Get(context, id);

            c.isControlledByPlayer = true;
        }

        // it is done for player only!
        internal static void LeaveCEOChair(GameContext gameContext, int companyId)
        {
            RemovePlayerControlledCompany(gameContext, companyId);

            // update company ceo component
        }
    }
}
