using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    partial class Companies
    {
        public static void ConfirmCorporateAcquisitionOffer(GameContext gameContext, int companyId, GameEntity corporation)
        {
            var company = Get(gameContext, companyId);
            var offer = GetAcquisitionOffer(gameContext, company, corporation);

            JoinCorporation(gameContext, companyId, corporation);
        }

        public static void JoinCorporation(GameContext gameContext, int companyId, GameEntity corporation)
        {
            var target = Get(gameContext, companyId);

            var shareholders = GetShareholders(target);
            int[] array = new int[shareholders.Keys.Count];


            var corporationCost = Economy.CostOf(corporation, gameContext);
            var targetCost = Economy.CostOf(target, gameContext);

            // TODO
            // var corporationShares = Companies.GetTotalShares(gameContext, companyId);
            var corporationShares = Companies.GetTotalShares(target);
            var emitedShares = corporationShares * targetCost / corporationCost;

            // give shares in corporation to shareholders of integratable company
            foreach (var shareholderId in array)
            {
                var percentOfSharesInPreviousCompany = GetShareSize(gameContext, target, shareholderId);

                var newShare = emitedShares * percentOfSharesInPreviousCompany / 100;

                AddShares(gameContext, corporation, shareholderId, (int)newShare);
                Debug.Log($"investor {GetInvestorName(gameContext, shareholderId)} will get {(int)newShare} shares of corporation {corporation.company.Name}");
            }


            foreach (var shareholderId in array)
            {
                RemoveShareholder(target, gameContext, shareholderId);
            }
            //AddShareholder(gameContext, target, buyerInvestorId, 100);
            AddShareholder(target, corporation, 100);
            SetIndependence(target, false);

            NotifyAboutCorporateAcquisition(gameContext, corporation, companyId);
        }
    }
}
