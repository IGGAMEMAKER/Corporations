using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        public static GameEntity[] GetPossibleInvestors(GameContext gameContext, int companyId)
        {
            var totalShareholders = gameContext.GetEntities(GameMatcher.Shareholder);

            var c = GetCompanyById(gameContext, companyId).company;

            return Array.FindAll(totalShareholders, s => IsInvestorSuitable(s.shareholder, c));
        }

        public static bool IsInvestorSuitable(ShareholderComponent shareholder, CompanyComponent company)
        {


            return shareholder.InvestorType == InvestorType.Angel;
        }
    }
}
