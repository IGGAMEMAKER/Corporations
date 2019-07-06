using Assets.Utils;

public class FillSharesList : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        GetComponent<ShareholdersListView>().SetItems(GetInvestments());
    }

    private GameEntity[] GetInvestments()
    {
        if (SelectedHuman.hasShareholder)
            return InvestmentUtils.GetInvestmentsOf(GameContext, SelectedHuman.shareholder.Id);

        return new GameEntity[0];
    }
}
