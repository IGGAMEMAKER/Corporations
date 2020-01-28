using UnityEngine;

namespace Assets.Core
{
    public static partial class Marketing
    {
        public static long GetClientFlow(GameContext gameContext, NicheType nicheType)
        {
            return Markets.GetClientFlow(gameContext, nicheType);
        }

        public static long GetBaseClientsForNewCompanies(GameContext gameContext, NicheType nicheType)
        {
            var niche = Markets.GetNiche(gameContext, nicheType);

            var monetisation = niche.nicheBaseProfile.Profile.MonetisationType;

            switch (monetisation)
            {
                case Monetisation.Adverts: return 5000;
                case Monetisation.Enterprise: return 3;
                case Monetisation.Paid: return 100;
                case Monetisation.Service: return 500;
                case Monetisation.IrregularPaid: return 350;

                default: return 0;
            }
        }
    }
}
