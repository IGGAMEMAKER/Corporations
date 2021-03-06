﻿namespace Assets.Core
{
    public static partial class Companies
    {
        public static bool IsCanBePartnersTheoretically(GameEntity requester, GameEntity acceptor)
        {
            // self partnering :)
            if (requester.company.Id == acceptor.company.Id)
                return false;

            // parents w childs
            // || IsDaughterOfCompany(acceptor, requester)
            if (IsDaughterOf(requester, acceptor))
                return false;

            if (!requester.isIndependentCompany || !acceptor.isIndependentCompany)
                return false;

            return true;
        }

        public static bool IsHasTooManyPartnerships(GameEntity company)
        {
            var maxPartnerships = 3;

            return company.partnerships.companies.Count >= maxPartnerships;
        }
    }
}
