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
        var human = SelectedHuman;

        if (human.hasShareholder)
            return Investments.GetOwnings(Q, human.shareholder.Id);

        return new GameEntity[0];
    }
}
