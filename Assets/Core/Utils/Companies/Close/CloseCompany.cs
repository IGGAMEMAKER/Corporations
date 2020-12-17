namespace Assets.Core
{
    partial class Companies
    {
        public static GameEntity CloseCompany(GameContext context, GameEntity company, bool bankrupt = false)
        {
            var balance = Economy.BalanceOf(company);

            company.companyResourceHistory.Actions.Add(new ResourceTransaction { Tag = "Close company", TeamResource = -balance });
            
            // pay to everyone
            PayDividends(context, company, balance);

            // fire everyone

            NotifyAboutProductSupportEnd(company, context);

            if (company.hasProduct)
            {
                Teams.DismissTeam(company, context);
                Markets.ReturnUsersWhenCompanyIsClosed(company, context);
            }

            RemoveAllPartnerships(company, context);

            foreach (var holding in GetDaughters(company, context))
            {
                RemoveShareholder(holding, context, company);
            }

            RemoveAllShareholders(context, company);
            //e.ReplaceShareholders(new Dictionary<int, BlockOfShares>());



            company.isAlive = false;

            if (bankrupt)
                company.isBankrupt = true;

            return company;
        }



        public static void NotifyAboutProductSupportEnd(GameEntity company, GameContext gameContext)
        {
            if (Companies.IsInPlayerSphereOfInterest(company, gameContext))
                NotificationUtils.AddNotification(gameContext, new NotificationMessageBankruptcy(company.company.Id));

            NotificationUtils.SendBankruptcyPopup(gameContext, company);
        }
    }
}
