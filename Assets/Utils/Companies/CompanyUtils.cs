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
            return InvestmentUtils.GetInvestorById(context, investorId);
        }

        public static int GetCompanyIdByInvestorId(GameContext context, int investorId)
        {
            return InvestmentUtils.GetCompanyIdByInvestorId(context, investorId);
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

        public static int GetRandomInvestmentFund(GameContext context)
        {
            var funds = GetFinancialCompanies(context);

            var index = UnityEngine.Random.Range(0, funds.Length);

            return funds[index].shareholder.Id;
        }

        // Update
        public static void Rename(GameContext context, int companyId, string name)
        {
            var c = GetCompanyById(context, companyId);

            c.ReplaceCompany(c.company.Id, name, c.company.CompanyType);
        }
    }
}
