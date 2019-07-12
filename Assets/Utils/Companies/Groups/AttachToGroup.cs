using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static void AttachToGroup(GameContext context, int parent, int subsidiary)
        {
            // TODO only possible if independent!

            var p = GetCompanyById(context, parent);

            if (!IsCompanyGroupLike(p))
                return;

            var s = GetCompanyById(context, subsidiary);

            Debug.Log("Attach " + s.company.Name + " to " + p.company.Name);

            var shareholders = new Dictionary<int, BlockOfShares>
            {
                {
                    p.shareholder.Id,
                    new BlockOfShares
                    {
                        amount = 100,
                        InvestorType = InvestorType.Strategic,
                        shareholderLoyalty = 100
                    }
                }
            };

            if (s.hasShareholders)
                s.ReplaceShareholders(shareholders);
            else
                s.AddShareholders(shareholders);

            p.isIndependentCompany = false;
        }

    }
}
