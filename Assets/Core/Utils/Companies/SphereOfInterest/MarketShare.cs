namespace Assets.Core
{
    public static partial class Companies
    {
        public static long GetMarketShareOfCompanyMultipliedByHundred(GameEntity product, GameContext gameContext)
        {
            long clients = 0;

            foreach (var p in Markets.GetProductsOnMarket(gameContext, product))
                clients += Marketing.GetClients(p);

            if (clients == 0)
                return 0;

            var share = 100 * Marketing.GetClients(product) / clients;

            return share;
        }

        public static long GetControlInMarket(GameEntity group, NicheType nicheType, GameContext gameContext)
        {
            long share = 0;
            long clients = 0;

            foreach (var p in Markets.GetProductsOnMarket(gameContext, nicheType))
            {
                var cli = Marketing.GetClients(p);
                share += GetControlInCompany(group, p, gameContext) * cli;

                clients += cli;
            }

            if (clients == 0)
                return 0;

            return share / clients;
        }

        public static int GetControlInCompany(GameEntity group, GameEntity target, GameContext gameContext)
        {
            int shares = 0;

            foreach (var daughter in GetDaughterCompanies(gameContext, group))
            {
                if (daughter.company.Id == target.company.Id)
                    shares += GetShareSize(gameContext, target.company.Id, group.shareholder.Id);

                if (!daughter.hasProduct)
                    shares += GetControlInCompany(daughter, target, gameContext);
            }

            return shares;
        }
    }
}
