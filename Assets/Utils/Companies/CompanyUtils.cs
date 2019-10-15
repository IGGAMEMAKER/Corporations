using Entitas;
using System;
using System.Linq;

namespace Assets.Utils
{
    public static partial class CompanyUtils
    {
        // Read
        public static GameEntity GetCompanyById(GameContext context, int companyId)
        {
            return Array.Find(context.GetEntities(GameMatcher.Company), c => c.company.Id == companyId);
        }

        public static GameEntity GetCompanyByName(GameContext context, string name)
        {
            return Array.Find(context.GetEntities(GameMatcher.Company), c => c.company.Name.Equals(name));
        }

        public static GameEntity GetInvestorById(GameContext context, int investorId)
        {
            return InvestmentUtils.GetInvestorById(context, investorId);
        }


        public static bool IsManagingCompany(GameEntity e)
        {
            var t = e.company.CompanyType;

            return t == CompanyType.Corporation || t == CompanyType.Group || t == CompanyType.Holding;
        }

        public static bool IsCompanyGroupLike(GameContext context, int companyId)
        {
            var c = GetCompanyById(context, companyId);

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
            return IsProductCompany(GetCompanyById(context, companyId));
        }



        // TODO move to separate file
        public static bool IsExploredCompany(GameContext gameContext, int companyId)
        {
            var company = GetCompanyById(gameContext, companyId);

            return IsExploredCompany(gameContext, company);
        }
        public static bool IsExploredCompany(GameContext gameContext, GameEntity company)
        {
            return company.hasResearch || IsCompanyRelatedToPlayer(gameContext, company);
        }


        // Update
        public static void Rename(GameContext context, int companyId, string name)
        {
            var c = GetCompanyById(context, companyId);

            c.ReplaceCompany(c.company.Id, name, c.company.CompanyType);
        }
    }
}
