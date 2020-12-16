namespace Assets.Core
{
    public static partial class Companies
    {
        public static void RemovePlayerControlledCompany(GameContext context, int id)
        {
            var company = Get(context, id);
            company.isControlledByPlayer = false;
            company.isRelatedToPlayer = false;
        }

        public static void BecomeCEO(GameContext gameContext, int companyID)
        {
            SetPlayerControlledCompany(gameContext, companyID);
        }

        public static void PlayAs(GameEntity company, GameContext GameContext)
        {
            var human = Humans.Get(GameContext, company.cEO.HumanId);

            SetPlayerControlledCompany(GameContext, company.company.Id);

            human.isPlayer = true;
        }

        public static void SetPlayerControlledCompany(GameContext context, int id)
        {
            var c = Get(context, id);

            c.isControlledByPlayer = true;
            AttachToPlayer(c);
        }

        // it is done for player only!
        public static void LeaveCEOChair(GameContext gameContext, int companyId)
        {
            RemovePlayerControlledCompany(gameContext, companyId);

            // update company ceo component
        }
    }
}
