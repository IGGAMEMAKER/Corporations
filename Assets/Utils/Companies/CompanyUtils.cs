using Entitas;
using System;
using System.Collections.Generic;

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

        public static int GetCompanyIdByInvestorId(GameContext context, int shareholderId)
        {
            return Array.Find(
                context.GetEntities(GameMatcher.Shareholder),
                e => e.shareholder.Id == shareholderId
                ).company.Id;
        }

        public static GameEntity GetInvestorById(GameContext context, int investorId)
        {
            var investorGroup = context.GetEntities(GameMatcher.Shareholder);

            return Array.Find(investorGroup, s => s.shareholder.Id == investorId);
        }

        public static int GetTotalShares(Dictionary<int, int> shareholders)
        {
            int totalShares = 0;
            foreach (var e in shareholders)
                totalShares += e.Value;

            return totalShares;
        }

        public static bool IsCompanyGroupLike(GameContext context, int companyId)
        {
            var c = GetCompanyById(context, companyId);

            return IsCompanyGroupLike(c);
        }

        public static bool IsCompanyGroupLike(GameEntity gameEntity)
        {
            if (gameEntity.company.CompanyType == CompanyType.ProductCompany)
                return false;

            return true;
        }

        // Update
        public static void Rename(GameContext context, int companyId, string name)
        {
            var c = GetCompanyById(context, companyId);

            c.ReplaceCompany(c.company.Id, name, c.company.CompanyType);
        }

        public static int BecomeInvestor(GameContext context, GameEntity e, long money)
        {
            int investorId = GenerateInvestorId(context); // GenerateInvestorId();

            string name = "Investor?";

            // company
            if (e.hasCompany)
                name = e.company.Name;

            // or human
            // TODO turn human to investor

            e.AddShareholder(investorId, name, money);

            return investorId;
        }

        public static void AddShareholder(GameContext context, int companyId, int investorId, int shares)
        {
            var c = GetCompanyById(context, companyId);

            Dictionary<int, int> shareholders;

            if (!c.hasShareholders)
            {
                shareholders = new Dictionary<int, int>();
                shareholders[investorId] = shares;

                c.AddShareholders(shareholders);
            }
            else
            {
                shareholders = c.shareholders.Shareholders;
                shareholders[investorId] = shares;

                c.ReplaceShareholders(shareholders);
            }
        }


        public static void RemovePlayerControlledCompany(GameContext context, int id)
        {
            GetCompanyById(context, id).isControlledByPlayer = false;
        }

        internal static void BecomeCEO(GameContext gameContext, int companyID)
        {
            SetPlayerControlledCompany(gameContext, companyID);
        }

        public static void SetPlayerControlledCompany(GameContext context, int id)
        {
            var c = GetCompanyById(context, id);

            c.isControlledByPlayer = true;
        }

        // it is done for player only!
        internal static void LeaveCEOChair(GameContext gameContext, int companyId)
        {
            RemovePlayerControlledCompany(gameContext, companyId);

            // update company ceo component
        }
    }
}
