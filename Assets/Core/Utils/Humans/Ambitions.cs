using System.Collections.Generic;

namespace Assets.Core
{
    public enum Ambition
    {
        No,
        GrowSkills,
        GrowRole,
        EarnMoney,
        RuleProductCompany,

        RuleCorporation,

    }
    public static partial class Humans
    {
        // ambitions
        public static Ambition GetAmbition(GameContext gameContext, int humanId)
        {
            var human = Get(gameContext, humanId);
            var rating = GetRating(human);

            return GetFounderAmbition(human.humanSkills.Traits, rating);
        }

        public static Ambition GetFounderAmbition(List<Trait> traits, int rating)
        {
            // 5 traits
            // * 1 on hiring
            // * 1 after test period (opens)
            // * 1 after year of work (opens)
            // * 1 after 3 years (?adds / opens)
            // * 1 after decade (adds)

            // if you do it: because you don't want to have a boss; because you don't want to be dependent from other's opinion; because you want to be independent
            // you end up with the complete opposite stuff :)
            // you are dependent from Clients, Investors, Government, Competition

            // if you want to change the world, make smth good, you don't necessarily need to create ur own company
            // you can make an impact in other companies too!

            // ambitious + wants smth new = create company
            // 
            if (traits.Contains(Trait.Ambitious))
            {
                // overly ambitious dude :)

                if (rating < 60)
                    return Ambition.GrowRole;

                // make 
                if (rating < 75)
                    return Ambition.RuleProductCompany;

                return Ambition.RuleCorporation;
            }

            if (traits.Contains(Trait.Careerist))
            {
                if (rating < 60)
                    return Ambition.GrowSkills;

                if (rating < 75)
                    return Ambition.GrowRole;

                if (rating < 85)
                    return Ambition.RuleProductCompany;

                return Ambition.RuleCorporation;
            }

            if (traits.Contains(Trait.Executor))
            {
                if (rating < 75)
                    return Ambition.GrowSkills;

                return Ambition.GrowRole;
            }



            if (rating < 75)
            {
                return Ambition.EarnMoney;
            }
            else if (traits.Contains(Trait.Ambitious))
            {
                return Ambition.RuleCorporation;
            }
            else
            {
                return Ambition.RuleProductCompany;
            }
        }
    }
}
