using Assets.Utils;

public class FillShareholderProposalsList : View
    , IAnyDateListener
{
    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }

    void OnEnable()
    {
        LazyUpdate(this);

        Render();
    }

    void Render()
    {
        var proposals = CompanyUtils.GetInvestmentProposals(GameContext, SelectedCompany.company.Id);

        GetComponent<ShareholderProposalsListView>()
            .SetItems(proposals.ToArray(), SelectedCompany);
    }
}
