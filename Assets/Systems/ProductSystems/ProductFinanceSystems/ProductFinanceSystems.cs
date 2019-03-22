public class ProductFinanceSystems : Feature
{
    public ProductFinanceSystems(Contexts contexts) : base("Product Finance Systems")
    {
        //// Adds task when upgrade product button is pressed
        //Add(new ProductRegisterUpgradeEvent(contexts));

        Add(new ProductExecutePriceChangeEvent(contexts));
    }
}
