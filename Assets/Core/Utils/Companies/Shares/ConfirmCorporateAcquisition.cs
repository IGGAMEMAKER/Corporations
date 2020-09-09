using System.Collections.Generic;
using UnityEngine;

namespace Assets.Core
{
    partial class Companies
    {
        public static void ConfirmCorporateAcquisitionOffer(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var offer = GetAcquisitionOffer(gameContext, companyId, buyerInvestorId);

            JoinCorporation(gameContext, companyId, buyerInvestorId);
        }

        public static void JoinCorporation(GameContext gameContext, int companyId, int buyerInvestorId)
        {
            var target = Get(gameContext, companyId);
            var corporation = Investments.GetCompanyByInvestorId(gameContext, buyerInvestorId);

            var shareholders = GetShareholders(target);
            int[] array = new int[shareholders.Keys.Count];


            var corporationCost = Economy.GetCompanyCost(gameContext, corporation);
            var targetCost = Economy.GetCompanyCost(gameContext, target);
            
            var corporationShares = Companies.GetTotalShares(gameContext, companyId);
            var emitedShares = corporationShares * targetCost / corporationCost;

            // give shares in corporation to shareholders of integratable company
            foreach (var shareholderId in array)
            {
                var percentOfSharesInPreviousCompany = GetShareSize(gameContext, companyId, shareholderId);

                var newShare = emitedShares * percentOfSharesInPreviousCompany / 100;

                AddShares(gameContext, corporation, shareholderId, (int)newShare);
                Debug.Log($"investor {GetInvestorName(gameContext, shareholderId)} will get {(int)newShare} shares of corporation {corporation.company.Name}");
            }


            foreach (var shareholderId in array)
            {
                RemoveShareholder(target, gameContext, shareholderId);
            }
            AddShareholder(gameContext, companyId, buyerInvestorId, 100);
            target.isIndependentCompany = false;

            NotifyAboutCorporateAcquisition(gameContext, buyerInvestorId, companyId);
        }
    }
}
