using Assets.Utils;

public class FillPossibleInvestorsList : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var list = CompanyUtils.GetPotentialInvestors(GameContext, SelectedCompany.company.Id);

        GetComponent<ShareholderProposalsListView>()
            .SetItems(list);
    }
}