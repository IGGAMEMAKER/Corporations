public class HideHoldingsIfProductCompany : HideOnSomeCondition
{
    public override bool HideIfTrue()
    {
        return SelectedCompany.hasProduct;
    }
}
