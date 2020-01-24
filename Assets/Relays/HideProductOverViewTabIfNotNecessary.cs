public class HideProductOverViewTabIfNotNecessary : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return !SelectedCompany.hasProduct;
    }
}
