﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    partial class Companies
    {
        public static void AttachToGroup(GameContext context, GameEntity parent, GameEntity daughter)
        {
            // TODO only possible if independent!

            // we cannot attach company to product company
            if (parent.hasProduct)
                return;

            if (daughter.hasProduct)
            {
                var industry = Markets.GetIndustry(daughter.product.Niche, context);

                AddFocusNiche(parent, daughter.product.Niche, context);
                AddFocusIndustry(industry, parent);
            }

            // TODO why not make it with transfer shares function?
            RemoveAllShareholders(context, daughter);
            AddShares(daughter, parent, 100);

            SetIndependence(daughter, false);
        }

        public static void AddOwning(GameEntity company, int owningCompanyId)
        {
            if (!company.ownings.Holdings.Contains(owningCompanyId))
                company.ownings.Holdings.Add(owningCompanyId);
        }

        public static void RemoveOwning(GameEntity owner, int owningCompanyId)
        {
            if (owner.ownings.Holdings.Contains(owningCompanyId))
                owner.ownings.Holdings.Remove(owningCompanyId);
        }

        public static GameEntity CreateProductAndAttachItToGroup(GameContext gameContext, NicheType nicheType, GameEntity group)
        {
            string name = group.company.Name + " " + Enums.GetFormattedNicheName(nicheType);

            var c = GenerateProductCompany(gameContext, name, nicheType);

            //AttachToGroup(gameContext, group.company.Id, c.company.Id);
            AttachToGroup(gameContext, group, c);

            return c;
        }

        public static void TurnProductToPlayerFlagship(GameEntity company, GameContext Q, NicheType nicheType, GameEntity parentCompany)
        {
            company.isFlagship = true;
            AttachToPlayer(company);
            company.AddChannelExploration(new Dictionary<int, int>(), new List<int>(), 1);

            // give bad positioning initially
            var infos = Marketing.GetAudienceInfos();

            Marketing.AddClients(company, -50);

            var positionings = Markets.GetNichePositionings(nicheType, Q);
            var positioningWorths = positionings.OrderBy(Markets.GetPositioningValue);

            // TODO POSITIONING
            var rand = Random.Range(0, 2);
            var newPositioning = rand < 1 ? 0 : 3; //  positioningWorths.ToArray()[rand].ID;
            // 0 - teens, 3 - old people

            Marketing.ChangePositioning(company, Q, newPositioning);

            Marketing.AddClients(company, 50);

            // give good salary to CEO, so he will not leave company
            var CEO = Humans.Get(Q, GetCEOId(company));
            var GroupCeoID = GetCEOId(parentCompany);

            CEO.AddPseudoHuman(GroupCeoID);
            
            // var salary = Teams.GetSalaryPerRating(CEO);
            // Teams.SetJobOffer(CEO, company, new JobOffer(salary), 0, Q);
        }

    }
}
