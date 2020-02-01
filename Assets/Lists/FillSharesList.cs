using Assets.Core;

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
            return Investments.GetInvestmentsOf(Q, SelectedHuman.shareholder.Id);

        return new GameEntity[0];
    }
}
