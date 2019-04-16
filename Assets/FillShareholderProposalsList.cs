using Assets.Utils;
using System.Collections.Generic;
using System.Linq;

public class FillShareholderProposalsList : View
{
    void OnEnable()
    {
        Render();
    }

    void Render()
    {
        var proposals = CompanyUtils.GetInvestmentProposals(GameContext, SelectedCompany.company.Id); // GetProposals();

        GetComponent<ShareholderProposalsListView>()
            .SetItems(proposals.ToArray(), SelectedCompany);
    }
}
