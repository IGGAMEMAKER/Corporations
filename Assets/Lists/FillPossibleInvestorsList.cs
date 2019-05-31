using Assets.Utils;

public class FillPossibleInvestorsList : View
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
        var list = CompanyUtils.GetPotentialInvestors(GameContext, SelectedCompany.company.Id);

        GetComponent<ShareholderProposalsListView>()
            .SetItems(list);
    }
}