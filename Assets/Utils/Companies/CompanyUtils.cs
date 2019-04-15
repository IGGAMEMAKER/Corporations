using Entitas;
using System;

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
            return Array.Find(context.GetEntities(GameMatcher.Shareholder), s => s.shareholder.Id == investorId);
        }

        public static int GetCompanyIdByInvestorId(GameContext context, int investorId)
        {
            return GetInvestorById(context, investorId).company.Id;
        }

        public static bool IsCompanyGroupLike(GameContext context, int companyId)
        {
            var c = GetCompanyById(context, companyId);

            return IsCompanyGroupLike(c);
        }

        public static bool IsProductCompany(GameEntity gameEntity)
        {
            return gameEntity.company.CompanyType == CompanyType.ProductCompany;
        }

        public static bool IsProductCompany(GameContext context, int companyId)
        {
            return IsProductCompany(GetCompanyById(context, companyId));
        }

        public static bool IsCompanyGroupLike(GameEntity gameEntity)
        {
            return !IsProductCompany(gameEntity);
        }

        public static GameEntity[] GetDaughterCompaniesRecursively(GameContext context, int companyId)
        {
            return GetDaughterCompanies(context, companyId);
        }

        public static GameEntity[] GetInvestableCompanies(GameContext context)
        {
            return context.GetEntities(GameMatcher.AllOf(GameMatcher.Company, GameMatcher.Shareholders));
        }

        public static GameEntity[] GetInvestments(GameContext context, int investorId)
        {
            return Array.FindAll(
                GetInvestableCompanies(context),
                c => c.shareholders.Shareholders.ContainsKey(investorId)
                );
        }

        public static GameEntity[] GetDaughterCompanies(GameContext context, int companyId)
        {
            var c = GetCompanyById(context, companyId);
            int investorId = c.shareholder.Id;

            if (!c.hasShareholder)
                return new GameEntity[0];

            return GetInvestments(context, investorId);
        }

        // Update
        public static void Rename(GameContext context, int companyId, string name)
        {
            var c = GetCompanyById(context, companyId);

            c.ReplaceCompany(c.company.Id, name, c.company.CompanyType);
        }
    }
}
