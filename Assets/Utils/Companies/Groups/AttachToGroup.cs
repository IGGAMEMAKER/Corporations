using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static void AttachToGroup(GameContext context, int parentId, int subsidiaryId)
        {
            // TODO only possible if independent!

            var parent = GetCompanyById(context, parentId);


            // we cannot attach company to product company
            if (!IsCompanyGroupLike(parent))
                return;

            var daughter = GetCompanyById(context, subsidiaryId);

            if (daughter.hasProduct)
            {
                AddFocusNiche(daughter.product.Niche, parent, context);
                var industry = NicheUtils.GetIndustry(daughter.product.Niche, context);

                AddFocusIndustry(industry, parent);
            }


            Debug.Log("Attach " + daughter.company.Name + " to " + parent.company.Name);

            var shareholders = new Dictionary<int, BlockOfShares>
            {
                {
                    parent.shareholder.Id,
                    new BlockOfShares
                    {
                        amount = 100,
                        InvestorType = InvestorType.Strategic,
                        shareholderLoyalty = 100
                    }
                }
            };

            if (daughter.hasShareholders)
                daughter.ReplaceShareholders(shareholders);
            else
                daughter.AddShareholders(shareholders);

            daughter.isIndependentCompany = false;
        }

    }
}
