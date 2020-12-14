using Entitas;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Companies
    {
        // Read
        public static GameEntity[] GetAll(GameContext context)
        {
            return context.GetEntities(GameMatcher.Company);
        }
        public static GameEntity[] Get(GameContext context)
        {
            return GetAll(context);
            return context.GetEntities(GameMatcher.AllOf(GameMatcher.Company, GameMatcher.Alive));
        }

        public static GameEntity Get(GameContext context, int companyId)
        {
            return Array.Find(Get(context), c => c.company.Id == companyId);
        }

        public static string GetName(GameContext context, int companyId) => GetName(Get(context, companyId));
        public static string GetName(GameEntity company) => company.company.Name;
        public static string GetShortName(GameEntity company) => company.company.Name.Substring(0, 1) + company.company.Name.FirstOrDefault(char.IsDigit);


        public static GameEntity GetCompanyByName(GameContext context, string name)
        {
            return Array.Find(context.GetEntities(GameMatcher.Company), c => c.company.Name.Equals(name));
        }

        public static GameEntity GetInvestorById(GameContext context, int investorId)
        {
            return Investments.GetInvestor(context, investorId);
        }


        public static bool IsGroup(GameEntity e)
        {
            var t = e.company.CompanyType;

            return t == CompanyType.Corporation || t == CompanyType.Group || t == CompanyType.Holding;
        }


        public static bool IsProduct(GameEntity e)
        {
            return e.hasProduct;
        }

        // Update
        public static void Rename(GameContext context, int companyId, string name)
        {
            var c = Get(context, companyId);

            c.ReplaceCompany(c.company.Id, name, c.company.CompanyType);
        }

        public static string GetCompanyNames(IEnumerable<GameEntity> companies, string separator = ", ") => string.Join(separator, companies.Select(c => c.company.Name));

        // Logging

        public static void LogError(GameEntity entity, string text) => Debug.LogError($"error in entity #{entity.creationIndex}" + text);
        public static void LogSuccess(GameEntity entity, string text) => Log(entity, Visuals.Positive(text));
        public static void LogFail(GameEntity entity, string text) => Log(entity, Visuals.Negative(text));
        public static void Log(GameEntity entity, string text)
        {
            if (!entity.hasLogging)
                entity.AddLogging(new List<string>());

            if (IsObservableCompany(entity))
            {
                entity.logging.Logs.Add(text);
                //Debug.Log(entity.company.Name + ": " + text);
            }

            //if (IsPlayerCompany(entity))
            //{
            //    Debug.Log(entity.company.Name + ": " + text);
            //}
        }

        public static void LogFinancialStatus(GameEntity company, GameContext gameContext)
        {
            var balance = Economy.BalanceOf(company);
            var profit = Economy.GetProfit(gameContext, company);

            Log(company, $"Financial report: balance={Format.Money(balance)}, profit={Format.Money(profit)}");
        }
        public static void LogFinancialTransactions(GameEntity company)
        {
            Log(company, string.Join("\n", company.companyResourceHistory.Actions.Select(r => r.Print())));
        }

        public static bool IsObservableCompany(GameEntity company)
        {
            // in player sphere of interest
            // or special company

            var names = new List<string>() { "Google" };

            return company.hasCompany && company.hasCompanyFocus
                && (company.companyFocus.Niches.Contains(NicheType.ECom_MoneyExchange)); // || names.Contains(company.company.Name)
        }
    }
}
