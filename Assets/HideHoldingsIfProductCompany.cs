public class HideHoldingsIfProductCompany : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return SelectedCompany.hasProduct;
    }
}
