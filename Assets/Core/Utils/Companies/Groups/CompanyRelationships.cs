using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    partial class Companies
    {
        public static GameEntity GetManagingCompanyOf(GameEntity company, GameContext gameContext)
        {
            if (company.isIndependentCompany)
                return company;

            var parent = GetParentCompany(gameContext, company);

            return parent ?? company;
        }

        public static GameEntity GetPayer(GameEntity company, GameContext gameContext)
        {
            if (company.isFlagship)
            {
                return GetPlayerCompany(gameContext);
            }

            return company;
        }
    }
}
