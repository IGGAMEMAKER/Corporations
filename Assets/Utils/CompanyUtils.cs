using Entitas;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    public static class CompanyUtils
    {
        // Create
        public static int GenerateCompanyId(GameContext context)
        {
            return context.GetEntities(GameMatcher.Company).Length;
        }

        public static int GenerateInvestorId(GameContext context)
        {
            return context.GetEntities(GameMatcher.Shareholder).Length;
        }

        public static void GenerateProduct(GameContext context, string name, NicheType niche, int id)
        {
            IndustryType industry = NicheUtils.GetIndustry(niche, context);

            var resources = new Classes.TeamResource(100, 100, 100, 100, 10000);

            uint clients = (uint)UnityEngine.Random.Range(0, 10000);
            int brandPower = UnityEngine.Random.Range(0, 15);

            int productLevel = 0;
            int explorationLevel = productLevel;

            var e = context.CreateEntity();
            e.AddCompany(id, name, CompanyType.ProductCompany);

            // product specific components
            e.AddProduct(id, name, niche, industry, productLevel, explorationLevel, resources);
            e.AddFinance(0, 0, 0, 5f);
            e.AddTeam(1, 0, 0, 100);
            e.AddMarketing(clients, brandPower, false);
        }

        public static int GenerateInvestmentFund(GameContext context, string name, long money)
        {
            var e = context.CreateEntity();

            int id = GenerateCompanyId(context); // GenerateId();
            int investorId = GenerateInvestorId(context);

            e.AddCompany(id, name, CompanyType.FinancialGroup);
            BecomeInvestor(context, e, money);

            return investorId;
        }

        public static int GenerateHoldingCompany(GameContext context, string name)
        {
            var e = context.CreateEntity();

            int id = GenerateCompanyId(context); // GenerateId();

            e.AddCompany(id, name, CompanyType.Holding);
            BecomeInvestor(context, e, 0);

            return id;
        }

        public static int GenerateCompanyGroup(GameContext context, string name, Dictionary<int, int> founders = null)
        {
            var e = CreateCompanyEntity(context, name, CompanyType.Group, founders);

            BecomeInvestor(context, e, 0);

            return e.company.Id;
        }

        public static int GenerateProductCompany(GameContext context, string name, NicheType niche)
        {
            int id = GenerateCompanyId(context); // GenerateId();

            GenerateProduct(context, name, niche, id);

            return id;
        }

        public static GameEntity CreateCompanyEntity(GameContext context, string name, CompanyType companyType, Dictionary<int, int> founders = null)
        {
            var e = context.CreateEntity();

            int id = GenerateCompanyId(context);

            e.AddCompany(id, name, companyType);

            if (founders == null)
                founders = new Dictionary<int, int>();

            e.AddShareholders(founders);

            return e;
        }

        // Read
        public static GameEntity GetCompanyById(GameContext context, int companyId)
        {
            return Array.Find(context.GetEntities(GameMatcher.Company), c => c.company.Id == companyId);
        }

        public static GameEntity GetAnyOfControlledCompanies(GameContext context)
        {
            return GetPlayerControlledProductCompany(context);
        }

        public static GameEntity GetCompanyByName(GameContext context, string name)
        {
            return Array.Find(context.GetEntities(GameMatcher.Company), c => c.company.Name.Equals(name));
        }

        public static GameEntity GetPlayerControlledProductCompany(GameContext context)
        {
            // TODO check all use cases! 
            // Most of them simply use MyControlledProductCompany, when we may need GetMyGroupCompany instead!

            var matcher = GameMatcher.AllOf(GameMatcher.Product, GameMatcher.ControlledByPlayer);

            var companies = context.GetEntities(matcher);

            if (companies.Length == 1) return companies[0];

            return null;
        }

        public static GameEntity GetPlayerControlledGroupCompany(GameContext context)
        {
            var matcher = GameMatcher.AllOf(GameMatcher.ControlledByPlayer).NoneOf(GameMatcher.Product);

            var companies = context.GetEntities(matcher);

            if (companies.Length == 1) return companies[0];

            return null;
        }

        public static GameEntity[] GetMyNeighbours(GameContext context)
        {
            GameEntity[] products = GetProductsNotControlledByPlayer(context);

            var myProduct = GetPlayerControlledProductCompany(context).product;

            return Array.FindAll(products, e => e.product.Niche != myProduct.Niche && e.product.Industry == myProduct.Industry);
        }

        public static GameEntity[] GetMyCompetitors(GameContext context)
        {
            GameEntity[] products = GetProductsNotControlledByPlayer(context);

            GameEntity myProductEntity = GetPlayerControlledProductCompany(context);

            return Array.FindAll(products, e => e.product.Niche == myProductEntity.product.Niche);
        }

        public static GameEntity[] GetProductsNotControlledByPlayer(GameContext context)
        {
            var matcher = GameMatcher
                        .AllOf(GameMatcher.Product)
                        .NoneOf(GameMatcher.ControlledByPlayer);

            GameEntity[] entities = context.GetEntities(matcher);

            return entities;
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

        // Update
        public static int PromoteProductCompanyToGroup(GameContext context, int companyId)
        {
            var c = GetCompanyById(context, companyId);

            Dictionary<int, int> founders = c.shareholders.Shareholders;

            int companyGroupId = GenerateCompanyGroup(context, c.company.Name + " Group", founders);
            AttachToGroup(context, companyGroupId, companyId);

            return companyGroupId;
        }

        public static void SetPlayerControlledCompany(GameContext context, int id)
        {
            var c = GetCompanyById(context, id);

            c.isControlledByPlayer = true;
            c.isSelectedCompany = true;
        }

        public static void RemovePlayerControlledCompany(GameContext context, int id)
        {
            GetCompanyById(context, id).isControlledByPlayer = false;
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

        public static void AttachToGroup(GameContext context, int parent, int subsidiary)
        {
            var p = GetCompanyById(context, parent);

            if (!IsCompanyGroupLike(p))
                return;

            var s = GetCompanyById(context, subsidiary);

            Debug.Log("Attach " + s.company.Name + " to " + p.company.Name);

            Dictionary<int, int> shareholders = new Dictionary<int, int>();
            shareholders.Add(p.shareholder.Id, 100);

            if (s.hasShareholders)
                s.ReplaceShareholders(shareholders);
            else
                s.AddShareholders(shareholders);
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

        // it is done for player only!
        internal static void LeaveCEOChair(GameContext gameContext, int companyId)
        {
            RemovePlayerControlledCompany(gameContext, companyId);

            // update company ceo component
        }
    }
}
