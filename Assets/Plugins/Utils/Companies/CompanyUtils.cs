using Entitas;
using System;
using System.Linq;

namespace Assets.Utils
{
    public static partial class Companies
    {
        // Read
        public static GameEntity GetCompany(GameContext context, int companyId)
        {
            return Array.Find(context.GetEntities(GameMatcher.Company), c => c.company.Id == companyId);
        }
        public static string GetCompanyName(GameContext context, int companyId)
        {
            return GetCompany(context, companyId).company.Name;
        }

        public static GameEntity GetCompanyByName(GameContext context, string name)
        {
            return Array.Find(context.GetEntities(GameMatcher.Company), c => c.company.Name.Equals(name));
        }

        public static GameEntity GetInvestorById(GameContext context, int investorId)
        {
            return Investments.GetInvestorById(context, investorId);
        }


        public static bool IsManagingCompany(GameEntity e)
        {
            var t = e.company.CompanyType;

            return t == CompanyType.Corporation || t == CompanyType.Group || t == CompanyType.Holding;
        }

        public static bool IsCompanyGroupLike(GameContext context, int companyId)
        {
            var c = GetCompany(context, companyId);

            return IsCompanyGroupLike(c);
        }

        public static bool IsCompanyGroupLike(GameEntity gameEntity)
        {
            return !IsProductCompany(gameEntity);
        }

        public static bool IsProductCompany(GameEntity gameEntity)
        {
            return gameEntity.company.CompanyType == CompanyType.ProductCompany;
        }

        public static bool IsProductCompany(GameContext context, int companyId)
        {
            return IsProductCompany(GetCompany(context, companyId));
        }



        // TODO move to separate file
        public static bool IsExploredCompany(GameContext gameContext, int companyId)
        {
            var company = GetCompany(gameContext, companyId);

            return IsExploredCompany(gameContext, company);
        }
        public static bool IsExploredCompany(GameContext gameContext, GameEntity company)
        {
            return company.hasResearch || IsRelatedToPlayer(gameContext, company);
        }


        // Update
        public static void Rename(GameContext context, int companyId, string name)
        {
            var c = GetCompany(context, companyId);

            c.ReplaceCompany(c.company.Id, name, c.company.CompanyType);
        }
    }
}
