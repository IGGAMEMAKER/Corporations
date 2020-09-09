using Entitas;
using System;

namespace Assets.Core
{
    public static partial class Companies
    {
        // Read
        public static GameEntity[] Get(GameContext context)
        {
            return context.GetEntities(GameMatcher.Company);
        }

        public static GameEntity Get(GameContext context, int companyId)
        {
            return Array.Find(Get(context), c => c.company.Id == companyId);
        }

        public static string GetCompanyName(GameContext context, int companyId) => GetCompanyName(Get(context, companyId));
        public static string GetCompanyName(GameEntity company) => company.company.Name;

        public static GameEntity GetCompanyByName(GameContext context, string name)
        {
            return Array.Find(context.GetEntities(GameMatcher.Company), c => c.company.Name.Equals(name));
        }

        public static GameEntity GetInvestorById(GameContext context, int investorId)
        {
            return Investments.GetInvestor(context, investorId);
        }


        public static bool IsManagingCompany(GameEntity e)
        {
            var t = e.company.CompanyType;

            return t == CompanyType.Corporation || t == CompanyType.Group || t == CompanyType.Holding;
        }

        public static bool IsCompanyGroupLike(GameContext context, int companyId) => IsCompanyGroupLike(Get(context, companyId));
        public static bool IsCompanyGroupLike(GameEntity gameEntity)
        {
            return !IsProductCompany(gameEntity);
        }

        public static bool IsProductCompany(GameContext context, int companyId) => IsProductCompany(Get(context, companyId));
        public static bool IsProductCompany(GameEntity gameEntity)
        {
            return gameEntity.company.CompanyType == CompanyType.ProductCompany;
        }


        public static void SupportCompany(GameEntity main, GameEntity daughter, long money)
        {
            SpendResources(main, money);
            daughter.companyResource.Resources.AddMoney(money);
        }


        // TODO move to separate file
        public static bool IsExploredCompany(GameContext gameContext, int companyId) => IsExploredCompany(gameContext, Get(gameContext, companyId));
        public static bool IsExploredCompany(GameContext gameContext, GameEntity company)
        {
            return company.hasResearch || IsRelatedToPlayer(gameContext, company);
        }


        // Update
        public static void Rename(GameContext context, int companyId, string name)
        {
            var c = Get(context, companyId);

            c.ReplaceCompany(c.company.Id, name, c.company.CompanyType);
        }
    }
}
