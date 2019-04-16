using System.Collections.Generic;

namespace Assets.Utils
{
    partial class CompanyUtils
    {
        public static List<InvestmentProposal> GetInvestmentProposals(GameContext context, int companyId)
        {
            return GetCompanyById(context, companyId).investmentProposals.Proposals;
            
            return new List<InvestmentProposal>();
            //return GetCompanyById(context, companyId).Inves
            //return GetCompanyById(context, companyId).shareholders.Shareholders;
        }
    }
}
