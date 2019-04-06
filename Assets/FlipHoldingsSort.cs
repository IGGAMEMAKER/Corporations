public class FlipHoldingsSort : ButtonController
{
    public FillCompanyOwnings FillCompanyOwnings;

    public override void Execute()
    {
        FillCompanyOwnings.ToggleSortingOrder();
    }
}
