public class HideHoldingsIfProductCompany : HideOnSomeCondition
{
    public override bool CheckConditions()
    {
        return SelectedCompany.hasProduct;
    }
}
