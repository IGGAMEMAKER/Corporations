namespace Assets.Core
{
    public static partial class Markets
    {
        public static MarketState GetMarketState(GameEntity niche)
        {
            return niche.nicheState.Phase;
        }

        public static bool IsExploredMarket(GameContext gameContext, NicheType nicheType)
        {
            var niche = GetNiche(gameContext, nicheType);

            return niche.hasResearch;
        }

        public static MarketState GetMarketState(GameContext gameContext, NicheType nicheType)
        {
            var niche = GetNiche(gameContext, nicheType);

            return GetMarketState(niche);
        }

        public static int GetMarketRating(GameContext gameContext, NicheType niche)
        {
            return GetMarketRating(GetNiche(gameContext, niche));
        }

        public static int GetMarketPotentialRating(GameEntity niche)
        {
            var rating = 1;


            var profile = niche.nicheBaseProfile.Profile;

            var audience = profile.AudienceSize;
            var income = profile.Margin;

            if (audience == AudienceSize.Global || audience == AudienceSize.BigEnterprise) rating += 2;
            if (audience == AudienceSize.Million100 || audience == AudienceSize.SmallEnterprise) rating += 1;


            if (income == Margin.High) rating += 2;
            if (income == Margin.Mid) rating += 1;

            return rating;
        }

    }
}
